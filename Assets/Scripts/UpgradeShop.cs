using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;




public class UpgradeShop : MonoBehaviour
{
    public static UpgradeShop up;
    public Text lifeText, damageText, fireRateText, bulletsText, reloadTimeText, upgradeCostText,damageBombText,boxLifeText, bombCostText;


    GameManager gameManager;
    HeroControl player;





    void Start()
    {
        /*
        if (File.Exists(gameManager.filePath))
        {
            gameManager.Load();
        }
        */
        //gameManager.Load();
        gameManager = GameManager.inst;
        player = FindObjectOfType<HeroControl>();
        UpdateUI();
    }

     public void UpdateUI()
    {
        //playerinfo.GameStartScoreM();
        lifeText.text = "Life: " + gameManager.life;
        damageText.text = "Damage: " + gameManager.damage;
        fireRateText.text = "Fire Rate: " + gameManager.fireRate;
        bulletsText.text = "Bullets: " + gameManager.bullets;
        reloadTimeText.text = "Reload Time: " + gameManager.reloadTime;
        damageBombText.text = "Damage Bomb: " + gameManager.damageBomb;
        boxLifeText.text = "Box Life: " + gameManager.boxLife;
        upgradeCostText.text = "Upgrade Cost: " + gameManager.upgradeCost;
        bombCostText.text = "Buy Bomb: " + gameManager.bombsCost;
        /*
        lifeText.text = "Life: " + gameManager.GetLife().ToString();
        damageText.text = "Damage: " + gameManager.GetDamage().ToString();
        fireRateText.text = "Fire Rate: " + gameManager.GetFireRate().ToString();
        bulletsText.text = "Bullets: " + gameManager.GetBullets().ToString(); ;
        reloadTimeText.text = "Reload Time: " + gameManager.GetReloadTime().ToString(); ;
        damageBombText.text = "Damage Bomb: " + gameManager.GetDamageBomb().ToString(); ;
        boxLifeText.text = "Box Life: " + gameManager.GetBoxLife().ToString(); ;
        upgradeCostText.text = "Upgrade Cost: " + gameManager.GetSetUpgradeCost().ToString(); ;
        bombCostText.text = "Buy Bomb: " + gameManager.GetBombs().ToString(); ;
        */
    }

    public void SetBoxLife()
    {
        if (gameManager.coins >= gameManager.upgradeCost && gameManager.boxLife < 20)
        {
            AudioManager.instance.SonsFXToca(0);
            gameManager.boxLife++;
            FindObjectOfType<UIManager>().UpdateLifeBar();
            player.SetPlayerStatus();
            SetCoins(gameManager.upgradeCost);
            gameManager.upgradeCost += (gameManager.upgradeCost / 5);
            UpdateUI();

        }
    }
    public void SetBoxBomb()
    {
        if (gameManager.coins >= gameManager.bombsCost)
        {
            AudioManager.instance.SonsFXToca(0);
            gameManager.bombs++;

            player.SetPlayerStatus();
            SetCoins(gameManager.bombsCost);
            UpdateUI();
            FindObjectOfType<UIManager>().UpdateBombsUI(gameManager.bombs);

        }
    }

    public void SetLife()
    {
        if (gameManager.coins >= gameManager.upgradeCost && gameManager.life < 50)
        {
            AudioManager.instance.SonsFXToca(0);
            gameManager.life++;
            FindObjectOfType<UIManager>().UpdateLifeBar();
            player.SetPlayerStatus();
            SetCoins(gameManager.upgradeCost);
            gameManager.upgradeCost += (gameManager.upgradeCost / 5);
            UpdateUI();

        }
    }

    public void SetDamage()
    {
        if (gameManager.coins >= gameManager.upgradeCost && gameManager.damage < 20)
        {
            AudioManager.instance.SonsFXToca(0);
            gameManager.damage++;           
            player.SetPlayerStatus();
            SetCoins(gameManager.upgradeCost);
            gameManager.upgradeCost += (gameManager.upgradeCost / 5);
            UpdateUI();

        }
    }

    public void SetDamageBomb()
    {
        if (gameManager.coins >= gameManager.upgradeCost && gameManager.damageBomb < 50)
        {
            AudioManager.instance.SonsFXToca(0);
            gameManager.damageBomb += 5;
            
            player.SetPlayerStatus();
            SetCoins(gameManager.upgradeCost);
            gameManager.upgradeCost += (gameManager.upgradeCost / 5);
            UpdateUI();

        }
    }
    public void SetFireRate()
    {
        if (gameManager.coins >= gameManager.upgradeCost)
        {
            AudioManager.instance.SonsFXToca(0);

            if (gameManager.fireRate <= 0)
            {
                gameManager.fireRate = 0;
                
            }
            else if(gameManager.fireRate >= 0)
            {
                gameManager.fireRate -= 0.1f;
                
                gameManager.upgradeCost += (gameManager.upgradeCost / 5);
                player.SetPlayerStatus();
                SetCoins(gameManager.upgradeCost);

            }
            UpdateUI();

        }
    }

    public void SetBullets()
    {
        if (gameManager.coins >= gameManager.upgradeCost)
        {
            AudioManager.instance.SonsFXToca(0);
            gameManager.bullets++;
            
            player.SetPlayerStatus();
            SetCoins(gameManager.upgradeCost);
            gameManager.upgradeCost += (gameManager.upgradeCost / 5);
            UpdateUI();

        }
    }

    public void SetReloadTime()
    {
        if (gameManager.coins >= gameManager.upgradeCost)
        {
            AudioManager.instance.SonsFXToca(0);

            if (gameManager.reloadTime <= 0.1f)
            {
                gameManager.reloadTime = 0.1f;

            }else if(gameManager.reloadTime >= 0)
            {
                gameManager.reloadTime -= 0.1f;
                
                player.SetPlayerStatus();
                SetCoins(gameManager.upgradeCost);
                gameManager.upgradeCost += (gameManager.upgradeCost / 5);

            }

            UpdateUI();
        }
    }

    void SetCoins(int coin)
    {
        gameManager.coins -= coin;
        FindObjectOfType<UIManager>().UpdateCoins();
    }
    
}
