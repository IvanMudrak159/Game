using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackMove : MonoBehaviour
{
	private SpecialAttack autoAttack; 
	public List<RouteDraw> routes;
	public List<Vector3> points;
	private float tParam = 0f;
	public float speedModifier = 0.3f;
	private IEnumerator GoByRoute()
	{
		while (points.Count != 0)
		{
			Vector3 p0, p1, p2, p3;
			p0 = points[0];
			p1 = points[1];
			p2 = points[2];
			p3 = points[3];
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
			points.RemoveAt(0);
			points.RemoveAt(0);
			points.RemoveAt(0);
			points.RemoveAt(0);
		}
		autoAttack.bullets.Add(transform);
		gameObject.SetActive(false);
	}
	private void OnEnable()
	{
		for (int i = 0; i < routes.Count; i++)
		{	
			for (int j = 0; j < routes[i].controlPoints.Length; j++)
			{
				points.Add(routes[i].controlPoints[j].position);
			}
		}
		if (routes.Count != 0)
		{
			autoAttack = routes[0].GetComponent<SpecialAttack>();
			StartCoroutine(GoByRoute());
		}
	}
}