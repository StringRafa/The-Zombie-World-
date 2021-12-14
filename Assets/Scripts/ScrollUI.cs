using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollUI : MonoBehaviour
{
    public RawImage back, front;


    void Update()
    {

        back.uvRect = new Rect(0.01f * Time.time,0,1,1);
        front.uvRect = new Rect(0.03f * Time.time, 0, 1, 1);

    }
}
