using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlDistribute : MonoBehaviour
{
    public bool easy;
    public bool medium;
    public bool hard;
    public AttackManager attackManager;
	public string key;
	[SerializeField]
	private int value;
	public void AddRoute()
	{
		if (PlayerPrefs.GetInt(key, value) == 1)
		{
			if (easy)
			{
				attackManager.easyRoutes.Add(gameObject);
			}
			if (medium)
			{
				attackManager.mediumRoutes.Add(gameObject);
			}
			if (hard)
			{
				attackManager.hardRoutes.Add(gameObject);
			}
		}
	}
}
