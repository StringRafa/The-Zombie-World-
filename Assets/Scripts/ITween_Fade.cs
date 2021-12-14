using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITween_Fade : MonoBehaviour
{

    void Start()
    {
        StartCoroutine("EfeitoAlpha");
    }

    IEnumerator EfeitoAlpha()
    {
        yield return new WaitForSeconds(0.5f);
        iTween.FadeTo(gameObject, iTween.Hash("alpha", 0,"delay",0.01f, "time", 0.02f, "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.easeInElastic));
    }
}
