using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    

    private Rigidbody2D bulletRigidbody;
    

    private void Awake()
    {
        
    }
    private void Start()
    {       
        bulletRigidbody = GetComponent<Rigidbody2D>();
        bulletRigidbody.AddForce(transform.right * GameSystemManager.Instance.bulletSpeed, ForceMode2D.Impulse);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyManager>().TakeDamage(GameSystemManager.Instance.bulletDamage);
            Destroy(this.gameObject);
        }
        if (collision.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
