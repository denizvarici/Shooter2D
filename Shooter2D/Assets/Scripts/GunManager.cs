using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GunManager : MonoBehaviour
{
    //For Shoot
    private Animator muzzleAnim;
    private Transform muzzleTransform;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletWay;
    [SerializeField] private Transform aimTransform;



    //Shoot Input
    InputManager inputManager;
    InputAction shootInput;


    //Weapon System
    [SerializeField] private WeaponSO weapon;
    [SerializeField] private WeaponSO[] weapons;
    

    //Weapon Setup
    private SpriteRenderer gunSpriteRenderer;
    private WeaponSO.WeaponShootType weaponShootType;
    
    private float nextFire;

    #region awake,onEnable,onDisable Methods
    private void Awake()
    {
        inputManager = new InputManager();
        muzzleAnim = GameObject.Find("MuzzleEffect").GetComponent<Animator>();
        muzzleTransform = GameObject.Find("MuzzleEffect").GetComponent<Transform>();

    }
    private void OnEnable()
    {
        shootInput = inputManager.Player.Shoot;
        shootInput.Enable();
    }
    private void OnDisable()
    {
        shootInput.Disable();
    }
    #endregion

    private void Start()
    {
        gunSpriteRenderer = GetComponent<SpriteRenderer>();
        WeaponSetup(0);
    }
    private void Update()
    {
        ShootSystem();
        ChangeWeapon();
    }


    
    void WeaponSetup(int index)
    {
        weapon = weapons[index];
        gunSpriteRenderer.sprite = weapon.weaponSprite;
        weaponShootType = weapon.weaponShootType;
        muzzleTransform.localPosition = new Vector2(weapon.muzzleAnim_X, weapon.muzzleAnim_Y);
        GameSystemManager.Instance.fireRate = weapon.fireRate;
        bulletWay.localPosition = new Vector2(weapon.bulletWay_X,weapon.bulletWay_Y);
        GameSystemManager.Instance.bulletSpeed = weapon.bulletSpeed;
        GameSystemManager.Instance.bulletDamage = weapon.bulletDamage;
        //bulletPrefab.GetComponent<BulletManager>().bulletSpeed = weapon.bulletSpeed;
        //bulletPrefab.GetComponent<BulletManager>().bulletDamage = weapon.bulletDamage;
    }

    

    void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            int i = weapon.weaponIndexNumber;
            i++;
            if (i > weapons.Length - 1)
            {
                i = 0;
                weapon = weapons[i];
                WeaponSetup(i);
            }
            else
            {
                weapon = weapons[i];
                WeaponSetup(i);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon = weapons[0];
            WeaponSetup(weapon.weaponIndexNumber);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon = weapons[1];
            WeaponSetup(weapon.weaponIndexNumber);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weapon = weapons[2];
            WeaponSetup(weapon.weaponIndexNumber);
        }else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            weapon = weapons[3];
            WeaponSetup(weapon.weaponIndexNumber);
        }
    }

    void ShootSystem()
    {
        if (weaponShootType == WeaponSO.WeaponShootType.Single && shootInput.WasPressedThisFrame() && Time.time > nextFire && GameSystemManager.Instance.canShoot == true)
        {
            ShootSingleAndAuto();
        }
        if (weaponShootType == WeaponSO.WeaponShootType.Auto && shootInput.IsPressed() && Time.time > nextFire && GameSystemManager.Instance.canShoot == true)
        {
            ShootSingleAndAuto();
        }
        if (weaponShootType == WeaponSO.WeaponShootType.Burst && shootInput.WasPressedThisFrame() && Time.time > nextFire && GameSystemManager.Instance.canShoot == true)
        {
            ShootBurst();
        }
    }

    void ShootSingleAndAuto()
    {
        nextFire = Time.time + GameSystemManager.Instance.fireRate;
        muzzleAnim.SetTrigger("Shoot");
        FindObjectOfType<AudioManager>().Play("Shoot");
        GameObject bulletObject = Instantiate(bulletPrefab, bulletWay.position, aimTransform.rotation);


        CameraShake.Instance.ShakeCamera(4f, .1f);
    }
    void ShootBurst()
    {
        nextFire = Time.time + GameSystemManager.Instance.fireRate;
        muzzleAnim.SetTrigger("Shoot");
        FindObjectOfType<AudioManager>().Play("Shoot");
        Instantiate(bulletPrefab, bulletWay.position,
            Quaternion.Euler(aimTransform.rotation.x, aimTransform.rotation.y, aimTransform.eulerAngles.z + 15f));
        Instantiate(bulletPrefab, bulletWay.position,
            Quaternion.Euler(aimTransform.rotation.x, aimTransform.rotation.y, aimTransform.eulerAngles.z));
        Instantiate(bulletPrefab, bulletWay.position,
            Quaternion.Euler(aimTransform.rotation.x, aimTransform.rotation.y, aimTransform.eulerAngles.z - 15f));
    }

    
}
