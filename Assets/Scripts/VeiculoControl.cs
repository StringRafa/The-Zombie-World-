using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VeiculoControl : Classe_Pai
{
    [Header("Variaveis do Veiculo")]
    //Balas
    public GameObject bala, canoArma;
    public Animator tankAnim;
    public Rigidbody2D rbTank;

    new void Start()
    {
        base.Start();
        infChar.rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (GameManager.inst.personagem == 1)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                GameManager.inst.QuebraParentesco();
            }
        }
        //Animação Andar
        if (infChar.noChao)
        {
            infChar.move = infChar.joyC.Hori();
            tankAnim.SetFloat("X", Mathf.Abs(infChar.move));

        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(infChar.noChaoCheck.position, infChar.noChaoRaio);
    }

    private void FixedUpdate()
    {
        if (GameManager.inst.personagem == 1)
        {
            InfosPulo();
        }
    }
    public override void Pulo()
    {
        if (GameManager.inst.personagem == 1)
        {
            if (infChar.noChao)
            {
                infChar.rb.AddForce(new Vector2(0, infChar.jumpForce), ForceMode2D.Impulse);
                //animHInf.SetTrigger("Pulo");
            }
        }

    }
    public override void Tiro()
    {
        if (GameManager.inst.personagem == 1)
        {
            if (infChar.noChao)
            {
                AudioManager.instance.SonsFXToca(12);
                tankAnim.SetTrigger("Tiro");
                GameObject balaInst = Instantiate(bala, canoArma.transform.position, Quaternion.identity) as GameObject;
                balaInst.GetComponent<MoviBala>().Vel *= transform.localScale.x;
            }
        }

    }
    
}
