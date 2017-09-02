using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DevelopEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour 
{
    private CanvasGroup introductionCG;
    private CanvasGroup casesCG;
    private CanvasGroup dayNightCG;
    public Sprite[] mSprite_bulidings;
    private int index = 0;
    private Image mImage_Building;
    private Text mText_BuildingType;
    private CanvasGroup maskCG;
    //private CanvasGroup effectCG;
    private CameraRotate camRotate;
    private bool isMask = false;
    private SunLight sunLight;

	void Start () 
    {
        Init();
	}

    void Init()
    {
        introductionCG = Global.FindChild(transform, "Panel_Introduction").GetComponent<CanvasGroup>();
        casesCG = Global.FindChild(transform, "Panel_Cases").GetComponent<CanvasGroup>();
        dayNightCG = transform.Find("Panel_DayNightEffect").GetComponent<CanvasGroup>();
        mImage_Building = Global.FindChild(transform, "CasePicture").GetComponent<Image>();
        mText_BuildingType = Global.FindChild(transform, "BuildingType").GetComponent<Text>();
        maskCG = transform.Find("Mask").GetComponent<CanvasGroup>();
        //effectCG = transform.Find("Panel_Effect").GetComponent<CanvasGroup>();
        camRotate = Camera.main.GetComponent<CameraRotate>();
        sunLight = FindObjectOfType<SunLight>();
    }

    /// <summary>
    /// 楼房介绍按钮
    /// </summary>
    public void ButtonIntroductionOnClick()
    {
        Global.isUIShow = true;
        camRotate.enabled = false;
        introductionCG.DOFade(1, 0.3f);
        introductionCG.blocksRaycasts = true;
    }

    /// <summary>
    /// 楼房介绍关闭按钮
    /// </summary>
    public void ButtonCloseIntroductionOnClick()
    {
        Global.isUIShow = false;
        camRotate.enabled = true;
        introductionCG.DOFade(0, 0.3f);
        introductionCG.blocksRaycasts = false;
    }

    /// <summary>
    /// 成功案例按钮
    /// </summary>
    public void ButtonCasesOnClick()
    {
        Global.isUIShow = true;
        camRotate.enabled = false;
        casesCG.DOFade(1, 0.3f);
        casesCG.blocksRaycasts = true;
    }
	
    public void ButtonRightOnClick()
    {
        if (index < 3)
        {
            index++;
            mImage_Building.sprite = mSprite_bulidings[index];
            mText_BuildingType.text = Configuration.GetContent("Case", "Case" + index);
        }
    }

    public void ButtonLeftOnClick()
    {
        if (index > 0) 
        {
            index--;
            mImage_Building.sprite = mSprite_bulidings[index];
            mText_BuildingType.text = Configuration.GetContent("Case", "Case" + index);
        }
    }

    /// <summary>
    /// 成功案例关闭按钮
    /// </summary>
    public void ButtonCloseCaseOnClick()
    {
        Global.isUIShow = false;
        camRotate.enabled = true;
        casesCG.DOFade(0, 0.3f);
        casesCG.blocksRaycasts = false;
    }

    /// <summary>
    /// 日夜循环按钮
    /// </summary>
    public void ButtonDayNightOnClick()
    {
        dayNightCG.DOFade(1, 0.3f);
        dayNightCG.blocksRaycasts = true;
    }

    public void ButtonDaySwitchOnClick()
    {
        sunLight.Day();
        sunLight.turnDay = true;
        dayNightCG.DOFade(0, 0.3f);
        dayNightCG.blocksRaycasts = false;
    }

    public void ButtonNightSwitchOnClick()
    {
        sunLight.Night();
        sunLight.turnNight = true;
        dayNightCG.DOFade(0, 0.3f);
        dayNightCG.blocksRaycasts = false;
    }

    /// <summary>
    /// 切换室内场景按钮
    /// </summary>
    public void ButtonEnterHouseOnClick()
    {
        camRotate.enabled = false;
        StartCoroutine(Mask());
        Camera.main.GetComponent<CameraMovement>().DoBezierPath();
    }

    IEnumerator Mask()
    {
        yield return new WaitForSeconds(1.2f);
        maskCG.DOFade(1, 0.7f);
    }

    //bool isPressEffect = false;

    //public void ButtonEffectSelectionOnClick()
    //{
    //    if (!isPressEffect)
    //    {
    //        effectCG.DOFade(1, 0.3f);
    //        effectCG.blocksRaycasts = true;
    //        isPressEffect = true;
    //    }
    //    else
    //    {
    //        effectCG.DOFade(0, 0.3f);
    //        effectCG.blocksRaycasts = false;
    //        isPressEffect = false;
    //    }
    //}
}
