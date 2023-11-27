using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPart : MonoBehaviour
{
    public Enemy enemy;
    public float multiply = 1;
    private void OnCollisionEnter(Collision collision)
    {
        if (enemy.vida<=0) {
            return;
        }
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null) {
            foreach (string tagsOmit in enemy.tagEnemy) {
                if (rb.gameObject.tag.CompareTo(tagsOmit) == 0) {
                    return;
                }
            }
            if (!rb.IsSleeping()) {
                Damange bul = collision.gameObject.GetComponent<Damange>();
                float variation = 1;
                if (bul != null) {
                    variation = bul.dano;
                }
                enemy.TakeDano(rb.mass* rb.velocity.magnitude* variation*multiply, gameObject.name);
                
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (enemy.vida <= 0)
        {
            return;
        }
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            foreach (string tagsOmit in enemy.tagEnemy)
            {
                if (rb.gameObject.tag.CompareTo(tagsOmit) == 0)
                {
                    return;
                }
            }
            if (!rb.IsSleeping())
            {
                Damange damange = other.gameObject.GetComponent<Damange>();
                float variation = 1;
                if (damange != null)
                {
                    variation = damange.dano;
                }
                enemy.TakeDano(rb.mass * rb.velocity.magnitude * variation * multiply, gameObject.name);

            }
        }
    }


}
