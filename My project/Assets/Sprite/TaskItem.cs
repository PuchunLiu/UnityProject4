using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour
{
    public string taskName;
    public string taskDescription;
    public Text taskText;

    private void Start()
    {
        taskText = GameObject.Find("TaskMessage").GetComponent<Text>();
    }
    public void OnClick()
    {
        taskText.text = taskDescription;
    }
}
