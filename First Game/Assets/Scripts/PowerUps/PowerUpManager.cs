using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
	[HideInInspector] public static PowerUpManager powerUpManager { get; private set; }

	public PowerUp[] powerUps;
	public Sprite[] powerUpSprites;

	public Image timerSprite;

	private int randomIndex;
	public void Awake()
	{
		powerUpManager = this;
		for (int i = 0; i < powerUps.Length; i++)
		{
			powerUps[i].Index = i;
		}
	}
	public Transform PowerUpPool()
	{
		randomIndex = Random.Range(0, powerUps.Length);
		PowerUp go = Instantiate(powerUps[randomIndex]);
		go.Index = randomIndex;
		return go.transform;
	}
	public void Timer(int index, float lifeTime)
	{
		if(lifeTime ==0 || index == -1)
		{
			return;
		}
		timerSprite.transform.parent.gameObject.SetActive(true);
		timerSprite.sprite = powerUpSprites[index];
		StartCoroutine(C_Timer(lifeTime));
	}
	private IEnumerator C_Timer(float lifeTime)
	{
		yield return new WaitForSeconds(lifeTime);
		timerSprite.transform.parent.gameObject.SetActive(false);
	}
}