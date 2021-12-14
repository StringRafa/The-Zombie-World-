using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField]
    public int life;
    [SerializeField]
    public int bombs;


    private void Start()
    {
        Physics2D.IgnoreLayerCollision(14, 15);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        HeroControl player = col.GetComponent<HeroControl>();

        if (player != null)
        {
            player.SetLifeAndBombs(life, bombs);
            Destroy(gameObject);
        }

        if (col.CompareTag("hero"))
        {
            AudioManager.instance.SonsFXToca(18);
        }
    }
}
