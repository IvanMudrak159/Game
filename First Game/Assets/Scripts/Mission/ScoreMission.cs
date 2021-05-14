using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMission : Mission
{
	public int score;
	public int health;
	public void Update()
	{

		if (health == playerHealth.health && score == playerScore.score)
		{
			Debug.Log("Quest competed");
		}
	}
}