using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrenadeController : MonoBehaviour
{
    Rigidbody rb;
    public LayerMask mask;
    public float launchForce;
    public float launchRotation;
    public float timer;
    public float countdown;
    private bool hasExploded;
    public float radius;
    public float explosionForce;
    public GameObject particles;

    public Transform player;
    void Start()
    {
        countdown = timer;
        rb = GetComponent<Rigidbody>();
        LaunchGrenade();
    }

    private void Update()
    {
        
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

            rb.AddForce((3 * player.forward + player.up) * launchForce, ForceMode.Impulse);
            rb.AddTorque(Random.Range(1, 10) * player.forward * launchRotation);
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
            if (nearbyObject.CompareTag("Enemy"))
            {
                nearbyObject.GetComponent<EnemyController>().kill();
            }
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius);
                
            }

        }
        hasExploded = true;
        Destroy(gameObject);
    }
}
