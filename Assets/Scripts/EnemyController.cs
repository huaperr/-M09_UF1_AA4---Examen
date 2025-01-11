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

    private Rigidbody rb;
    private Rigidbody[] boneRigidbodies;

    private Animator animator;

    private Collider mainCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        boneRigidbodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        mainCollider = GetComponent<Collider>();
    }

    void Update()
    {
        LookPlayer();
        Move();
    }

    private void Move()
    {
        if (player)
        {
            float dist = Vector3.Distance(player.position, transform.position);
            Debug.Log(dist);

            

            animator.SetFloat("Velocity", Mathf.InverseLerp(0, 1, dist));
            if (dist > distance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            
        }
    }

    void LookPlayer()
    {

        Vector3 dir = player.position - transform.position;
        dir.y = 0;

        if (dir == Vector3.zero)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * speedRotation);
    }
    public void kill()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        if (animator != null)
        {
            animator.enabled = false;
        }
        if (mainCollider != null)
        {
            mainCollider.enabled = false;
        }
        if (boneRigidbodies != null && boneRigidbodies.Length > 0)
        {
            foreach (Rigidbody boneRb in boneRigidbodies)
            {
                boneRb.isKinematic = false;
            }
        }
        Rounds.instance.enemies.Remove(this.gameObject);
        Destroy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("bullet"))
        {
            kill();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Explosion"))
        {
            kill();
        }
    }
}
