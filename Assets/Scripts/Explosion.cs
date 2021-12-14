using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private bool colidiu = false;
    [SerializeField]
    private float raio;
    [SerializeField]
    private LayerMask layer;
    [SerializeField]
    private GameObject explosionAnim;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (colidiu)
        {
            AudioManager.instance.SonsFXTocaL(2);
            //Instantiate(explosionAnim,transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        colidiu = Physics2D.OverlapCircle(transform.position, raio, layer);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, raio);
    }
}
