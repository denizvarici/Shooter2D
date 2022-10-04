using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemManager : MonoBehaviour
{

    
    public static GameSystemManager Instance;



    [SerializeField] public int score;
    [SerializeField] public int highScore;


    //GAME FEATURES
    [SerializeField] public int xpAmount;
    [SerializeField] public float playerSpeed;
    [SerializeField] public float enemySpeed;
    [SerializeField] public float enemyHealth;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float bulletDamage;
    [SerializeField] public float XPChance;
    [SerializeField] public float fireRate;
    [SerializeField] public float enemySpawnTime;
    [SerializeField] public List<UpgradeSO> allUpgrades;
    [SerializeField] public bool canShoot;
    [SerializeField] public bool canSpawn;


    //TEST
    [SerializeField] public List<UpgradeSO> testUpgrades;



    public XPBarManager xpBarManager;

    [SerializeField] public bool isUpgrading;
    


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ESCPanel.SetActive(false);
        highScore = PlayerPrefs.GetInt("highScore", highScore);
        isUpgrading = false;
        score = 0;
        PlayerPrefs.SetInt("score", score);
        canShoot = true;
        canSpawn = true;

        //enemySpawnTime = 5f;
        //playerSpeed = 5;
        //enemySpeed = 0.5f;
        //enemyHealth = 12;
        //XPChance = 10;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ESCPauseGame();           
        }
    }


    public void IncreaseXP(int increaseAmount)
    {
        xpAmount += increaseAmount;
        xpBarManager.SetXP(xpAmount);
    }

    [SerializeField] private GameObject ESCPanel;
    public void ESCPauseGame()
    {
        if (isUpgrading == false)
        {
            ESCPanel.SetActive(true);
            PauseGame();
        }       
    }
    public void ESCResumeGame()
    {
        if (isUpgrading == false)
        {
            ESCPanel.SetActive(false);
            ResumeGame();
        }
        
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }


}
