using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CreateWeapon",menuName ="ScriptableObjects/New Weapon")]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public Sprite weaponSprite;
    public WeaponShootType weaponShootType;
    public float muzzleAnim_X;
    public float muzzleAnim_Y;
    public float fireRate;
    public int weaponIndexNumber;
    public float bulletSpeed;
    public int bulletDamage;
    public float bulletWay_X;
    public float bulletWay_Y;



    public enum WeaponShootType
    {
        Single,
        Auto,
        Burst
    }
}
