using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string itemName;
    public string itemMessge;
    public int price;
    public Text priceText;
    public UIShowMessage UIitemMessage;

    private void Awake()
    {
        priceText.text = price.ToString();
        UIitemMessage = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIBagButton>().UIitemMessage;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIitemMessage.ShowMessage(itemName, itemMessge);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIitemMessage.HideMessage();
    }

    public void BuyItem()
    {
        if (itemName == "生命药水")
        {
            if (GameManager.instance.coin >= price)
            {
                GameManager.instance.coin -= price;
                GameManager.instance.HPBottleNum += 1;
            }
        }
        else if (itemName == "上级生命药水")
        {
            if (GameManager.instance.coin >= price)
            {
                GameManager.instance.coin -= price;
                GameManager.instance.HHPBottleNum += 1;
            }
        }
        else if (itemName == "铁剑")
        {
            if (GameManager.instance.coin >= price)
            {
                GameManager.instance.coin -= price;
                GameManager.instance.Sword1Num = 1;
            }
        }
        else if (itemName == "宝剑")
        {
            if (GameManager.instance.coin >= price)
            {
                GameManager.instance.coin -= price;
                GameManager.instance.Sword2Num = 1;
            }
        }
    }
}
