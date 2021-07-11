using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class x2PowerUp : PowerUp
{
	public float x2time;
	private void Awake()
	{
		x2time = PlayerPrefs.GetFloat("x2", 5);
	}
	protected override void Action()
	{
		StartCoroutine(C_x2());
	}
	private IEnumerator C_x2()
	{
		Score.scoreSingleton.scoreMultiplier = 2;
		yield return new WaitForSeconds(x2time);
		Score.scoreSingleton.scoreMultiplier = 1;
		Destroy(gameObject);
	}
}
