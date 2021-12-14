using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Fungus;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
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
public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    public bool gameOver;
    public GameObject heroi, veiculo,gameOverPainel;
    //Recarregar Arma
    public float reloadTime = 1f;
    public int coins;
    public int bullets = 6;
    public int bombs = 2;
    public int bombsCost = 50;
    public int upgradeCost = 20;
    public float fireRate = 1f;
    public int damage = 1,damageBomb = 5,boxLife = 5;
    public int life = 3;
    [SerializeField]
    private Transform boss, merchant;
    [SerializeField]
    private Animator barCima, barBaixo;
    private Image joyImg;
    public Flowchart fungus;
    public GameObject[] uiElementos;
    public int ExeUmaVez = 0;
    private WaitForSeconds fungosTime;
    private WaitForSeconds t;
    private Button denovo;
    public GameObject container1, container2;
    private Rigidbody2D heroiRb, veiculoRb;
    HeroControl player;
    public String filePath;

    enum character
    {
        personagem = 0,
        tanque = 1
    };

    public int personagem;

    enum Estado
    {
        jogo = 0,
        filme =1
    };

    public int gameEstado;

    private void Awake()
    {
        
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Carrega;
        filePath = Application.persistentDataPath + "/playerInfo.data";
    }
    void Carrega(Scene cena, LoadSceneMode modo)
    {
        if (OndeEstou.instance.fase >= 2)
        {
            PegaDados();
        }
        
    }


    void PegaDados()
    {
        container1 = GameObject.FindWithTag("objs");
        container2 = GameObject.FindWithTag("uicena");

        //Objetos de cena
        heroi = container1.transform.GetChild(2).gameObject;
        //veiculo = container1.transform.GetChild(1).gameObject;
        boss = container1.transform.GetChild(1);
        merchant = container1.transform.GetChild(3);
        fungus = container1.transform.GetChild(0).GetComponent<Flowchart>();
        //barreira = container1.transform.GetChild(4).gameObject;
        //UI
        //txtTempo = container2.transform.GetChild(7).GetComponent<Text>();
        joyImg = container2.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        uiElementos[0] = container2.transform.GetChild(0).gameObject;
        uiElementos[1] = container2.transform.GetChild(1).gameObject;
        uiElementos[2] = container2.transform.GetChild(2).gameObject;
        uiElementos[3] = container2.transform.GetChild(3).gameObject;
        uiElementos[4] = container2.transform.GetChild(4).gameObject;
        uiElementos[5] = container2.transform.GetChild(5).gameObject;
        uiElementos[6] = container2.transform.GetChild(6).gameObject;
        uiElementos[7] = container2.transform.GetChild(7).gameObject;
        gameOverPainel = container2.transform.GetChild(11).gameObject;
        denovo = container2.transform.GetChild(11).GetChild(3).GetComponent<Button>();
        //Animators
        //animaEmFrente = container2.transform.GetChild(8).GetComponent<Animator>();
        barCima = container2.transform.GetChild(9).GetComponent<Animator>();
        barBaixo = container2.transform.GetChild(10).GetComponent<Animator>(); ;
        //gamecontrol.Load();
        //RBs
        heroiRb = heroi.GetComponent<Rigidbody2D>();
        //veiculoRb = veiculo.GetComponent<Rigidbody2D>();

        Reinicia();

        denovo.onClick.AddListener(ReloadScene);
    }
    void Start()
    {
        UpdateBulletUI();
        UpdateBombsUI();
        UpdateCoinsUI();
        UpdateLifeUI();
        //PlayerData.playerdata.life.ToString();
        //player.SetPlayerStatus();
        if (File.Exists(filePath))
        {
            Load();
        }

    }
    void Update()
    {
        if (OndeEstou.instance.fase >= 2)
        {
            Save();

            UpdateShop();
        }

        
        if (Input.GetKeyDown(KeyCode.F))
        {
            ReloadScene();
        }
    }

    public void QuebraParentesco()
    {
        veiculo.GetComponent<BoxCollider2D>().enabled = false;
        veiculo.GetComponentInParent<Rigidbody2D>().isKinematic = true;
        heroi.SetActive(true);
        heroi.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 12), ForceMode2D.Impulse);
        heroi.transform.parent = null;
        heroi.GetComponent<CapsuleCollider2D>().enabled = true;

        if (heroi.transform.localScale.x <= -1)
        {
            heroi.transform.localScale = new Vector3(1, 1, 1);
        }
        personagem = 0;
    }

    public void LutaFinal(Transform t)
    {
        if (ExeUmaVez == 0)
        {
            if (Vector2.Distance(t.transform.position, merchant.position) < 5)
            {
                //DisplayShop.inst.shopPainel.SetActive(false);
                gameEstado = 1;
                //barCima.Play("BarraCima");
                //barBaixo.Play("BarraBaixo");
                for (int i = 0; i < uiElementos.Length; i++)
                {
                    uiElementos[i].SetActive(false);
                }

                //fungus.ExecuteBlock("Start");
                StartCoroutine("TempoCoroutine2");
            }
        }
        else
        {
            gameEstado = 0;
            //barCima.Play("BarraCimaReverso");
            //barBaixo.Play("BarraBaixoReverso");
            //DisplayShop.inst.shopPainel.SetActive(true);
            for (int i = 0; i < uiElementos.Length; i++)
            {
                uiElementos[i].SetActive(true);
            }

        }
        if (ExeUmaVez == 0 || ExeUmaVez == 1)
        {
            if (Vector2.Distance(t.transform.position, boss.position) < 13)
            {

                gameEstado = 1;
                barCima.Play("BarraCima");
                barBaixo.Play("BarraBaixo");
                for (int i = 0; i < uiElementos.Length; i++)
                {
                    uiElementos[i].SetActive(false);
                }

                //fungus.ExecuteBlock("Start");
                StartCoroutine("TempoCoroutine");
            }

        }
        else
        {
            gameEstado = 0;
            barCima.Play("BarraCimaReverso");
            barBaixo.Play("BarraBaixoReverso");
            for (int i = 0; i < uiElementos.Length; i++)
            {
                uiElementos[i].SetActive(true);
            }

        }

    }
    IEnumerator TempoCoroutine2()
    {
        yield return fungosTime;
        fungus.ExecuteBlock("Merchant");
    }
    IEnumerator TempoCoroutine()
    {
        yield return fungosTime;
        fungus.ExecuteBlock("Start");
    }

    public void FechaBlocoMerchant()
    {
        ExeUmaVez = 1;
        StopCoroutine("TempoCoroutine2");
        
    }
    public void FechaBloco()
    {
        ExeUmaVez = 2;
        StopCoroutine("TempoCoroutine");
        CameraSegue.inst.enabled = false;
    }

    public void GameOver()
    {
        gameOverPainel.gameObject.SetActive(true);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Reinicia()
    {
        personagem = (int)character.personagem;
        gameEstado = (int)Estado.jogo;
        gameOver = false;
        ExeUmaVez = 0;
        t = new WaitForSeconds(2.5f);
        heroi.GetComponent<Rigidbody2D>();
        //veiculo.GetComponent<Rigidbody2D>();
        ExeUmaVez = 0;

        gameEstado = 0;
        gameOverPainel.gameObject.SetActive(false);
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath);

        PlayerData data = new PlayerData();

        data.coin = coins;
        data.life = life;
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
    }
    public void Load()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath,FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            life = data.life;
            coins = data.coin;
            bullets = data.bullets;
            bombs = data.bombs;
            upgradeCost = data.upgradeCost;
            damage = data.damage;
            damageBomb = data.damageBomb;
            boxLife = data.boxLife;
            fireRate = data.fireRate;
            reloadTime = data.reloadTime;

            
        }
    }
    void UpdateBulletUI()
    {
        FindObjectOfType<UIManager>().UpdateBulletsUI(bullets);
    }
    void UpdateBombsUI()
    {
        FindObjectOfType<UIManager>().UpdateBombsUI(bombs);
    }

    void UpdateCoinsUI()
    {
        FindObjectOfType<UIManager>().UpdateCoins();
    }
    void UpdateShop()
    {
        FindObjectOfType<UpgradeShop>().UpdateUI();
    }

    void UpdateLifeUI()
    {
        FindObjectOfType<UIManager>().UpdateLifeBar();
        FindObjectOfType<UIManager>().UpdateLifeUI(life);
    }
   
}
