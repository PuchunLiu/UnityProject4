using UnityEngine;
using UnityEngine.UI;

public class UINPCTalkCanvas : MonoBehaviour
{
    public GameObject playerSprite;
    public GameObject NPCSprite;
    public Text nameText;
    public Text dialogueText;
    public string[] dialogueLines;
    public int currentLine = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnTalkClick();
        }
    }


    private void Awake()
    {
        OnTalkClick();
    }

    public void Dialogue(int current)
    {
        if (current % 2 == 0)
        {
            nameText.text = "NPC";
            playerSprite.SetActive(false);
            NPCSprite.SetActive(true);
        }
        else
        {
            nameText.text = "Íæ¼Ò";
            playerSprite.SetActive(true);
            NPCSprite.SetActive(false);
        }
        dialogueText.text = dialogueLines[current];
    }
    
    public void OnTalkClick()
    {
        if (currentLine < dialogueLines.Length)
        {
            Dialogue(currentLine);
            currentLine++;
        }
        else
        {
            gameObject.SetActive(false);
            GameManager.instance.isGetTask1 = true;
        }
    }
}
