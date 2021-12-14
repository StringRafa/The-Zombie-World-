using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ProcuraGameManager : MonoBehaviour
{
    public void FechaBloco()
    {
        GameManager.inst.FechaBloco();
    }
    public void FechaBlocoMerchant()
    {
        GameManager.inst.FechaBlocoMerchant();
    }
}
