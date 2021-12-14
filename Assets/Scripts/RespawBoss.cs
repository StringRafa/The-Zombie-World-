using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawBoss : MonoBehaviour
{
    public static RespawBoss inst;
    public GameObject EnemyShootingPrefab;
    public float tempo = 20;
    public bool ativacao;
    public GameObject player;


    private void Update()
    {
        if (ativacao)
        {
            tempo -= Time.deltaTime;

            if (tempo < 0)
            {
                Instantiate(EnemyShootingPrefab,new Vector2(player.transform.position.x + 15, player.transform.position.y + 2),Quaternion.identity);
                Instantiate(EnemyShootingPrefab, new Vector2(player.transform.position.x - 15, player.transform.position.y + 2), Quaternion.identity);
               
                tempo = 20;
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
