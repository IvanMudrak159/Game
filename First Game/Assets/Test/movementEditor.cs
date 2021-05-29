using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementEditor : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;
    void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        Vector3 velocity = new Vector3(playerInput.x, playerInput.y, 0f) * maxSpeed;
        Vector3 displacement = velocity * Time.deltaTime;
        transform.localPosition += displacement;
    }
}
