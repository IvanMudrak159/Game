using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
	public float speedModifier = 0.35f;
	private int anotherSide = 1;
	public int AnotherSide {
		get => anotherSide;
		private set
		{
			anotherSide = value;
		}
	}

	private void Update()
	{
		transform.Rotate(Vector3.up * speedModifier * anotherSide * Time.deltaTime);
		if (Input.GetMouseButtonDown(0))
		{
			AnotherSide *= -1;
		}
	}
}