using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DISTANCIA_VILAO : MonoBehaviour {

    public GameObject zombie01,zombie02,zombie03;
    public float tempo = 10;
    public bool ativacao;
    public GameObject player;
    public int zombie = 0;


    private void Update()
    {
        if (ativacao)
        {
            tempo -= Time.deltaTime;

            if (tempo < 0 && zombie == 0)
            {
                Instantiate(zombie01, new Vector2(player.transform.position.x - 20, player.transform.position.y + 2), Quaternion.identity);
                tempo = 10;
                zombie = 1;
            }
            if (tempo < 0 && zombie == 1)
            {
                Instantiate(zombie02, new Vector2(player.transform.position.x - 20, player.transform.position.y + 2), Quaternion.identity);
                tempo = 10;
                zombie = 2;
            }
            if (tempo < 0 && zombie == 2)
            {
                Instantiate(zombie03, new Vector2(player.transform.position.x - 20, player.transform.position.y + 2), Quaternion.identity);
                tempo = 10;
                zombie = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("hero"))
        {
            ativacao = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("hero"))
        {
            ativacao = false;
        }
    }

}
