using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MataObject : MonoBehaviour
{
    [SerializeField]
    private float tempo;
    void Start()
    {
        Destroy(gameObject, tempo);
    }


}
