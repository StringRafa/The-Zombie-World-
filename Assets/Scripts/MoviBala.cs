using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoviBala : MonoBehaviour
{
    public float vel = 50;
    public float Vel
    {
        get {return vel; }
        set {vel = value; }
    }
    [SerializeField]
    private Transform front, back;
    private void Start()
    {
        front = GameObject.FindWithTag("front").GetComponent<Transform>();
        back = GameObject.FindWithTag("back").GetComponent<Transform>();
    }
    private void Move()
    {
        Vector3 aux = transform.position;
        aux.x += vel * Time.deltaTime;
        transform.position = aux;
    }

    void Update()
    {
        Move();
        /*
        if (transform.position.x > front.position.x || transform.position.x < back.position.x)
        {
            Destroy(gameObject);
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("front") || col.gameObject.CompareTag("back"))
        {
            Destroy(gameObject);
        }
    }
}
