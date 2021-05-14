using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[System.Serializable]
public struct lvl
{
	public int easy, medium, hard;
	public int score;
	public bool isDefault;
}
public class AttackManager : MonoBehaviour
{
	#region Variables
	//Score
	public int scorePoints = 0;
	//Default shooting
	private bool standart = true;
	public bool coroutineAllowed = true;
	public List<Rigidbody> rocketsRigidbodies;
	private float angle;
	private PlayerMovement playerMovement;
	private const float pi = 3.14f;
	[SerializeField] private Transform player;
	//Variables for calculating speed of the default bullets
	private float speed;
	private float force = 500;
	private float timeBullet = 1;
	private float radSpeed;
	//Timer for default shooting
	private float time = 0f;
	private float timeStep = 0.1f;
	private float saveTimeStep;
	//Choosing route for shooting
	#endregion
	public lvl[] lvls;
	private int level = 0;
	public List<GameObject> easyRoutes;
	public List<GameObject> mediumRoutes;
	public List<GameObject> hardRoutes;
	public List<GameObject> temporaryRoutes;
	private int lastChosenRoute = -1;
	private int scoreStep = 8;
	private int oldScorePoints = 0; 
	private void Start()
	{
		playerMovement = player.gameObject.GetComponent<PlayerMovement>();
		saveTimeStep = timeStep;

		//Give initial value to bullet speed
		radSpeed = playerMovement.speedModifier * 3.14f;//0.1 speedModifier = 3.14 speed 
		timeBullet = 500f / force;
		speed = timeBullet * radSpeed;
	}
	private void Update()
	{
		//Default shooting
		if (standart)
		{
			if (time > timeStep)
			{
				timeStep += saveTimeStep;
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
		else if (scorePoints % scoreStep == 0)
		{
			if(scorePoints != oldScorePoints)
			{
				oldScorePoints = scorePoints;
				EnableRoute();
			}
		}
		//Increase difficulty
		if (scorePoints == lvls[level].score)
		{
			//ClearTemporaryRoute();
			standart = false;
			force += 10;
			playerMovement.speedModifier += 0.003f;
			radSpeed = playerMovement.speedModifier * 3.14f;
			timeBullet = 500f / force;
			speed = timeBullet * radSpeed;
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
				int random;
				#region Add routes to list
				for (int i = 0; i < lvls[level].easy; i++)
				{
					do
					{
						random = Random.Range(0, easyRoutes.Count);
					} while (temporaryRoutes.Contains(easyRoutes[random]));
					temporaryRoutes.Add(easyRoutes[random]);
				}
				for (int i = 0; i < lvls[level].medium; i++)
				{
					do
					{
						random = Random.Range(0, mediumRoutes.Count);
					} while (temporaryRoutes.Contains(mediumRoutes[random]));
					temporaryRoutes.Add(mediumRoutes[random]);
				}
				for (int i = 0; i < lvls[level].hard; i++)
				{
					do
					{
						random = Random.Range(0, hardRoutes.Count);
					} while (temporaryRoutes.Contains(hardRoutes[random]));
					temporaryRoutes.Add(hardRoutes[random]);
				}
				#endregion
				EnableRoute();
			}
		}
	}

	private void EnableRoute()
	{
		if (lastChosenRoute == -1)
		{
			lastChosenRoute = Random.Range(0, temporaryRoutes.Count);
		}
		else
		{
			temporaryRoutes[lastChosenRoute].SetActive(false);
			temporaryRoutes.Remove(temporaryRoutes[lastChosenRoute]);
			lastChosenRoute = Random.Range(0, temporaryRoutes.Count);
		}
		temporaryRoutes[lastChosenRoute].SetActive(true);
	}

	private void ClearTemporaryRoute()
	{
		if (lastChosenRoute != -1)
		{
			temporaryRoutes[lastChosenRoute].SetActive(false);
			lastChosenRoute = -1;
			temporaryRoutes.Clear();
		}
	}

	private IEnumerator C_Straight()
	{
		coroutineAllowed = false;
		int attackPlayer = 0;
		int addRandom = 1;
		int length = Random.Range(1, 4);
		for (int i = 0; i < length; i++)
		{
			if (rocketsRigidbodies.Count == 0)
			{
				break;
			}
			float probability = Random.Range(0, 11);
			if(probability > 7)
			{
				attackPlayer = 1;
			}
			if(probability > 4)
			{
				addRandom = 0;
			}
			float randomAngle = Random.Range(0.4f, 0.6f);
			if (playerMovement.anotherSide == -1)
			{
				randomAngle *= -1;
			}
			rocketsRigidbodies[0].velocity = Vector3.zero;
			rocketsRigidbodies[0].gameObject.SetActive(true);
			angle = Angle;
			float a = (angle + playerMovement.anotherSide * speed * attackPlayer + randomAngle * addRandom) * 180 / pi;
			Quaternion rotation = Quaternion.Euler(90, 0, a);
			transform.rotation = rotation;
			rocketsRigidbodies[0].AddForce(transform.right * force);
			rocketsRigidbodies.Remove(rocketsRigidbodies[0]);
			attackPlayer = 0;
			addRandom = 1;
			yield return new WaitForSeconds(0.115f);
		}
		coroutineAllowed = true;
	}
	private float Angle
	{
		get
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
}