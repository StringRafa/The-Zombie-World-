using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomFacada : MonoBehaviour
{
  public void SomFacada2()
    {
        AudioManager.instance.SonsFXToca(16);
    }

    public void SomExplosion()
    {
        AudioManager.instance.SonsFXToca(11);
    }
}
