using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/*
 public class PlayerData
{

    public int life;
    public int coin;
    public float reloadTime;
    public int bullets;
    public int bombs;
    public int upgradeCost;
    public float fireRate;
    public int damage, damageBomb, boxLife;


}
*/
public class GameControl : MonoBehaviour
{
    public static GameControl inst;
    GameManager gameManager;
    private int life;
    private int coin;   
    private int bullets;
    private int bombs;
    private int upgradeCost;
    private int damage, damageBomb, boxLife;
    private float fireRate;
    private float reloadTime;




    /*
    public void Start()
    {
        life = gameManager.life;
        coin = gameManager.coins;
        reloadTime = gameManager.reloadTime;
        bullets = gameManager.bullets;
        bombs = gameManager.bombs;
        upgradeCost = gameManager.upgradeCost;
        fireRate = gameManager.fireRate;
        damage = gameManager.damage;
        damageBomb = gameManager.damageBomb;
        boxLife = gameManager.boxLife;
    }
    */


    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        //filePath = Application.persistentDataPath + "/playerInfo.dat";
    }

    public void Save(int coin)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        /*
        data.life = gameManager.life;
        data.coin = coin;
        data.bullets = bullets;
        data.bombs = bombs;
        data.upgradeCost = upgradeCost;
        data.damage = damage;
        data.damageBomb = damageBomb;
        data.boxLife = boxLife;
        data.fireRate = fireRate;
        data.reloadTime = reloadTime;

        bf.Serialize(file, data);

        file.Close();
        */
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            /*
            gameManager.life = data.life;
            coin = data.coin;
            bullets = data.bullets;
            bombs = data.bombs;
            upgradeCost = data.upgradeCost;
            damage = data.upgradeCost;
            damageBomb = data.damageBomb;
            boxLife = data.boxLife;
            fireRate = data.fireRate;
            reloadTime = data.reloadTime;
            */
        }
    }

    public void SetLife()
    {
        life += gameManager.life++;       
    }

    public int GetLife()
    {
        return life;
    }

    public void SetCoin(int value)
    {
        coin += value;
    }

    public int GetCoin()
    {
        return coin;
    }
    public void SetBullets(int value)
    {
        bullets += value;
    }

    public int GetBullets()
    {
        return bullets;
    }
    public void SetBombs(int value)
    {
        bombs += value;
    }

    public int GetBombs()
    {
        return bombs;
    }
    public void SetUpgradeCost(int value)
    {
        upgradeCost += value;
    }

    public int GetSetUpgradeCost()
    {
        return upgradeCost;
    }
    public void SetDamage(int value)
    {
        damage += value;
    }

    public int GetDamage()
    {
        return damage;
    }
    public void SetDamageBomb(int value)
    {
        damageBomb += value;
    }

    public int GetDamageBomb()
    {
        return damageBomb;
    }
    public void SetBoxLife(int value)
    {
        boxLife += value;
    }

    public int GetBoxLife()
    {
        return boxLife;
    }
    public void SetFireRate(float value)
    {
        fireRate += value;
    }

    public float GetFireRate()
    {
        return fireRate;
    }
    public void SetReloadTime(float value)
    {
        reloadTime += value;
    }

    public float GetReloadTime()
    {
        return reloadTime;
    }
}
