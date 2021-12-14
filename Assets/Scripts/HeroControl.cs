using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;




public class HeroControl : Classe_Pai
{
    public static HeroControl inst;

    [Header("Variaveis do Player")]
    public Transform carro;
    public Animator animHSup, animHInf;

    public GameObject bala,canoArma,bomb,bombL;

    public WaitForSeconds tempo2 = new WaitForSeconds(0.2f);

    [SerializeField]
    private JoyStickControle joyC;

    private float fireRate = 1f;
    private float nextFire;
    private int bullets;
    private float reloadTime = 5f;
    private int life;
    private int maxLife;
    private int bombs;
    private bool reloading;
    public bool canFire = true;
    public float damageTime = 1f;
    private bool tookDamage = false;
    public GameObject playerDead;
    private Vector2 direcao;
    public bool heroiDead = false;

    /*
    [SerializeField]
    private Transform boss, merchant;
    [SerializeField]
    private Animator barCima, barBaixo;

    public Flowchart fungus;
    public GameObject[] uiElementos;
    public int ExeUmaVez = 0;
    private WaitForSeconds fungosTime;
    */
    public bool enemyDead = false;

    GameManager gameManager;
    
    new void Start()
    {

        Physics2D.IgnoreLayerCollision(3, 19);
        base.Start();
        infChar.rb = GetComponent<Rigidbody2D>();
        gameManager = GameManager.inst;
        SetPlayerStatus();
        bombs = gameManager.bombs;
        life = maxLife;
        UpdateBulletUI();
        UpdateBombsUI();
        UpdateLifeUI();

    }

    void Update()
    {
       
        if (gameManager.gameEstado == 0)
        {
            if (GameManager.inst.personagem == 0)
            {

                //Animação Andar
                infChar.move = infChar.joyC.Hori();
                animHInf.SetFloat("X", Mathf.Abs(infChar.move));

                //Animação Pulo


            }
        }
        else
        {
            infChar.move = 0;
            animHInf.SetFloat("X", 0);
        }



        animHSup.SetFloat("Y", infChar.rb.velocity.y);
        animHInf.SetFloat("Y", infChar.rb.velocity.y);
        animHSup.SetBool("Chao", infChar.noChao);
        animHInf.SetBool("Chao", infChar.noChao);

        gameManager.LutaFinal(transform);

        /*

        if (ExeUmaVez == 0)
        {
            if (Vector2.Distance(transform.position, boss.position) < 13)
            {

                gameManager.gameEstado = 1;
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
                gameManager.gameEstado = 0;
                barCima.Play("BarraCimaReverso");
                barBaixo.Play("BarraBaixoReverso");
                for (int i = 0; i < uiElementos.Length; i++)
                {
                    uiElementos[i].SetActive(true);
                }
          
        }
        if(ExeUmaVez == 0 || ExeUmaVez == 1)
        {
            if (Vector2.Distance(transform.position, merchant.position) < 5)
            {
                //DisplayShop.inst.shopPainel.SetActive(false);
                gameManager.gameEstado = 1;
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
            gameManager.gameEstado = 0;
            //barCima.Play("BarraCimaReverso");
            //barBaixo.Play("BarraBaixoReverso");
            //DisplayShop.inst.shopPainel.SetActive(true);
            for (int i = 0; i < uiElementos.Length; i++)
            {
                uiElementos[i].SetActive(true);
            }

        }
        */
    }
    /*
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
        StopCoroutine("TempoCoroutine2");
        ExeUmaVez = 2;
    }
    public void FechaBloco()
    {
        StopCoroutine("TempoCoroutine");
        CameraSegue.inst.enabled = false;
        //gameManager.gameEstado = 0;
        ExeUmaVez = 1;

    }
        */
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(infChar.noChaoCheck.position, infChar.noChaoRaio);
    }

    private void FixedUpdate()
    {
        if (GameManager.inst.personagem == 0)
        {
            InfosPulo();
        }
    }

    public override void Pulo()
    {
        if (gameManager.gameEstado == 0)
        {
            if (GameManager.inst.personagem == 0)
            {
                if (infChar.noChao)
                {
                    AudioManager.instance.SonsFXToca(9);
                    infChar.rb.AddForce(new Vector2(0, infChar.jumpForce), ForceMode2D.Impulse);
                }
            }
        }


    }
    public override void Tiro()
    {
        if (gameManager.gameEstado == 0)
        {
            if (GameManager.inst.personagem == 0 && Time.time > nextFire && bullets > 0 && !reloading && canFire && heroiDead == false)
            {
                nextFire = Time.time + fireRate;
                AudioManager.instance.SonsFXToca(10);
                animHSup.SetTrigger("Tiro");
                GameObject balaInst = Instantiate(bala, canoArma.transform.position, Quaternion.identity) as GameObject;
                balaInst.GetComponent<MoviBala>().Vel *= transform.localScale.x;
                bullets--;
                UpdateBulletUI();
            }
            else if (GameManager.inst.personagem == 0 && bullets <= 0 && infChar.noChao)
            {
                StartCoroutine(Reloading());
            }
        }



    }
    public void Bomba()
    {
        SetPlayerStatus();
        if (gameManager.gameEstado == 0)
        {
            if (GameManager.inst.personagem == 0 && bombs > 0 && Time.time > nextFire && heroiDead == false)
            {
                nextFire = Time.time + fireRate;
                animHSup.SetTrigger("Bomb");
                GameObject bombInst = Instantiate(bomb, bombL.transform.position, Quaternion.identity) as GameObject;
                bombInst.GetComponent<Rigidbody2D>().AddForce(new Vector2(20.5f * transform.localScale.x, 8.5f), ForceMode2D.Impulse);
                //gameManager.bombs--;
                bombs--;
                UpdateBombsUI();
            }
        }

    }
    
    IEnumerator Reloading()
    {
        reloading = true;
        animHSup.SetBool("Reload", true);
        yield return new WaitForSeconds(reloadTime);
        bullets = gameManager.bullets;
        animHSup.SetBool("Reload", false);
        UpdateBulletUI();
        reloading = false;
    }
    
    //Veiculo

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("entrada"))
        {
            carro = col.transform;
            carro.GetComponentInParent<Rigidbody2D>().isKinematic = false;
            GameManager.inst.personagem = 1;
            transform.SetParent(carro, true);
            transform.localPosition = new Vector2(0, 0);
            transform.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.SetActive(false);
            AudioManager.instance.SonsFXTocaL(1);
        }
        if (col.CompareTag("Zombie") && !tookDamage)
        {
            StartCoroutine(TookDamage(col));
        }
        if (col.CompareTag("BalaTerrorist"))
        {
            StartCoroutine(TookDamage(col));
            Destroy(col.gameObject);
        }

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Zombie") && !tookDamage)
        {
            StartCoroutine(TookDamage(col));
        }
    }

    public void OnCollisionEnter2D(UnityEngine.Collision2D col)
    {
        if (col.gameObject.CompareTag("coin"))
        {
            AudioManager.instance.SonsFXToca(19);
            Destroy(col.gameObject);
            gameManager.coins += Random.Range(1,5);
            UpdateCoinsUI();
        }

    }

    IEnumerator TookDamage(Collider2D zombie)
    {

        AudioManager.instance.SonsFXTocaL(4);
        tookDamage = true;
        life--;
        UpdateLifeUI();
        if (life <= 0)
        {
            heroiDead = true;
            AudioManager.instance.SonsFXTocaL(5);
            GameManager.inst.gameOver = true;
            CameraSegue.inst.segueHeroi = false;
            GameObject temp = Instantiate(playerDead, new Vector2(transform.position.x, transform.position.y), Quaternion.identity) as GameObject;
            temp.transform.localScale = transform.localScale;
            animHSup.gameObject.SetActive(false);
            animHInf.gameObject.SetActive(false);
            yield return new WaitForSeconds(2f);
            gameManager.GameOver();           
            Destroy(gameObject,1000000f);
            print("GameOver");

        }
        else if(life > 0)
        {
            
            
            //direcao = infChar.rb.transform.position - zombie.transform.position;
            
            //iTween.MoveBy(infChar.rb.gameObject, iTween.Hash("x", direcao.normalized.x * 0.5f, "time", 0.3f));
            
            Physics2D.IgnoreLayerCollision(3, 8);
            for (float i = 0; i < damageTime; i+= 0.2f)
            {

                CameraSegue.inst.CamShake();
                iTween.ColorTo(infChar.rb.gameObject, iTween.Hash("g", 0, "b", 0, "time", 0.03f, "looptype", iTween.LoopType.pingPong, "oncompletetarget", infChar.rb.gameObject, "oncomplete", "StopEffect"));
                yield return new WaitForSeconds(0.2f);

            }
   
            Physics2D.IgnoreLayerCollision(3, 8,false);
            tookDamage = false;
        }

    }



    void UpdateLifeUI()
    {
        FindObjectOfType<UIManager>().UpdateLifeUI(life);
    }

    public void SetPlayerStatus()
    {
        fireRate = gameManager.fireRate;
        bullets = gameManager.bullets;
        reloadTime = gameManager.reloadTime;
        maxLife = gameManager.life;
        bombs = gameManager.bombs;
    }

    void UpdateBulletUI()
    {
        FindObjectOfType<UIManager>().UpdateBulletsUI(bullets);
    }
    void UpdateBombsUI()
    {
        FindObjectOfType<UIManager>().UpdateBombsUI(bombs);
        gameManager.bombs = bombs;
    }

    void UpdateCoinsUI()
    {
        FindObjectOfType<UIManager>().UpdateCoins();
    }
    IEnumerator StopEffect()
    {
        yield return tempo;
        iTween.Stop(gameObject, true);
        VoltaCor();
    }
    void VoltaCor()
    {
        iTween.ColorTo(gameObject, iTween.Hash("color", Color.white, "time", 0.1f));
    }

    public void SetLifeAndBombs(int lifeup,int bomb)
    {
        life += lifeup;
        if (life >= maxLife)
        {
            life = maxLife;
        }
        bombs += bomb;
        UpdateBombsUI();
        UpdateLifeUI();
    }
}
