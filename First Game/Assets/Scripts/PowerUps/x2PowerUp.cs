using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class x2PowerUp : PowerUp
{
	public Score score;
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
		score.scoreMultiplier = 2;
		yield return new WaitForSeconds(x2time);
		score.scoreMultiplier = 1;
		gameObject.SetActive(false);
	}
}
