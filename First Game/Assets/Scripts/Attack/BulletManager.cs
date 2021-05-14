using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
	public List<RouteDraw> routes;
	public List<SpecialAttackMove> children;
	public void Awake()
	{
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			children.Add(transform.GetChild(i).gameObject.GetComponent<SpecialAttackMove>());
		}
	}
	public void SetRoutes()
	{
		for (int i = 0; i < children.Count; i++)
		{
			children[i].routes.Clear();
			for (int j = 0; j < routes.Count; j++)
			{
				children[i].routes.Add(routes[j]);
			}
		}
	}
}
