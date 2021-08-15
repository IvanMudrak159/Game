using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	public UILineRenderer line;
	public PowerUpManager powerUpManager;
	public GameObject deathPanel;
	public Text healthText;
	[HideInInspector] public int health;
	public bool protectEnabled = false;
	private float timeOfProtection = 1f;
	public ParticleSystem particleSystem;
	public Menu menu;
	[Header("Sounds")]
	private AudioSource audio;
	public AudioClip death;
	public AudioClip hit;
	private int duration;

	private void Awake()
	{
		audio = GetComponent<AudioSource>();
		AdManager.ExtraLife += HealthReward;
		health = (int)PlayerPrefs.GetFloat("Health", 4);
		healthText.text = "HP: " + health;
	}
	private void HealthReward()
	{
		audio.Stop();	
		deathPanel.SetActive(false);
		health++;
		healthText.text = "HP: " + health;
		StartCoroutine(menu.C_Timer());
		StartCoroutine(C_EnableProtection(4.5f));
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Bullet") && !protectEnabled)
		{
			StartCoroutine(C_EnableParticle());	
			line.AddValue(1f, -1.5f);
			Vibration.Vibrate(PlayerPrefs.GetInt("Vibration", 100));
			health--;
			healthText.text = "HP: " + health;
			if(health < 2 && Random.Range(0,11) > 7)
			{
				powerUpManager.PowerUpPool();
			}
			if (health > 0)
			{
				audio.clip = hit;
				StartCoroutine(C_EnableProtection(timeOfProtection));
			}
			else
			{
				audio.clip = death;
				if(Random.Range(0f, 1f) > 0.7f)
				{
				AdManager.ShowAdsVideo("Interstitial_Android");
				}
				deathPanel.SetActive(true);
				PlayerPrefs.SetInt("Money", GetComponent<Score>().score + PlayerPrefs.GetInt("Money"));
				GetComponent<Score>().ShowHighScore();
				Time.timeScale = 0;
			}
			audio.Play();
		}
	}
	public IEnumerator C_EnableParticle()
	{
		particleSystem.Play();
		yield return new WaitForSeconds(0.1f);
		particleSystem.Stop();
	}
	public IEnumerator C_EnableProtection(float lifeTime)
	{
		protectEnabled = true;
		yield return new WaitForSeconds(lifeTime);
		protectEnabled = false;
	}
}
