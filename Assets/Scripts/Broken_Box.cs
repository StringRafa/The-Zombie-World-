using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broken_Box : MonoBehaviour
{
    [SerializeField]
    private GameObject boxBroken, surprise;
    private int dano = 0;
    private SpriteRenderer spriteR;
    [SerializeField]
    private Sprite[] sprites;
    public int multiplica;


    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
        spriteR.sprite = sprites[0];
    }
    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("bala"))
        {

            Destroy(col.gameObject);
            if (dano < sprites.Length - 1)
            {
                dano++;
                spriteR.sprite = sprites[dano];
            }
            else if (dano == sprites.Length - 1)
            {
                AudioManager.instance.SonsFXTocaL(3);
                if (multiplica == 1)
                {
                    Instantiate(surprise, transform.position, Quaternion.identity);
                }
                if (multiplica == 2)
                {
                    Instantiate(surprise, transform.position, Quaternion.identity);
                    Instantiate(surprise, transform.position, Quaternion.identity);
                }
                Instantiate(boxBroken, transform.position, Quaternion.identity);
                
                Destroy(gameObject);
            }
        }
        if (col.gameObject.CompareTag("tank"))
        {
            AudioManager.instance.SonsFXTocaL(3);
            Instantiate(boxBroken, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Bomb"))
        {
            AudioManager.instance.SonsFXTocaL(3);
            Instantiate(boxBroken, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
