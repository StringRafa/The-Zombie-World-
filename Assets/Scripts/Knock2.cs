using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knock2 : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rbPai;
    private Vector2 direcao;
    void Start()
    {
        rbPai = GetComponentInParent<Rigidbody2D>();
    }


    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Zombie"))
        {
            AudioManager.instance.SonsFXToca(7);
            Danos(col);           
        }
    }
    


    public void Danos(Collider2D zombie)
    {

        direcao = rbPai.transform.position - zombie.transform.position;
        CameraSegue.inst.CamShake();
        iTween.MoveBy(rbPai.gameObject, iTween.Hash("x", direcao.normalized.x * 5f, "time", 0.3f));
        iTween.ColorTo(rbPai.gameObject, iTween.Hash("g", 0, "b", 0, "time", 0.03f, "looptype", iTween.LoopType.pingPong, "oncompletetarget", rbPai.gameObject, "oncomplete", "StopEffect"));
        LifeBar.instance.Dano(1);       
    }

}
