using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
	public float bulletSpeed = 0.5f;
	public float speed = 50f;
	public BulletManager[] bulletsManagers;
	public List<Transform> bullets;
	public List<Transform> NextPatternsInChain;
	public float time = 0f;
	public float timeStep = 1f;
	private float saveTimeStep;
	public int numberOfBullets = 4;
	public int bulletLimit = 4;

	private void Awake()
	{
		Score.scoreSingleton.powerUpSpawn += AddPowerUp;
		NextPatternsInChain.Insert(0, GetComponent<Transform>());
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
			bulletsManagers[i].bulletSpeed = bulletSpeed;
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
		transform.Rotate(Vector3.forward * Time.deltaTime * speed);
		if (bullets.Count != 0)
		{
			time += Time.deltaTime;
		}
		if (time > timeStep)
		{
			if (bullets[0].gameObject.activeSelf)
			{
				bullets.Add(bullets[0]);//dunno why this works
			}
			timeStep += saveTimeStep;
			bullets[0].gameObject.SetActive(true);
			bullets.Remove(bullets[0]);
		}
	}
	private void AddPowerUp()
	{
		Transform powerUp = PowerUpManager.powerUpManager.PowerUpPool();
		SpecialAttackMove specialAttackMove = powerUp.GetComponent<SpecialAttackMove>();
		specialAttackMove.routes.Clear();
		specialAttackMove.speedModifier = bulletSpeed;
		specialAttackMove.routes.Add(this.transform);
		specialAttackMove.isPowerUp = true;
		bullets.Insert(0,powerUp);
	}
}
