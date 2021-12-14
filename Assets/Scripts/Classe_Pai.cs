using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class InfosChar
{
    public float puloMenorY = 6.0f, puloMaiorY = 2;

    public bool face = true;
    public float maxSpeed = 5f;
    public float move;
    public bool noChao;
    public Transform noChaoCheck;
    public float noChaoRaio = 0.5f;
    public LayerMask oQueEChao;
    public float jumpForce = 5f;
    public Rigidbody2D rb;

    //Joy
    public JoyStickControle joyC;

    //Btn
    public Button pulo, tiro;


}

public abstract class Classe_Pai : MonoBehaviour
{
    public InfosChar infChar;

    public Vector2 direcaoH;

    public WaitForSeconds tempo = new WaitForSeconds(1);

    public virtual void Start()
    {
        infChar.pulo = GameObject.FindWithTag("pulo").GetComponent<Button>();
        infChar.pulo.onClick.AddListener(Pulo);

        infChar.tiro = GameObject.FindWithTag("tiro").GetComponent<Button>();
        infChar.tiro.onClick.AddListener(Tiro);

        direcaoH = Vector2.right;
    }

    public abstract void Pulo();
    public abstract void Tiro();

    void FaceMetodo(Transform t)
    {
        if (infChar.move > 0 && !infChar.face)
        {
            Flip(t);
        }
        else if (infChar.move < 0 && infChar.face)
        {
            Flip(t);
        }
    }

    void Flip(Transform obj)
    {
        infChar.face = !infChar.face;
        Vector3 tempScale = obj.localScale;
        tempScale.x *= -1;
        obj.localScale = tempScale;
    }

    protected void InfosPulo()
    {
        infChar.noChao = Physics2D.OverlapCircle(infChar.noChaoCheck.position, infChar.noChaoRaio, infChar.oQueEChao);

        if (infChar.rb.velocity.y < 0)
        {
            infChar.rb.gravityScale = infChar.puloMenorY;
        }
        else if (infChar.rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        {
            infChar.rb.gravityScale = infChar.puloMaiorY;
        }
        else
        {
            infChar.rb.gravityScale = 1;
        }

        //if (infChar.noChao)
        //{
            infChar.rb.velocity = new Vector2(infChar.move * infChar.maxSpeed, infChar.rb.velocity.y);
        //}
        FaceMetodo(transform);
    }
    /*
   public void OnCollisionEnter2D(Collision2D col)
   {
       if (col.gameObject.CompareTag("Zombie"))
       {
           KnockBack(-2.5f * direcaoH.x);
           SendMessage("Dano",1);
       }
   }


   void KnockBack(float power)
   {
       iTween.MoveBy(gameObject,new Vector3(power,0,0),0.3f);
       iTween.ColorTo(gameObject, iTween.Hash("g", 0, "b", 0, "time", 0.05f, "looptype", iTween.LoopType.pingPong,"oncomplete", "StopEffect"));
   }

   void VoltaCor()
   {
       iTween.ColorTo(gameObject, iTween.Hash("color", Color.white, "time", 0.1f));
   }

   IEnumerator StopEffect()
   {
       yield return tempo;
       iTween.Stop(gameObject,true);
       VoltaCor();
   }
   */
}
