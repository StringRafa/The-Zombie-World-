using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSegue : MonoBehaviour
{


    public GameObject player;
    public float camVel = 0.25f;
    public bool segueHeroi;
    public Vector3 ultimoAlvoPos;
    public Vector3 velAtual;
    [Range(0, 5)]
    public float ajustCamY = 1,ajustCamX = 1;
    Vector3 novaCamPos,novaCamPosX;

    public static CameraSegue inst;

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
    }

    void Start()
    {
        segueHeroi = true;
        ultimoAlvoPos = player.transform.position;
    }

    private void FixedUpdate()
    {
        if (segueHeroi)
        {
            if (player.transform.position.x >=  transform.position.x)
            {
                novaCamPos = Vector3.SmoothDamp(transform.position, player.transform.position, ref velAtual, camVel);
                //novaCamPosX = Vector3.SmoothDamp(player.transform.position, transform.position,  ref velAtual, camVel);
                transform.position = new Vector3(novaCamPos.x, novaCamPos.y + ajustCamY, transform.position.z);
 
            }
            else
            {
                novaCamPos = Vector3.SmoothDamp(transform.position, player.transform.position, ref velAtual, camVel);
                //novaCamPosX = Vector3.SmoothDamp(player.transform.position, transform.position, ref velAtual, camVel);
                transform.position = new Vector3(transform.position.x, novaCamPos.y + ajustCamY, transform.position.z);
            }
        }
    }

    public void CamShake()
    {
        iTween.ShakePosition(gameObject, new Vector3(0.8f, 0, 0), 0.3f);
    }
}
