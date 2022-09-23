using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private float flashDuration;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //Destroy(this.gameObject);
        }
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        spriteRenderer.material.SetInt("_hit", 1);
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.material.SetInt("_hit", 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

}
