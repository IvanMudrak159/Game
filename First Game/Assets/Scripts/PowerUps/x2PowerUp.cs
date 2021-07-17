using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class x2PowerUp : PowerUp
{
	private void Awake()
	{
		lifeTime = PlayerPrefs.GetFloat("x2", 5);
	}
	protected override void Action()
	{
		StartCoroutine(C_x2());
	}
	private IEnumerator C_x2()
	{
		Score.scoreSingleton.scoreMultiplier = 2;
		yield return new WaitForSeconds(lifeTime);
		Score.scoreSingleton.scoreMultiplier = 1;
		Destroy(gameObject);
	}
}
