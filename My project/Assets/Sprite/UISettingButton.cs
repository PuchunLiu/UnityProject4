using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISettingButton : MonoBehaviour
{
    public GameObject settingUI;
    public GameObject saveCanvas;

    //public Button settingButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSetting()
    {
        settingUI.SetActive(true);
    }

    public void CloseSetting()
    {
        settingUI.SetActive(false);
    }

    public void OnClickBackMain()
    {
        SceneManager.LoadScene(0);
    }

    public void SaveGameData()
    {
        GameManager.instance.SaveGame();
        saveCanvas.SetActive(true);
        Invoke("HideSaveCanvas", 1f);
    }

    public void HideSaveCanvas()
    {
        saveCanvas.SetActive(false);
    }
}
