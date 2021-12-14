using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TweenMoviPer : MonoBehaviour
{
    [SerializeField]
    public RectTransform imgRect;
    [SerializeField]
    public Image img;
    [SerializeField]
    public float pos;
    [SerializeField]
    public int duract;
    [SerializeField]
    private float startPosX;

    void Start()
    {
        imgRect = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        startPosX = transform.position.x;
        StartCoroutine(MoveX(1));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(MoveXInvers(0.5f));
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(MoveX(1));
        }
    }

    IEnumerator MoveX(float temp)
    {
        yield return new WaitForSeconds(temp);
        img.enabled = true;
        imgRect.DOAnchorPosX(pos, duract, true);
        img.DOFade(1, 0.5f);
    }

    IEnumerator MoveXInvers(float temp)
    {
        while (img.color.a > 0.01f)
        {
            yield return new WaitForSeconds(temp);
            imgRect.DOMoveX(startPosX, duract, true);
            img.DOFade(0, 0.5f);

        }
        img.enabled = false;
    }
}
