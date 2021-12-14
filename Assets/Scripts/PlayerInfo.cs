using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo playerinfo;
    GameManager gamemanager;

    public int life;
    public int coin;
    public float reloadTime;
    public int bullets;
    public int bombs;
    public int upgradeCost;
    public float fireRate;
    public int damage, damageBomb, boxLife;

    private void Awake()
    {
        ZPlayerPrefs.Initialize("123456", "zombieworld");
        if (playerinfo == null)
        {
            playerinfo = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
        void Start()
    {
        /*
        gamemanager.life = life;
        gamemanager.coins = coin;
        gamemanager.reloadTime = reloadTime;
        gamemanager.bullets = bullets;
        gamemanager.bombs = bombs;
        gamemanager.upgradeCost = upgradeCost;
        gamemanager.fireRate = fireRate;
        gamemanager.damage = damage;
        gamemanager.damageBomb = damageBomb;
        gamemanager.boxLife = boxLife;
        */
    }

    public void GameStartScoreM()
    {
        //Coins
        if (ZPlayerPrefs.HasKey("coinsSave"))
        {
            coin = ZPlayerPrefs.GetInt("coinsSave");
        }
        else
        {
            coin = gamemanager.coins;
            ZPlayerPrefs.SetInt("coinsSave", coin);
        }

        //Life
        if (ZPlayerPrefs.HasKey("lifeSave"))
        {
            life = ZPlayerPrefs.GetInt("lifeSave");
        }
        else
        {
            life = gamemanager.life;
            ZPlayerPrefs.SetInt("lifeSave", life);
        }

        //ReloadTime
        if (ZPlayerPrefs.HasKey("reloadTimeSave"))
        {
            reloadTime = ZPlayerPrefs.GetFloat("reloadTimeSave");
        }
        else
        {
            reloadTime = gamemanager.reloadTime;
            ZPlayerPrefs.SetFloat("reloadTimeSave", reloadTime);
        }

        //Bullets
        if (ZPlayerPrefs.HasKey("bulletsSave"))
        {
            bullets = ZPlayerPrefs.GetInt("bulletsSave");
        }
        else
        {
            bullets = gamemanager.bullets;
            ZPlayerPrefs.SetInt("bulletsSave", bullets);
        }

        //Bombs
        if (ZPlayerPrefs.HasKey("bombsSave"))
        {
            bombs = ZPlayerPrefs.GetInt("bombsSave");
        }
        else
        {
            bombs = gamemanager.bombs;
            ZPlayerPrefs.SetInt("bombsSave", bombs);
        }

        //UpgradeCost
        if (ZPlayerPrefs.HasKey("upgradeCostSave"))
        {
            upgradeCost = ZPlayerPrefs.GetInt("upgradeCostSave");
        }
        else
        {
            upgradeCost = gamemanager.upgradeCost;
            ZPlayerPrefs.SetInt("upgradeCostSave", upgradeCost);
        }

        //FireRate
        if (ZPlayerPrefs.HasKey("fireRateSave"))
        {
            fireRate = ZPlayerPrefs.GetFloat("fireRateSave");
        }
        else
        {
            fireRate = gamemanager.fireRate;
            ZPlayerPrefs.SetFloat("fireRateSave", fireRate);
        }

        //Damage
        if (ZPlayerPrefs.HasKey("damageSave"))
        {
            damage = ZPlayerPrefs.GetInt("damageSave");
        }
        else
        {
            damage = gamemanager.damage;
            ZPlayerPrefs.SetInt("damageSave", damage);
        }

        //DamageBombs
        if (ZPlayerPrefs.HasKey("damageBombsSave"))
        {
            damageBomb = ZPlayerPrefs.GetInt("damageBombsSave");
        }
        else
        {
            damageBomb = gamemanager.damageBomb;
            ZPlayerPrefs.SetInt("damageBombsSave", damageBomb);
        }

        //BoxLife
        if (ZPlayerPrefs.HasKey("boxLifeSave"))
        {
            boxLife = ZPlayerPrefs.GetInt("boxLifeSave");
        }
        else
        {
            boxLife = gamemanager.boxLife;
            ZPlayerPrefs.SetInt("boxLifeSave", boxLife);
        }
    }
    public void UpdateScore()
    {
        life = ZPlayerPrefs.GetInt("lifeSave");
        coin = ZPlayerPrefs.GetInt("coinsSave");
        reloadTime = ZPlayerPrefs.GetInt("reloadTimeSave");
        bullets = ZPlayerPrefs.GetInt("bulletsSave");
        bombs = ZPlayerPrefs.GetInt("bombsSave");
        upgradeCost = ZPlayerPrefs.GetInt("upgradeCostSave");
        fireRate = ZPlayerPrefs.GetInt("fireRateSave");
        damage = ZPlayerPrefs.GetInt("damageSave");
        damageBomb = ZPlayerPrefs.GetInt("damageBombsSave");
        boxLife = ZPlayerPrefs.GetInt("boxLifeSave");
    }
    public void ColetaMoedas(int moedas)
    {
        coin += moedas;
        SalvaMoedas(coin);
    }

    public void PerdeMoedas(int moedas)
    {
        coin -= moedas;
        SalvaMoedas(coin);
    }

    public void SalvaMoedas(int moedas)
    {
        ZPlayerPrefs.SetInt("coinsSave", moedas);
    }

}
