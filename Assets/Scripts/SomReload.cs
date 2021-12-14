using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomReload : MonoBehaviour
{
    public void SomRecarrega1()
    {
        AudioManager.instance.SonsFXToca(13);
    }
    public void SomRecarrega2()
    {
        AudioManager.instance.SonsFXToca(15);
    }
}
