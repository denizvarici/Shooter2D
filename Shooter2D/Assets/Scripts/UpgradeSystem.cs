using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{
    public static UpgradeSystem Instance;
    [SerializeField] private List<UpgradeSO> upgrades;
    private int excludeIndex;
    private int chooseIndex;

    private UpgradeSO currentUpgrade;
    [SerializeField] private Text upgradeText;
    [SerializeField] private GameObject upgradePanel;

    [SerializeField] private TextMeshProUGUI firstUpgradeText;
    //[SerializeField] private TextMeshProUGUI firstUpgradeText;
    [SerializeField] private UpgradeSO firstUpgrade;
    [SerializeField] private TextMeshProUGUI secondUpgradeText;
    [SerializeField] private UpgradeSO secondUpgrade;

    [SerializeField] Button firstUpgradeButton;
    [SerializeField] Button secondUpgradeButton;

    [SerializeField] private int maxXpAmount;
    public XPBarManager xpBarManager;



    public void Start()
    {
        Instance = this;
        //upgrades = GameSystemManager.Instance.allUpgrades;
        upgrades = GameSystemManager.Instance.testUpgrades;
        excludeIndex = -1;
        chooseIndex = -1;
        xpBarManager.SetMaxXP(maxXpAmount);
    }



    private void Update()
    {
        if (GameSystemManager.Instance.xpAmount >= maxXpAmount)
        {
            StartCoroutine(ShowUpgradePanel());
            maxXpAmount += 10;
            GameSystemManager.Instance.XPChance += 3;
            GameSystemManager.Instance.enemySpawnTime -= 0.5f;
            GameSystemManager.Instance.enemySpeed += 0.2f;
            GameSystemManager.Instance.enemyHealth += 0.5f;
            xpBarManager.SetMaxXP(maxXpAmount);
            
        }
    }

    IEnumerator ShowUpgradePanel()
    {
        GameSystemManager.Instance.xpAmount = 0;
        ChooseRandomUpgrade(1);
        GetUpgrades(firstUpgradeText);
        ChooseRandomUpgrade(2);
        GetUpgrades(secondUpgradeText);        
        GameSystemManager.Instance.PauseGame();
        GameSystemManager.Instance.isUpgrading = true;
        yield return new WaitForSecondsRealtime(1.5f);       
        upgradePanel.SetActive(true);
    }

    

    void ChooseRandomUpgrade(int numberOfUpgrade)
    {
        //chooseIndex = UnityEngine.Random.Range(0, upgrades.Count());

        while (chooseIndex == excludeIndex)
        {
            chooseIndex = UnityEngine.Random.Range(0, upgrades.Count());
        }
        currentUpgrade = upgrades[chooseIndex];
        if (numberOfUpgrade == 1)
        {
            firstUpgrade = currentUpgrade;
        }
        if (numberOfUpgrade == 2)
        {
            secondUpgrade = currentUpgrade;
        }
        excludeIndex = chooseIndex;
        


        //upgrades.RemoveAt(i);       
    }


    void GetUpgrades(TextMeshProUGUI currentText)
    {
        if (currentUpgrade.fireRate != 0)
        {
            if (currentUpgrade.fireRate > 0)
            {
                currentText.text += "\nFire rate : +" + currentUpgrade.fireRate;
            }
            else
            {
                currentText.text += "\nFire rate : " + currentUpgrade.fireRate;
            }
            //upgradeText.text += "Fire rate : " + currentUpgrade.fireRate + "\n";
        }
        if (currentUpgrade.bulletSpeed != 0)
        {
            if (currentUpgrade.bulletSpeed > 0)
            {
                currentText.text += "\nBullet speed : +" + currentUpgrade.bulletSpeed;
            }
            else
            {
                currentText.text += "\nBullet speed : " + currentUpgrade.bulletSpeed;
            }
            //upgradeText.text += "Bullet speed : " + currentUpgrade.bulletSpeed + "\n";
        }
        if (currentUpgrade.bulletDamage != 0)
        {
            if (currentUpgrade.bulletDamage > 0)
            {
                currentText.text += "\nBullet damage : +" + currentUpgrade.bulletDamage;
            }
            else
            {
                currentText.text += "\nBullet damage : " + currentUpgrade.bulletDamage;
            }
            //upgradeText.text += "Bullet damage : " + currentUpgrade.bulletDamage + "\n";
        }
        if (currentUpgrade.enemyHealth != 0)
        {
            if (currentUpgrade.enemyHealth > 0)
            {
                currentText.text += "\nEnemy health : +" + currentUpgrade.enemyHealth;
            }
            else
            {
                currentText.text += "\nEnemy health : " + currentUpgrade.enemyHealth;
            }
            //upgradeText.text += "Enemy health : " + currentUpgrade.enemyHealth + "\n";
        }
        if (currentUpgrade.playerSpeed != 0)
        {
            if (currentUpgrade.playerSpeed > 0)
            {
                currentText.text += "\nPlayer speed : +" + currentUpgrade.playerSpeed;
            }
            else
            {
                currentText.text += "\nPlayer speed : " + currentUpgrade.playerSpeed;
            }
            //upgradeText.text += "Player speed : " + currentUpgrade.playerSpeed + "\n";
        }
        if (currentUpgrade.enemySpeed != 0)
        {
            if (currentUpgrade.enemySpeed > 0)
            {
                currentText.text += "\nEnemy speed : +" + currentUpgrade.enemySpeed;
            }
            else
            {
                currentText.text += "\nEnemy speed : " + currentUpgrade.enemySpeed;
            }
            //upgradeText.text += "Enemy speed : " + currentUpgrade.enemySpeed + "\n";
        }
        if (currentUpgrade.enemySpawnTime != 0)
        {
            if (currentUpgrade.enemySpawnTime > 0)
            {
                currentText.text += "\nEnemy Spawn Time : +" + currentUpgrade.enemySpawnTime;
            }
            else
            {
                currentText.text += "\nEnemy Spawn Time : " + currentUpgrade.enemySpawnTime;
            }
        }
        if (currentUpgrade.xpChance != 0)
        {
            if (currentUpgrade.xpChance > 0)
            {
                currentText.text += "\nXP Chance : +" + currentUpgrade.xpChance; 
            }
            else
            {
                currentText.text += "\nXP Chance : " + currentUpgrade.xpChance;
            }
        }
    }

    public void InstantiateUpgrade(Button button)
    {
        if (button.name == "FirstUpgradeButton")
        {           
            ApplyUpgrades(firstUpgrade);
        }

        if (button.name == "SecondUpgradeButton")
        {            
            ApplyUpgrades(secondUpgrade);
        }
    }

    void ApplyUpgrades(UpgradeSO whichUpgrade)
    {
        GameSystemManager.Instance.fireRate += whichUpgrade.fireRate;
        GameSystemManager.Instance.XPChance += whichUpgrade.xpChance;
        GameSystemManager.Instance.bulletDamage += whichUpgrade.bulletDamage;
        GameSystemManager.Instance.bulletSpeed += whichUpgrade.bulletSpeed;
        GameSystemManager.Instance.enemyHealth += whichUpgrade.enemyHealth;
        GameSystemManager.Instance.enemySpeed += whichUpgrade.enemySpeed;
        GameSystemManager.Instance.playerSpeed += whichUpgrade.playerSpeed;
        GameSystemManager.Instance.enemySpawnTime += whichUpgrade.enemySpawnTime;
        firstUpgradeText.text = "";
        secondUpgradeText.text = "";
        excludeIndex = -1;
        upgradePanel.SetActive(false);
        xpBarManager.SetXP(GameSystemManager.Instance.xpAmount);
        GameSystemManager.Instance.ResumeGame();
        GameSystemManager.Instance.isUpgrading = false;
        
    }

    

}
