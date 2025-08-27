using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements.Experimental;
using Unity.VisualScripting;

public class UIBagItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text itemNumText;
    public string itemName;
    public string itemMessge;
    public Image itemImage;
    public UIShowMessage UIitemMessage;
    public UIItemsUse ItemUseUI;
    public int itemNum;
    public UIBagButton bagButton;
    public GameObject EWeapon1Prefab;
    public GameObject EWeapon2Prefab;

    private void Awake()
    {
        UIitemMessage = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIBagButton>().UIitemMessage;
        ItemUseUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIBagButton>().ItemUseUI;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ItemUseUI.HideMessage();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIitemMessage.ShowMessage(itemName, itemMessge);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIitemMessage.HideMessage();
    }

    public void ItemOnClick()
    {
        ItemUseUI.ItemMessage("使用", "丢弃", itemName, this);
        UIitemMessage.HideMessage();
    }   

    public void ItemCreate(int num, UIBagButton bag)
    {
        itemNumText.text = num.ToString();
        itemNum = num;
        bagButton = bag;
    }

    public void Button1Click()
    {      
        itemNum -= 1;
        itemNumText.text = itemNum.ToString();
        
        if (itemName == "生命药水")
        {
            GameManager.instance.player.ADDHP(50);
            GameManager.instance.HPBottleNum = itemNum;
        }
        else if (itemName == "上级生命药水")
        {
            GameManager.instance.player.ADDHP(75);
            GameManager.instance.HPBottleNum = itemNum;
        }
        else if (itemName == "铁剑")
        {
            if(bagButton.equipmentWeapon.childCount >= 2)
            {
                Destroy(bagButton.equipmentWeapon.GetChild(1).gameObject);
            }
            GameManager.instance.player.PlayerHandsEquipment(itemName);
            GameManager.instance.Sword1Num = itemNum;
            Instantiate(EWeapon1Prefab, GameObject.Find("EWeapon").transform);
        }
        else if (itemName == "宝剑")
        {
            if (bagButton.equipmentWeapon.childCount >= 2)
            {
                Destroy(bagButton.equipmentWeapon.GetChild(1).gameObject);
            }
            GameManager.instance.player.PlayerHandsEquipment(itemName);
            GameManager.instance.Sword2Num = itemNum;
            Instantiate(EWeapon2Prefab, GameObject.Find("EWeapon").transform);
        }
        CheckNum();
        bagButton.SetBagText();
        bagButton.ReFreshBag();
    }

    public void Button2Click()
    {
        itemNum -= 1;
        
        itemNumText.text = itemNum.ToString();
        if(itemName == "生命药水")
        {
            GameManager.instance.HPBottleNum = itemNum;
        }
        else if (itemName == "上级生命药水")
        {
            GameManager.instance.HHPBottleNum = itemNum;
        }
        else if (itemName == "铁剑")
        {
            GameManager.instance.Sword1Num = itemNum;
        }
        else if (itemName == "宝剑")
        {
            GameManager.instance.Sword2Num = itemNum;
        }
        CheckNum();
        bagButton.ReFreshBag();
    }
    
    public void CheckNum()
    {
        if(itemNum < 1)
        {
            Destroy(gameObject);
        }
    }
}
