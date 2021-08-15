using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTrigger : MonoBehaviour
{
	public Health health;
	private int hp, totalHp;
	private void Start()
	{
		hp = (int)PlayerPrefs.GetFloat("Shield", 1);
		Debug.Log(hp);
		totalHp = hp;
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Bullet"))
		{
			hp--;
			Debug.Log(hp);
			if (hp <= 0)
			{
				hp = totalHp;
				health.protectEnabled = false;
				gameObject.SetActive(false);
			}
		}
	}
}
