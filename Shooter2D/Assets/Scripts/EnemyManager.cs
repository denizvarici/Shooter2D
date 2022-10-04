using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //Health System
    
    [SerializeField] private float currentHealth;

    //Flash and destroy effect after hit
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float flashDuration;
    [SerializeField] private GameObject destroyEffectPrefab;


    //TARGET AND MOVE TO HIT
    [SerializeField] private GameObject playerObject;
    private Rigidbody2D enemyRigidbody;
    
    [SerializeField] private GameObject targetDetectorObject;

    //XP SYSTEM
    [SerializeField] private GameObject xpPrefab;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = GameSystemManager.Instance.enemyHealth;
        playerObject = GameObject.FindWithTag("Player");
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        TargetAndMove();
    }
    private void Update()
    {
        if (GameSystemManager.Instance.isUpgrading)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
            CreateRandomXP();
            FindObjectOfType<AudioManager>().Play("Die");
            Destroy(this.gameObject);
        }
        StartCoroutine(Flash());
    }

    public void TargetAndMove()
    {
        if (playerObject != null)
        {
            var playerTransform = playerObject.transform;
            var enemyTransform = targetDetectorObject.transform;

            var direction = (playerTransform.position - enemyTransform.position).normalized;
            enemyTransform.right = Vector3.Slerp(enemyTransform.right, direction, 1f);

            enemyRigidbody.velocity = enemyTransform.right * GameSystemManager.Instance.enemySpeed;
        }
    }

    IEnumerator Flash()
    {
        spriteRenderer.material.SetInt("_hit", 1);
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.material.SetInt("_hit", 0);
    }

    void CreateRandomXP()
    {
        var randomXPAmount = Random.Range(1, GameSystemManager.Instance.XPChance);
        
        while (randomXPAmount > 0)
        {
            var a = Random.Range(-1f, 1f);
            Instantiate(xpPrefab, new Vector2(transform.position.x + a, transform.position.y + a), Quaternion.identity);
            randomXPAmount--;
        }
    }

    

}
