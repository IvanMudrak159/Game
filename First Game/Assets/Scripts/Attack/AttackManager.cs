using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[System.Serializable]
public struct lvl
{
	public GameObject route;
	public int score;
	public bool isDefault;
}
public class AttackManager : MonoBehaviour
{
	#region Variables
	//Score
	[HideInInspector] public int scorePoints = 0;
	//Default shooting
	private bool standart = false;
	private bool coroutineAllowed = true;
	public List<Rigidbody> rocketsRigidbodies;
	private float angle;
	public PlayerMovement playerMovement;
	private const float pi = 3.14f;
	[SerializeField] private Transform player;
	//Variables for calculating speed of the default bullets
	private float speed;
	private float force = 500;
	private float timeBullet = 1;
	private float radSpeed;
	//Timer for default shooting
	private float time = 0f;
	public float defaultTimeStep = 0.1f;
	private float saveTimeStep;
	//Level content
	public lvl[] lvls;
	private int level = 0;
	#endregion
	private void Start()
	{
		saveTimeStep = defaultTimeStep;

		//Give initial value to bullet speed
		radSpeed = playerMovement.speedModifier * 3.14f;//0.1 speedModifier = 3.14 speed 
		timeBullet = 500f / force;
		speed = timeBullet * radSpeed;
		standart = lvls[level].isDefault;
		if (lvls[level].route != null && !standart)
		{
			lvls[level].route.SetActive(true);
		}
	}
	private void Update()
	{
		//Default shooting
		if (standart)
		{
			if (time > defaultTimeStep)
			{
				defaultTimeStep += saveTimeStep;
				if (coroutineAllowed)
				{
					StartCoroutine(C_Straight());
				}
			}
			if(rocketsRigidbodies.Count != 0)
			{
				time += Time.deltaTime;
			}
		}
		//Increase difficulty
		if (scorePoints == lvls[level].score)
		{
			standart = false;
			force += 10;
			playerMovement.speedModifier += 0.003f;
			radSpeed = playerMovement.speedModifier * 3.14f;
			timeBullet = 500f / force;
			speed = timeBullet * radSpeed;
			lvls[level].route.SetActive(false);
			if (level < lvls.Length)
			{
				level++;
			}
			//Choose special route
			if (lvls[level].isDefault)
			{
				standart = true;
			}
			else
			{
				lvls[level].route.SetActive(true);
			}
		}
	}

	private IEnumerator C_Straight()
	{
		coroutineAllowed = false;
		int attackPlayer = 0;
		int addRandom = 1;
		int length = Random.Range(1, 3);
		for (int i = 0; i < length; i++)
		{
			if (rocketsRigidbodies.Count == 0)
			{
				break;
			}
			float probability = Random.Range(0, 11);
			if (probability > 6)
			{
				attackPlayer = 1;
			}
			if (probability > 4)
			{
				addRandom = 0;
			}
			float randomAngle = Random.Range(0.4f, 0.6f);
			if (playerMovement.AnotherSide == -1)
			{
				randomAngle *= -1;
			}
			rocketsRigidbodies[0].velocity = Vector3.zero;
			rocketsRigidbodies[0].gameObject.SetActive(true);
			angle = Angle();
			float a = (angle + playerMovement.AnotherSide * speed * attackPlayer + randomAngle * addRandom) * 180 / pi;
			Quaternion rotation = Quaternion.Euler(90, 0, a);
			transform.rotation = rotation;
			rocketsRigidbodies[0].AddForce(transform.right * force);
			rocketsRigidbodies.Remove(rocketsRigidbodies[0]);
			//Debug.Log("Rocket[" + rocketsRigidbodies[0].gameObject + "has been removed");
			attackPlayer = 0;
			addRandom = 1;
			yield return new WaitForSeconds(0.115f);
		}
		coroutineAllowed = true;
	}
	private float Angle()
	{
		if (player.position.x >= 0)
		{
			if (player.position.z >= 0)
			{
				return Mathf.Atan2(player.position.z, player.position.x);
			}
			else
			{
				return Mathf.Atan2(player.position.z, player.position.x) + 2 * pi;
			}
		}
		else
		{
			if (player.position.z >= 0)
			{
				return Mathf.Atan2(player.position.z, player.position.x);
			}
			else
			{
				return Mathf.Atan2(player.position.z, player.position.x) + 2 * pi;
			}
		}
	}
}