using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayShop : MonoBehaviour
{
    public static DisplayShop inst;
    public GameObject shopPainel;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (GameManager.inst.gameEstado == 1)
        {
            shopPainel.SetActive(false);

        }
        else
        {
            HeroControl player = col.GetComponent<HeroControl>();
            if (player != null)
            {
                //player.canFire = false;
                shopPainel.SetActive(true);
            }
        }

    }
    private void OnTriggerExit2D(Collider2D col)
    {

        HeroControl player = col.GetComponent<HeroControl>();
        if (player != null)
        {
            //player.canFire = true;
            shopPainel.SetActive(false);
        }
    }
}
