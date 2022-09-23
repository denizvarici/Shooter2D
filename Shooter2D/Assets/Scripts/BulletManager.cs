using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{    
    private Rigidbody2D bulletRigidbody;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int bulletDamage;
    private void Start()
    {       
        bulletRigidbody = GetComponent<Rigidbody2D>();
        bulletRigidbody.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyManager>().TakeDamage(bulletDamage);
            Destroy(this.gameObject);
        }
    }
}
