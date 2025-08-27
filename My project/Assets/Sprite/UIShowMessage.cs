using UnityEngine;
using UnityEngine.UI;

public class UIShowMessage : MonoBehaviour
{
    public Text itemText;
    public Text messageText;
    public RectTransform rect;
    
    public void ShowMessage(string itemName, string message)
    {
        itemText.text = itemName;
        messageText.text = message;
        gameObject.SetActive(true);
    }

    public void HideMessage()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
    }

    private void Update()
    {
        rect.position = Input.mousePosition;
    }
}
