using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{

    /*
      GAME COLOR CAN BE GREEN GREY BLUE BLACK_WHITE
     Create Weapon System with scriptable objects
     Shop System Currency System (Maybe Unity Registry with Econmy?)
     Create an Enemy Manager
     Change mouse cursor      
     
        GAME WILL BE PLAYED AGAINST TIME
        BULLET SPEED
        GUN SHOP
        ARMOR BUYING
        DASH ABILITY
        MOVEMENT SPEED
        WILL BE ADDED SOON
    */
    //INPUT SYSTEM
    InputManager inputManager;
    InputAction moveInput;
    InputAction shootInput;

    //COMPONENTS
    private Rigidbody2D playerRB;
    private Animator playerAnimator;


    //FEATURES
    [SerializeField] private float playerSpeed;
    private Vector2 moveDirection;

    //AIM and SHOOT
    private Transform aimTransform;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField]private Transform bulletWay;
    [SerializeField] private float bulletSpeed;

    //MuzzleEffect
    private Animator muzzleAnim;


    #region Awake,OnEnable,Disable
    private void Awake()
    {
        inputManager = new InputManager();
        aimTransform = transform.Find("Aim");
        muzzleAnim = GameObject.Find("MuzzleEffect").GetComponent<Animator>();
        //bulletWay = transform.Find("BulletWay");
    }
    private void OnEnable()
    {
        moveInput = inputManager.Player.Move;
        moveInput.Enable();
        shootInput = inputManager.Player.Shoot;
        shootInput.Enable();
    }
    private void OnDisable()
    {
        moveInput.Disable();
        shootInput.Disable();
    }
    #endregion


    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        Aim();
        if (shootInput.WasPressedThisFrame())
        {
            Debug.Log("Ateþ edildi!");
            muzzleAnim.SetTrigger("Shoot");
            GameObject bulletObject = Instantiate(bulletPrefab, bulletWay.position, aimTransform.rotation);
            //bulletObject.GetComponent<Rigidbody2D>().AddForce(bulletWay.right * bulletSpeed,ForceMode2D.Impulse);
            CameraShake.Instance.ShakeCamera(1f, .1f);
            
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //Read move Input Update
        moveDirection = moveInput.ReadValue<Vector2>().normalized;
        //Animation
        playerAnimator.SetFloat("Horizontal", moveDirection.x);
        playerAnimator.SetFloat("Vertical", moveDirection.y);
        playerAnimator.SetFloat("Speed", moveDirection.sqrMagnitude);
        //Move 8Way FixedUpdate
        playerRB.velocity = moveDirection * playerSpeed;
    }

    void Aim()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.rotation = Quaternion.Euler(0, 0, angle);

        Vector3 tempScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            tempScale.y = -1f;
        }
        else
        {
            tempScale.y = +1f;
        }
        aimTransform.localScale = tempScale;
        
    }

    
}
