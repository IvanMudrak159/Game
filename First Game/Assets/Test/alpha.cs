using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alpha : MonoBehaviour
{
	public float radius;
	public float k;
	private Material material;
	private Color color;
	private void Start()
	{
		material = GetComponent<Renderer>().material;
	}
	private void Update()
	{
		color = material.color;
		if (Mathf.Abs(transform.position.y) > radius)
		{
			color.a = k * Mathf.Abs(Mathf.Abs(transform.position.y) - radius);
		}
		else
		{
			color.a = 0;
		}
		material.color = color;
	}
}
