using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackMove : MonoBehaviour
{
	public bool isPowerUp = false;
	private SpecialAttack autoAttack; 
	public List<Transform> routes;
	public List<Vector3> positions;
	private float tParam = 0f;
	public float speedModifier = 0.3f;
	private TrailRenderer trail;
	private void Awake()
	{
		trail = GetComponent<TrailRenderer>();
	}
	private IEnumerator GoByRoute()
	{
		int routeNumber = 0;
		while (routeNumber != routes.Count)
		{
			Vector3 p0, p1, p2, p3;
			p0 = positions[0];
			p1 = positions[1];
			p2 = positions[2];
			p3 = positions[3];
			positions.RemoveAt(0);
			positions.RemoveAt(0);
			positions.RemoveAt(0);
			positions.RemoveAt(0);

			transform.position = p0 + 3 * p1 + 3 * p2 + p3;
			while (tParam <= 1)
			{
				tParam += Time.deltaTime * speedModifier;
				transform.position = Mathf.Pow(1 - tParam, 3) * p0 +
				3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
				3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
				Mathf.Pow(tParam, 3) * p3;
				yield return new WaitForEndOfFrame();
			}
			tParam = 0;
			routeNumber++;
		}
		if (!isPowerUp)
		{
			trail.Clear();
			autoAttack.bullets.Add(transform);
			gameObject.SetActive(false);
		}
		else
		{
			GetComponent<MeshRenderer>().enabled = false;
		}
	}
	private void OnEnable()
	{
		if (routes.Count != 0)
		{
			autoAttack = routes[0].GetComponent<SpecialAttack>();
			for (int i = 0; i < routes.Count; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					positions.Add(routes[i].GetChild(j).position);
				}
			}
			StartCoroutine(GoByRoute());
		}
	}
}