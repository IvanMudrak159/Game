using System.Collections;
using UnityEngine;
[RequireComponent(typeof(MeshRenderer))]
public class PowerUp : MonoBehaviour
{
	public PowerUpManager powerUpManager;
	[HideInInspector] public float lifeTime = 0;
	private int index = -1;

	public int Index { get => index;
		set
		{
			if(index == -1)
			{
				index = value;
			}
		}
	}
	protected void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			MeshRenderer mesh = GetComponent<MeshRenderer>();
			mesh.enabled = false;	
			powerUpManager.Timer(Index, lifeTime);
			Action();
		}
	}
	protected virtual void Action()
	{
	}
}