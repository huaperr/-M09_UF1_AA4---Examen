using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGun : MonoBehaviour
{
    public LineRenderer line;
    public float lineFadeSpeed;
    public LayerMask mask;
    public float knockbackForce;
    void Update()
    {
        line.startColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, line.startColor.a - Time.deltaTime * lineFadeSpeed);
        line.endColor = new Color(line.endColor.r, line.endColor.g, line.endColor.b, line.endColor.a - Time.deltaTime * lineFadeSpeed);

        if (Input.GetButtonDown("Fire1"))
        {
            line.startColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, 1);
            line.endColor = new Color(line.endColor.r, line.endColor.g, line.endColor.b, 1);

            line.SetPosition(0, transform.position);

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
            {
                line.SetPosition(1, hit.point);

                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();

                if (hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<EnemyController>().kill();
                }
                if (rb != null)
                {
                    rb.AddForceAtPosition(transform.forward * knockbackForce, hit.point);

                }
            }
            else
            {
                line.SetPosition(1, transform.position + transform.forward * 1000);
            }
        }
    }
}
