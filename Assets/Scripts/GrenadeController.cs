using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrenadeController : MonoBehaviour
{
    Rigidbody rb;
    public LayerMask mask;
    public float launchForce;
    public float timer;
    public float countdown;
    private bool hasExploded;
    public float radius;
    public float explosionForce;
    public GameObject particles;

    EnemyController enemy;
    void Start()
    {
        countdown = timer;
    }

    private void Update()
    {
        LaunchGrenade();
        countdown -= Time.deltaTime;

        if (countdown < 0 && !hasExploded) 
        {
            Explosion();
        }
    }
    void LaunchGrenade()
    {
        if (rb != null)
        {
            Vector3 launchDirection = transform.forward;

            rb.AddForce(launchDirection * launchForce, ForceMode.Impulse);
        }
    }
    void Explosion()
    {
        GameObject spawnP = Instantiate(particles, transform.position, transform.rotation);
        Destroy(spawnP, 1);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Player"))
            {
                continue;
            }
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius);
                
            }

        }
        hasExploded = true;
        Destroy(gameObject);
    }
}
