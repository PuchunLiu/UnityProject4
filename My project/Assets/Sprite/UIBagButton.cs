using UnityEngine;
using UnityEngine.UI;

public class UIBagButton : MonoBehaviour
{
    public GameObject bagUI;
    public Transform equipmentWeapon;
    public Text bagPlayerHP;
    public Text bagPlayerATK;
    public Text bagPlayerSpeed;
    public Player player;
    public UIShowMessage UIitemMessage;
    public UIItemsUse ItemUseUI;
    public Transform bagParent;
    public GameObject HPBottleObj;
    public GameObject HHPBottleObj;
    public GameObject Sword1obj;
    public GameObject Sword2obj;
    public GameObject EWeaponP;
    public GameObject EWeapon2P;

    public void SetBagUIActive()
    {
        bagUI.SetActive(true);
        ReFreshBag();
    }

    public void SetBagText()
    {
        bagPlayerHP.text = "HP:" + player.playHpNow.ToString() + "/" + player.playHpMax.ToString();
        bagPlayerATK.text = "ATK:" + player.ATK.ToString();
        bagPlayerSpeed.text = "Speed:" + player.moveSpeed.ToString();
    }

    public void SetBagUIHide()
    {
        for (int i = 0; i < bagParent.childCount; i++) 
        {
            Destroy(bagParent.GetChild(i).gameObject);
        }
        if (equipmentWeapon.childCount >= 2)
        {
            Destroy(equipmentWeapon.GetChild(1).gameObject);
        }
        bagUI.SetActive(false);
        ItemUseUI.gameObject.SetActive(false);
    }

    public void ADDBagItems()
    {
        if (GameManager.instance.HPBottleNum > 0)
        {
            GameObject HPBottleItem = Instantiate(HPBottleObj, bagParent);
            HPBottleItem.GetComponent<UIBagItem>().ItemCreate(GameManager.instance.HPBottleNum, this);
        }
        if (GameManager.instance.HHPBottleNum > 0)
        {
            GameObject HHPBottleItem = Instantiate(HHPBottleObj, bagParent);
            HHPBottleItem.GetComponent<UIBagItem>().ItemCreate(GameManager.instance.HHPBottleNum, this);
        }
        if(GameManager.instance.Sword1Num > 0)
        {
            GameObject sword1Item = Instantiate(Sword1obj, bagParent);
            sword1Item.GetComponent<UIBagItem>().ItemCreate(GameManager.instance.Sword1Num, this);
        }
        if (GameManager.instance.Sword2Num > 0)
        {
            GameObject sword2Item = Instantiate(Sword2obj, bagParent);
            sword2Item.GetComponent<UIBagItem>().ItemCreate(GameManager.instance.Sword2Num, this);
        }
    }

    public void ReFreshBag()
    {
        for (int i = 0; i < bagParent.childCount; i++)
        {
            Destroy(bagParent.GetChild(i).gameObject);
        }
        if (equipmentWeapon.childCount >= 2)
        {
            Destroy(equipmentWeapon.GetChild(1).gameObject);
        } 
        ADDBagItems();
        ADDEquipment();
        SetBagText();
    }

    public void ADDEquipment()
    {
        if (GameManager.instance.handsisEquipment)
        {
            if(GameManager.instance.handsEquipmentName == "Ìú½£")
            {
                Instantiate(EWeaponP, equipmentWeapon);
            }
            else if (GameManager.instance.handsEquipmentName == "±¦½£")
            {
                Instantiate(EWeapon2P, equipmentWeapon);
            }
        }
    }
}
