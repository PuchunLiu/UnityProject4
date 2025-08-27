using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartSceneCanvas : MonoBehaviour
{
    public GameObject loadGamePanel;
    private void Start()
    {
        SoundManager.instance.PlayBGMSound(0);
    }

    public void StartGame()
    {
        //Debug.Log("button:Start");
        loadGamePanel.SetActive(true);
        SceneManager.LoadScene(1);
        SoundManager.instance.PlayBGMSound(1);
    }

    public void LoadGame()
    {
        loadGamePanel.SetActive(true);
        GameManager.instance.LoadGame();
        SceneManager.LoadScene(1);
        SoundManager.instance.PlayBGMSound(1);

    }

    public void QuitGame()
    {
        //Debug.Log("button:Quit");
        Application.Quit();
    }
}
