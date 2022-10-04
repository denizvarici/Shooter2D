using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CreateUpgrade",menuName ="ScriptableObjects/New Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public float fireRate;
    public float bulletSpeed;
    public float bulletDamage;
    public float enemyHealth;
    public float playerSpeed;
    public float enemySpeed;
    public float xpChance;
    public float enemySpawnTime;
}
