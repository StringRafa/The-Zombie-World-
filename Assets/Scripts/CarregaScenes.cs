using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregaScenes : MonoBehaviour
{
    public void Carregamento(string s)
    {
        SceneManager.LoadScene(s);
    }
}
