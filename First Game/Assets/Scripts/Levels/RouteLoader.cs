/*This script was made to avoid some errors linked with event functions of BulletManager, SpecialAttack*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteLoader : MonoBehaviour
{
	public List<LvlDistribute> routes;
	public GameObject routesParent;
	private void Awake()
	{
		for (int i = 0; i < routes.Count; i++)
		{
			routes[i].AddRoute();
		}
	}
}
