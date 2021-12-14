using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public static EnemyShooting inst;
    public int life;
    public bool enemyDead = false;
    //public float velMove;
    public float attackDistance;
    public GameObject coin;
    public GameObject deathAnimation;
    //[SerializeField]
    //private bool visaoV;
    //[SerializeField]
    //private float raio;
    //[SerializeField]
    //private LayerMask layerV;
    //private bool chamado = true, pontoAttack;
    //private bool moveE;
    //public Transform[] limite;
    //public LayerMask layer;

    protected Animator anim;
    protected bool facingRigth = true;
    protected Transform target;
    protected float targetDistance;
    public Rigidbody2D rb2d;
    protected SpriteRenderer sprite;
    public GameObject bulletPrefab;
    public float fireRate;
    public Transform shotSpawner;
    private float nextFire;
    public Transform lifebarGreen; //barra Verde
    public GameObject lifeBarPai; // Objeto pai das barras

    private Vector3 lifeBarScale; //Tamanho da Barra
    private float lifePercent; //Percentual de vida
    void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 18);
        lifeBarScale = lifebarGreen.localScale;
        lifePercent = lifeBarScale.x / life;
        lifeBarPai.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
        target = FindObjectOfType<HeroControl>().transform;
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void UpdateLifeBar()
    {
        lifeBarScale.x = lifePercent * life;
        lifebarGreen.localScale = lifeBarScale;
    }
    private WaitForSeconds t = new WaitForSeconds(1);
    /*
    IEnumerator PersegueH(bool flipx, bool movE)
    {
        yield return t;
        sprite.flipX = flipx;
        moveE = movE;
    }
    */
    void Update()
    {

        /*
        visaoV = Physics2D.OverlapCircle(transform.position, raio, layerV);
        pontoAttack = Physics2D.OverlapCircle(transform.position, raio * 0.7f, layerV);

        if (GameManager.inst.gameEstado == 0)
        {
            if (visaoV && pontoAttack == false)
            {
                var relativeP = transform.InverseTransformPoint(Physics2D.OverlapCircle(transform.position, raio, layerV).gameObject.transform.position);

                if (relativeP.x < 0.0f && life > 0)
                {
                    //AudioManager.instance.SonsFXToca(6);
                    anim.Play("EnemyRun");
                    velMove = 4f;
                    StartCoroutine(PersegueH(true, false));
                }
                else if (relativeP.x > 0.0f && life > 0)
                {
                    //AudioManager.instance.SonsFXToca(3);
                    
                    velMove = 4f;
                    StartCoroutine(PersegueH(false, true));
                }
            }

            if (!pontoAttack)
            {
                //AudioManager.instance.SonsFXToca(1);
                if (moveE)
                {
                    rb2d.velocity = new Vector2(-velMove, rb2d.velocity.y);
                }
                else
                {
                    rb2d.velocity = new Vector2(velMove, rb2d.velocity.y);
                }
            }
            else if (Time.time > nextFire)
            {

                if (moveE)
                {
                    Shooting();
                    //rb2d.velocity = new Vector2(-velMove, rb2d.velocity.y);
                    anim.SetTrigger("Shooting");
                    nextFire = Time.time + fireRate;
                }
                else
                {
                    Shooting();
                    //rb2d.velocity = new Vector2(velMove, rb2d.velocity.y);
                    anim.SetTrigger("Shooting");
                    nextFire = Time.time + fireRate;
                }

                //AudioManager.instance.SonsFXToca(16);
                /*
                if (k == null)
                {
                    k = Physics2D.OverlapCircle(transform.position, raio * 0.1f, layerV).GetComponent<Knock2>();
                }
                k.Danos(Physics2D.OverlapCircle(transform.position, raio * 0.1f, layerV).GetComponent<Collider2D>());
            */
        /*
            }
            if (visaoV == false && pontoAttack == false)
            {
                //anim.Play("Terrorist_Walk");
                if (moveE)
                {
                    velMove = 2;
                    rb2d.velocity = new Vector2(-velMove, rb2d.velocity.y);
                }
                else
                {
                    velMove = 2;
                    rb2d.velocity = new Vector2(velMove, rb2d.velocity.y);
                }
            }
        }

        VerificaCol();
    */

        if (GameManager.inst.gameEstado == 0 && GameManager.inst.gameOver == false)
        {
            targetDistance = transform.position.x - target.position.x;
            if (targetDistance < 0)
            {
                if (!facingRigth)
                {

                    Flip();
                }

            }
            else
            {
                if (facingRigth)
                {

                    Flip();
                }
            }

            if (Mathf.Abs(targetDistance) < attackDistance && Time.time > nextFire)
            {
                AudioManager.instance.SonsFXTocaL(7);
                anim.SetTrigger("Shooting");
                nextFire = Time.time + fireRate;

            }else if(Mathf.Abs(targetDistance) > attackDistance && Time.time > nextFire)
            {
                AudioManager.instance.SonsFXTocaL(7);
                anim.SetTrigger("Shooting");
                nextFire = Time.time + fireRate;
            }
        }


}
    /*
    void VerificaCol()
    {
        if (!Physics2D.Raycast(limite[0].position, Vector2.down, 0.1f, layer) && chamado || !Physics2D.Raycast(limite[1].position, Vector2.down, 0.1f, layer) && chamado)
        {
            StartCoroutine(ChamaFlip());
        }
    }

    IEnumerator ChamaFlip()
    {
        Flip();
        chamado = false;
        yield return new WaitForSeconds(0.5f);
        chamado = true;
    }
    
    void Flip()
    {
        moveE = !moveE;

        if (moveE)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }

    }
    */
    
    void Flip()
    {
        facingRigth = !facingRigth;
        //moveE = !moveE;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    
    public void Shooting()
    {
        GameObject balaInst = Instantiate(bulletPrefab, shotSpawner.transform.position, Quaternion.identity) as GameObject;
        balaInst.GetComponent<MoviBala>().Vel *= transform.localScale.x;

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("bala"))
        {
            lifeBarPai.gameObject.SetActive(true);
            life -= GameManager.inst.damage;
            UpdateLifeBar();
            Destroy(col.gameObject);
            if (life <= 0)
            {
                anim.gameObject.SetActive(false);
                Instantiate(coin, transform.position, transform.rotation);
                Instantiate(deathAnimation, transform.position, transform.rotation);
                enemyDead = true;
                Destroy(gameObject, 3f);
            }
            //TookDamage();
            //StartCoroutine(TookDamageCoroutine());         
        }
    }
    

    
    public void TookDamage()
    {
        life -= GameManager.inst.damage;
        if (life <= 0)
        {
            Instantiate(coin, transform.position, transform.rotation);
            Instantiate(deathAnimation, transform.position, transform.rotation);

            Destroy(gameObject, 3f);
        }
        else
        {
            StartCoroutine(TookDamageCoroutine());
        }
    }

    public IEnumerator TookDamageCoroutine()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, raio);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raio * 0.7f);

    }
    */
}
