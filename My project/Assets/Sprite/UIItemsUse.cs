using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItemsUse : MonoBehaviour
{
    public Text Button1Text;
    public Text Button2Text;
    public string ItemName;
    public RectTransform rect;
    public UIBagItem bagItem;

    private void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        rect.position = Input.mousePosition;
    }

    public void ItemMessage(string text1, string text2, string name, UIBagItem item)
    {
        Button1Text.text = text1;
        Button2Text.text = text2;
        ItemName = name;
        bagItem = item;
        ShowMessage();
    }

    public void Button1Click()
    {
        bagItem.Button1Click();
        HideMessage();
    }

    public void Button2Click()
    {
        bagItem.Button2Click();
        HideMessage();
    }

    public void ShowMessage()
    {
        gameObject.SetActive(true);
    }

    public void HideMessage()
    {
        gameObject.SetActive(false);
    }
}
