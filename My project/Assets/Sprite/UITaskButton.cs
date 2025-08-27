using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UITaskButton : MonoBehaviour
{
    public GameObject taskUI;
    public Transform taskContent;
    public Text taskText;
    public GameObject task1Prefab;
    public GameObject taskFinishTip;
    public void SetTaskUIActive()
    {
        taskUI.SetActive(true);
        if (GameManager.instance.isGetTask1 && !GameManager.instance.task1IsFinished)
        {
            Instantiate(task1Prefab, taskContent);
        }
        taskText.text = "";
    }

    public void SetTaskUIHide()
    {
        taskUI.SetActive(false);
        for (int i = 0; i < taskContent.childCount; i++)
        {
            Destroy(taskContent.GetChild(i).gameObject);
        }
    }

    public void Task1Finish()
    {
        if (!GameManager.instance.task1IsFinished)
        {
            GameManager.instance.task1IsFinished = true;
            GameManager.instance.Sword2Num = 1;
            taskFinishTip.SetActive(true);
            Invoke("HideFinishTip", 5f);
        }
    }

    public void HideFinishTip()
    {
        taskFinishTip.SetActive(false);
    }
}
