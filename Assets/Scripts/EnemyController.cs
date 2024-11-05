using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    private float distance = 3;
    public float speedRotation;
    public float stoppingDistance;

    void Update()
    {
        LookPlayer();
        Move();
    }

    void LookPlayer()
    {
        
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, speedRotation * Time.deltaTime);
    }

    private void Move()
    {
        if (player)
        {
            float dist = Vector3.Distance(player.position, transform.position);
            print("Distance to other: " + dist);

            if(dist > 3) 
            {
                transform.position = Vector3.MoveTowards(player.transform.position, transform.position, speed);
            }
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
