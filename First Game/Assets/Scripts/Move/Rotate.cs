using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 direction = Vector3.zero;
	public bool redAxis, blueAxis, greenAxis, invert;
	private void Awake()
	{
		if (redAxis)
		{
			direction = transform.right;
		}
		else if(blueAxis)
		{
			direction = transform.forward;
		}
		else if (greenAxis)
		{
			direction = transform.up;
		}
		if (invert)
		{
			direction = -direction;
		}
	}
	void Update()
    {
        transform.RotateAround(direction, speed * Time.deltaTime);
    }
}
