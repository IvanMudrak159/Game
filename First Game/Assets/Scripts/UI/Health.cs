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
	public int health = 5;
	public bool protectEnabled = false;
	private float timeOfProtection = 1f;

	private void Awake()
	{
		healthText.text = "HP: " + health;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Bullet") && !protectEnabled)
		{
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

	public IEnumerator C_EnableProtection(float lifeTime)
	{
		playerAnimation.Play();
		protectEnabled = true;
		yield return new WaitForSeconds(lifeTime);
		playerAnimation.Stop();
		protectEnabled = false;
	}
}
