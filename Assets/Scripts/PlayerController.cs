using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float speedRotation;
    public Transform target;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        float horizon = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        Vector3 mov = new Vector3(horizon, 0, vertical) * speed;
        rb.velocity = new Vector3(mov.x, rb.velocity.y, mov.z);
    }
}
