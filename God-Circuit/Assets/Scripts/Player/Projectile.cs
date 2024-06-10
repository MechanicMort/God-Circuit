using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float damage;
    public float speed;
    private Rigidbody rb;
    public bool destroyOnContact;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<IEnemyModule>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        print("Collision with" + collision.transform.name);
        //LIKE DO STUFF AND LIKE EFFECTS KABOOOM
        if (destroyOnContact)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
            if (other.transform.tag == "Enemy")
            {
            other.gameObject.GetComponent<IEnemyModule>().TakeDamage(damage);
                Destroy(this.gameObject);
            }
            print("Collision with" + other.transform.name);
        //LIKE DO STUFF AND LIKE EFFECTS KABOOOM
        if (destroyOnContact)
        {
            Destroy(this.gameObject);
        }

        }

    public void FixedUpdate()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(transform.forward * speed);

        rb.AddForce(Physics.gravity);
    }
}
