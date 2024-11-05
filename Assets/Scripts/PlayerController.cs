using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    public float speedRotation;
    public Transform player;
    public Transform quad;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float horizon = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float MoveSpeed = speed;

        Vector3 move = transform.right * horizon + transform.forward * vertical;
        rb.MovePosition(transform.position + move.normalized * speed * Time.deltaTime);
    }
}
