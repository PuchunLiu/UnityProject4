using UnityEngine;
using UnityEngine.UI;

public class UISkillButton : MonoBehaviour
{
    public GameObject skillUI;
    public Text skillPointsString;
    public UISkillCanvas[] skillButtons;
    public void OpenSkill()
    {
        skillUI.SetActive(true);
        SetButtonStudy();
        skillPointsString.text = "¼¼ÄÜµã:" + GameManager.instance.skillpoints.ToString();
    }

    public void CloseSkill()
    {
        skillUI.SetActive(false);
    }

    public void SetButtonStudy()
    {
        if (GameManager.instance.isHPUP)
        {
            skillButtons[0].SetIsStudy();
        }
        if (GameManager.instance.isATKUP)
        {
            skillButtons[1].SetIsStudy();
        }
        if (GameManager.instance.isSpeedUP)
        {
            skillButtons[2].SetIsStudy();
        }
        if (GameManager.instance.isSkillUP)
        {
            skillButtons[3].SetIsStudy();
        }
        if (GameManager.instance.isEffectUP)
        {
            skillButtons[4].SetIsStudy();
        }
        if (GameManager.instance.isCanSkill1)
        {
            skillButtons[5].SetIsStudy();
        }
        if (GameManager.instance.isCanSkill2)
        {
            skillButtons[6].SetIsStudy();
        }
        if (GameManager.instance.isCanSkill3)
        {
            skillButtons[7].SetIsStudy();
        }
        if (GameManager.instance.isCanSkill4)
        {
            skillButtons[8].SetIsStudy();
        }
        if (GameManager.instance.isCanSkill5)
        {
            skillButtons[9].SetIsStudy();
        }
    }
}
