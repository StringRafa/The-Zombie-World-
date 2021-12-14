using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int life = 5;

    public float speed;
    public float attackDistance;
    public GameObject coin;
    public GameObject deathAnimation;

    protected Animator anim;
    protected bool facingRigth = true;
    protected Transform target;
    protected float targetDistance;
    protected Rigidbody2D rb2d;
    protected SpriteRenderer sprite;

    private void Awake()
    {

        anim = GetComponent<Animator>();
        target = FindObjectOfType<HeroControl>().transform;
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }


    protected virtual void Update()
    {
        targetDistance = transform.position.x - target.position.x;
    }

    protected void Flip()
    {
        facingRigth = !facingRigth;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
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

}
