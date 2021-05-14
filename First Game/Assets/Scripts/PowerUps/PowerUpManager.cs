using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
	public PlayerMovement playerMovement;
	public List<GameObject> powerUps;
	public List<GameObject> savePowerUps;
	public Transform[] routes;
	private int randomIndex;
	private int chosenRoute;
	public void Awake()
	{
		for (int i = 0; i < powerUps.Count; i++)
		{
			savePowerUps.Add(powerUps[i]);
		}
	}
	public void PowerUpPool()
	{
		if(powerUps.Count == 0)
		{
			for (int i = 0; i < savePowerUps.Count; i++)
			{
				powerUps.Add(savePowerUps[i]);
			}
		}
		randomIndex = Random.Range(0, powerUps.Count);
		powerUps[randomIndex].transform.position = GetPos();
		powerUps[randomIndex].SetActive(true);
		powerUps.RemoveAt(randomIndex);
	}
	protected Vector3 GetPos()
	{
		chosenRoute = 1 - playerMovement.routeToGo;
		Vector3 p0 = routes[chosenRoute].GetChild(0).position;
		Vector3 p1 = routes[chosenRoute].GetChild(1).position;
		Vector3 p2 = routes[chosenRoute].GetChild(2).position;
		Vector3 p3 = routes[chosenRoute].GetChild(3).position;
		float tParam = playerMovement.tParam;
		Vector3 pos = Mathf.Pow(1 - tParam, 3) * p0 +
				3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
				3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
				Mathf.Pow(tParam, 3) * p3;
		return pos;
	}
}