using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager inst;

    public Text bulletsTXT;
    public Text coinsTXT;
    public Text bombsTXT;
    public Text lifeTXT;
    public Slider lifeBar;

    void Start()
    {
        UpdateCoins();
        UpdateLifeBar();
    }

    public void UpdateBulletsUI(int bullets)
    {
        bulletsTXT.text = bullets.ToString();
    }

    public void UpdateBombsUI(int bombs)
    {
        bombsTXT.text = bombs.ToString();
    }

    public void UpdateCoins()
    {
        coinsTXT.text = GameManager.inst.coins.ToString();
    }

    public void UpdateLifeUI(int life)
    {
        lifeTXT.text = life.ToString();
        lifeBar.value = life;
    }

    public void UpdateLifeBar()
    {
        lifeBar.maxValue = GameManager.inst.life;
    }
}
