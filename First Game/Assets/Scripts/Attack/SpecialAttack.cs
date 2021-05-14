using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
	public float speedX = 50f;
	public BulletManager[] bulletsManagers;
	public List<Transform> bullets;
	public List<RouteDraw> NextPatternsInChain;
	public float time = 0f;
	public float timeStep = 1f;
	private float saveTimeStep;
	private int counter = 0;
	public int numberOfBullets = 4;
	private int bulletCounter = 0;
	public int bulletLimit = 4;

	private void Awake()
	{
		NextPatternsInChain.Insert(0, GetComponent<RouteDraw>());
		saveTimeStep = timeStep;
		for (int i = 0; i < bulletsManagers.Length; i++)
		{
			for (int j = 0; j < bulletsManagers[i].children.Count; j++)
			{
				bullets.Add(bulletsManagers[i].children[j].transform);
			}
		}
	}
	private void OnEnable()
	{
		for (int i = 0; i < bulletsManagers.Length; i++) //добавляем сами пути в массив routes
		{
			bulletsManagers[i].routes.Clear();
			for (int j = 0; j < NextPatternsInChain.Count; j++)
			{
				bulletsManagers[i].routes.Add(NextPatternsInChain[j]);
			}
			bulletsManagers[i].SetRoutes();//задаем для шариков пути по которым они будут двигаться
		}
	}
	void Update()
    {
		transform.Rotate(Vector3.forward * Time.deltaTime * speedX);
		if (bullets.Count != 0)
		{
			time += Time.deltaTime;
		}

		/*if (bulletCounter == bulletLimit)
		{
			bulletCounter = 0;
			time -= 1f;
		}*/
		if (time > timeStep)
		{
			if (bullets[0].gameObject.activeSelf)
			{
				bullets.Add(bullets[0]);//dunno why this works
			}
			bulletCounter++;
			timeStep += saveTimeStep;
			bullets[0].gameObject.SetActive(true);
			bullets.Remove(bullets[0]);
		}
	}
	public void Decrease()
	{
		if(counter < 5)
		{
			saveTimeStep -= saveTimeStep * 0.05f;
			counter++;
		}
	}
}
