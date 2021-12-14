using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broken_Wall : MonoBehaviour
{
    [SerializeField]
    private GameObject boxBroken;
    private int dano = 0;
    private SpriteRenderer spriteR;
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private GameObject explosionAnim;

    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
        spriteR.sprite = sprites[0];
    }
    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("balaTank"))
        {
            AudioManager.instance.SonsFXToca(11);
            Instantiate(explosionAnim, transform.position, Quaternion.identity);
            Destroy(col.gameObject);
            if (dano < sprites.Length - 1)
            {
                dano++;
                spriteR.sprite = sprites[dano];
            }
            else if (dano == sprites.Length - 1)
            {
                AudioManager.instance.SonsFXToca(2);
                Instantiate(boxBroken, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

    }
}
