using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionAnim;
    [SerializeField]
    private float tempo;
    void Start()
    {
        //Destroy(gameObject, tempo);
        StartCoroutine(Bomb());
    }

    void Update()
    {
        
    }
    /*
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Zombie"))
        {
            AudioManager.instance.SonsFXTocaL(2);
            Instantiate(explosionAnim, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    */
    IEnumerator Bomb()
    {
        yield return new WaitForSeconds(tempo);
        //AudioManager.instance.SonsFXToca(11);
        Instantiate(explosionAnim, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
