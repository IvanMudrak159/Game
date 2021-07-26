using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
	public UILineRenderer line;
	public Animation playerAnimation;
	public PowerUpManager powerUpManager;
	public GameObject deathPanel;
	public Text healthText;
	[HideInInspector] public int health;
	public bool protectEnabled = false;
	private float timeOfProtection = 1f;
	public ParticleSystem particleSystem;

	private void Awake()
	{
		health = (int)PlayerPrefs.GetFloat("Health", 4);
		healthText.text = "HP: " + health;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Bullet") && !protectEnabled)
		{
			StartCoroutine(C_EnableParticle());	
			line.AddValue(1f, -1.5f);
			Vibration.Vibrate(100);
			health--;
			healthText.text = "HP: " + health;
			if(health < 2 && Random.Range(0,11) > 7)
			{
				powerUpManager.PowerUpPool();
			}
			if (health > 0)
			{
				StartCoroutine(C_EnableProtection(timeOfProtection));
			}
			else
			{
				deathPanel.SetActive(true);
				PlayerPrefs.SetInt("Money", GetComponent<Score>().score + PlayerPrefs.GetInt("Money"));
				GetComponent<Score>().ShowHighScore();
				Time.timeScale = 0;
			}
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
		playerAnimation.Play();
		protectEnabled = true;
		yield return new WaitForSeconds(lifeTime);
		playerAnimation.Stop();
		protectEnabled = false;
	}
}
