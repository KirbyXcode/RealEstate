using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIWallPaperManager : MonoBehaviour 
{
    private CanvasGroup wallPaperCG;
    private CanvasGroup sellectionCG;
    private bool isWallPaper = false;
    private MeshRenderer wallPaperRender;

    void Start()
    {
        wallPaperCG = transform.Find("Panel_WallPaper").GetComponent<CanvasGroup>();
        sellectionCG = transform.Find("Panel_Selection").GetComponent<CanvasGroup>();
        wallPaperRender = GameObject.Find("Wall 4").GetComponent<MeshRenderer>();
    }

    public void ButtonWallPaperOnClick()
    {
        if(!isWallPaper)
        {
            sellectionCG.DOFade(1, 0.3f);
            sellectionCG.blocksRaycasts = true;
            isWallPaper = true;
        }
        else
        {
            sellectionCG.DOFade(0, 0.3f);
            sellectionCG.blocksRaycasts = false;
            isWallPaper = false;
        }
    }

    public void ButtonRedPaperOnClick()
    {
        wallPaperRender.materials[2].color = Color.red;
    }

    public void ButtonGreenPaperOnClick()
    {
        wallPaperRender.materials[2].color = Color.green;
    }

    public void ButtonBluePaperOnClick()
    {
        wallPaperRender.materials[2].color = Color.blue;
    }

    public void ButtonYellowPaperOnClick()
    {
        wallPaperRender.materials[2].color = Color.yellow;
    }
}
