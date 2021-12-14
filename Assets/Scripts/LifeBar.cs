using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public static LifeBar inst;

    public int maxHeart;
    public int inicioQuantCor;
    public int quantPedaHeart = 10;
    public Image[] emptyHeart;
    public Sprite[] spriteHeart;

    public int vidaAtual;
    public int maxVida;
    public GameObject playerDead;

    public static LifeBar instance = null;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        CalculaValoresVida();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            MaisHeart();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Dano(1);
        }
    }
    void QuantityLife()
    {
        for (int i = 0; i < maxHeart; i++)
        {
            if (inicioQuantCor <= i)
            {
                emptyHeart[i].enabled = false;
            }
            else
            {
                emptyHeart[i].enabled = true;
            }
        }
        Coracoes();
    }

    void Coracoes()
    {
        bool vazio = false;
        int x = 0;
        foreach (Image imagem in emptyHeart)
        {
            if (vazio == true)
            {
                imagem.sprite = spriteHeart[0];

            }
            else
            {
                x++;
                if (vidaAtual >= x * quantPedaHeart)
                {
                    imagem.sprite = spriteHeart[2];
                }
                else
                {
                    int coracoesAtual = (int)(quantPedaHeart - (quantPedaHeart * x - vidaAtual));
                    int vidaImagem = quantPedaHeart / (spriteHeart.Length - 1);
                    int id = coracoesAtual / vidaImagem;
                    imagem.sprite = spriteHeart[id];
                    vazio = true;
                }
            }
        }
    }

    public void Dano(int d)
    {
        if (vidaAtual > 0)
        {
            vidaAtual -= d;
        }
        else
        {
            GameManager.inst.gameOver = true;
            CameraSegue.inst.segueHeroi = false;
            GameObject temp = Instantiate(playerDead,new Vector2(transform.position.x,transform.position.y),Quaternion.identity) as GameObject;

            temp.transform.localScale = transform.localScale;
            Destroy(gameObject);
            print("GameOver");
        }
        Coracoes();
    }

    public void MaisHeart()
    {
        if (inicioQuantCor < maxHeart)
        {
            inicioQuantCor++;
        }
        CalculaValoresVida();

    }

    void CalculaValoresVida()
    {
        vidaAtual = inicioQuantCor * quantPedaHeart;
        maxVida = maxHeart * quantPedaHeart;

        QuantityLife();
    }
}
