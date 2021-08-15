using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	public Vector3 upperLimit, lowerLimit;
	private Vector3 moveDirection;
	public float moveSpeed = 1f;

    public float speed = 1f;
    public Vector3 direction = Vector3.zero;
	public bool redAxis, blueAxis, greenAxis, invert;
	private bool insideLimits = true;
	private void Awake()
	{
		moveDirection = (upperLimit - lowerLimit).normalized;
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
		if(transform.position.y > upperLimit.y || transform.position.y < lowerLimit.y)
		{
			if (insideLimits)
			{
			moveSpeed *= -1;
			}
			insideLimits = false;
		}
		else
		{
			insideLimits = true;
		}
		transform.localPosition += moveDirection * moveSpeed * Time.deltaTime;
    }
}
