using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieControl : MonoBehaviour
{
    private float velMove = 2f;
    private Rigidbody2D rb;
    private bool moveE;
    [SerializeField]
    private Transform[] limite;

    public LayerMask layer;
    private Animator anim;

    [SerializeField]
    private bool visaoV;
    [SerializeField]
    private float raio;
    [SerializeField]
    private LayerMask layerV;
    [SerializeField]
    private SpriteRenderer sRender;
    private bool chamado = true, pontoAttack;
    private WaitForSeconds tempo2 = new WaitForSeconds(0.5f);
    public GameObject deathAnimation;

    [SerializeField]
    private GameObject explosionAnim,coin,zombieMorte;
    public WaitForSeconds tempo = new WaitForSeconds(0.2f);

    public int life = 10; //Vida do Zombie
    //public int danoBala = 1, danoBomb = 3, danoTank = 10;
    public Transform lifebarGreen; //barra Verde
    public GameObject lifeBarPai; // Objeto pai das barras

    private Vector3 lifeBarScale; //Tamanho da Barra
    private float lifePercent; //Percentual de vida



    void Start()
    {
        lifeBarScale = lifebarGreen.localScale;
        lifePercent = lifeBarScale.x / life;
        lifeBarPai.gameObject.SetActive(false);

        Physics2D.IgnoreLayerCollision(8, 19);
        Physics2D.IgnoreLayerCollision(8,8);  //Outro Zombie
        Physics2D.IgnoreLayerCollision(8, 6); //Cam
        Physics2D.IgnoreLayerCollision(8, 11); //Tank
        Physics2D.IgnoreLayerCollision(8, 12); //PlayerDead
        rb = GetComponent<Rigidbody2D>();
        moveE = true;
        anim = GetComponent<Animator>();

        sRender = GetComponent<SpriteRenderer>();


    }

    void UpdateLifeBar()
    {
        lifeBarScale.x = lifePercent * life;
        lifebarGreen.localScale = lifeBarScale;
    }

    public Knock2 k;
    private WaitForSeconds t = new WaitForSeconds(1);

    IEnumerator PersegueH(bool flipx, bool movE)
    {
        yield return t;
        sRender.flipX = flipx;
        moveE = movE;
    }
    void Update()
    {

        Physics2D.IgnoreLayerCollision(8, 19);
        Physics2D.IgnoreLayerCollision(8, 8);
        //AudioManager.instance.SonsFXToca(6);
        visaoV = Physics2D.OverlapCircle(transform.position, raio, layerV);
        pontoAttack = Physics2D.OverlapCircle(transform.position, raio*0.1f, layerV);

        if (visaoV && pontoAttack == false)
        {
            var relativeP = transform.InverseTransformPoint(Physics2D.OverlapCircle(transform.position, raio, layerV).gameObject.transform.position);
            
            if (relativeP.x < 0.0f)
            {
                //AudioManager.instance.SonsFXToca(6);
                anim.Play("Zombie_01_Attack");
                Physics2D.IgnoreLayerCollision(8, 8, false);
                velMove = 5f;
                StartCoroutine(PersegueH(true, false));
            }
            else if (relativeP.x > 0.0f)
            {
                //AudioManager.instance.SonsFXToca(3);
                anim.Play("Zombie_01_Attack");
                Physics2D.IgnoreLayerCollision(8, 8, false);
                velMove = 5f;
                StartCoroutine(PersegueH(false, true));
            }
        }

        if (!pontoAttack)
        {
            //AudioManager.instance.SonsFXToca(1);
            if (moveE)
            {
                rb.velocity = new Vector2(-velMove, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(velMove, rb.velocity.y);
            }
        }
        else
        {
            
            anim.Play("Zombie_01_Attack2");
            Physics2D.IgnoreLayerCollision(8, 3, false);

            /*
            if (k == null)
            {
                k = Physics2D.OverlapCircle(transform.position, raio * 0.2f, layerV).GetComponent<Knock2>();
            }
            k.Danos(Physics2D.OverlapCircle(transform.position, raio * 0.2f, layerV).GetComponent<Collider2D>());
        */
        }





        if (visaoV == false && pontoAttack == false)
        {
           
            if (moveE)
            {
                velMove = 3;
                rb.velocity = new Vector2(-velMove, rb.velocity.y);
            }
            else
            {
                velMove = 3;
                rb.velocity = new Vector2(velMove, rb.velocity.y);
            }
        }

        VerificaCol();
    }

    void VerificaCol()
    {
        if (!Physics2D.Raycast(limite[0].position,Vector2.down,0.1f,layer)&& chamado || !Physics2D.Raycast(limite[1].position, Vector2.down, 0.1f, layer) && chamado)
        {
            StartCoroutine(ChamaFlip());
        }
    }

    IEnumerator ChamaFlip()
    {
        Flip();
        chamado = false;
        yield return tempo2;
        chamado = true;
    }

    void Flip()
    {
        moveE = !moveE;

        if (moveE)
        {
            sRender.flipX = false;
        }
        else
        {
            sRender.flipX = true;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Explosion"))
        {
            lifeBarPai.gameObject.SetActive(true);
            life -= GameManager.inst.damageBomb;
            UpdateLifeBar();
            //AudioManager.instance.SonsFXTocaL(2);
            //Instantiate(explosionAnim, transform.position, Quaternion.identity);
            if (life <= 0)
            {
                Instantiate(coin, transform.position, Quaternion.identity);
                Instantiate(deathAnimation, transform.position, transform.rotation);
                //Physics2D.IgnoreLayerCollision(8, 3);
                Instantiate(zombieMorte, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                AudioManager.instance.SonsFXTocaL(6);
                //Destroy(col.gameObject);
                //anim.Play("Zombie_01_Dead");
                //velMove = 0f;
                Destroy(gameObject);
                //lifebarGreen.gameObject.SetActive(false);
            }

        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {


        if (col.gameObject.CompareTag("bala"))
        {
            
            lifeBarPai.gameObject.SetActive(true);
            //life -= danoBala;
            life -= GameManager.inst.damage;
            UpdateLifeBar();
            Destroy(col.gameObject);
            if (life <= 0)
            {
                Instantiate(coin, transform.position, Quaternion.identity);
                Instantiate(deathAnimation, transform.position, transform.rotation);
                //anim.Play("Zombie_01_Dead");
                //velMove = 0f;
                //moveE = false;
                Destroy(gameObject);
                //lifebarGreen.gameObject.SetActive(false);
                //Physics2D.IgnoreLayerCollision(8, 3, false);
            }
            
        }


        if (col.gameObject.CompareTag("balaTank"))
        {
            lifeBarPai.gameObject.SetActive(true);
            life -= GameManager.inst.damage;
            UpdateLifeBar();
            AudioManager.instance.SonsFXToca(11);
            //Instantiate(explosionAnim, transform.position, Quaternion.identity);
            Destroy(col.gameObject);
            if (life <= 0)
            {

                Physics2D.IgnoreLayerCollision(8, 3);
                anim.Play("Zombie_01_Dead");
                velMove = 0f;
                Destroy(gameObject, 1f);
                lifebarGreen.gameObject.SetActive(false);
            }


        }
        
    }
    /*
    void KnockBack()
    {       
        iTween.ColorTo(gameObject, iTween.Hash("a", 0, "time", 0.03f, "looptype", iTween.LoopType.pingPong, "oncomplete", "StopEffect"));
    }

    void VoltaCor()
    {
        iTween.ColorTo(gameObject, iTween.Hash("color", Color.white, "time", 0));
    }

    IEnumerator StopEffect()
    {
        yield return tempo;
        iTween.Stop(gameObject, true);
        VoltaCor();
    }
    */
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,raio);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raio*0.1f);
        
    }
}
