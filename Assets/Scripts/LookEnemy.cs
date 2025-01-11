using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookEnemy : MonoBehaviour
{

    public float radius;
    public float speedRotation;
    public LayerMask mask;

    public GameObject currentTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookE();

    }

    void LookE()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, mask);
        if(colliders.Length == 0)
        {
            currentTarget.transform.position = transform.position;
            return;
        }
        float dist = Vector3.Distance(transform.position, colliders[0].transform.position);
        Collider coll = colliders[0];
        for (int i = 0; i < colliders.Length; i++)
        {
            if (Vector3.Distance(transform.position, colliders[i].transform.position) < dist)
            {
                dist = Vector3.Distance(transform.position, colliders[i].transform.position);
                coll = colliders[i];
            }
        }

        Vector3 dir = coll.transform.position - transform.position;
        dir.y = 0;

        if (dir == Vector3.zero)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * speedRotation);

        currentTarget.transform.position = coll.transform.position;
    }
}
