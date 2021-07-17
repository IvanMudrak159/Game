using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTrigger : MonoBehaviour
{
	public Health health;
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Bullet"))
		{
			health.protectEnabled = false;
			gameObject.SetActive(false);
		}
	}
}
