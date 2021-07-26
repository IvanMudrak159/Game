using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTrigger : MonoBehaviour
{
	public Health health;
	private int hp;
	private void Start()
	{
		hp = (int)PlayerPrefs.GetFloat("Shield", 1);
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Bullet"))
		{
			hp--;
			if(hp == 0)
			{
			health.protectEnabled = false;
			gameObject.SetActive(false);
			}
		}
	}
}
