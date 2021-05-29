using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 15f;
    void Update()
    {
        transform.RotateAround(transform.right, speed * Time.deltaTime);
    }
}
