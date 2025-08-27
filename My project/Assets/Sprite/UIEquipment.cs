using UnityEngine;

public class UIEquipment : MonoBehaviour
{
    public string itemName;
    public UIBagButton bagButton;
    public void OnClick()
    {
        bagButton = GameObject.Find("Canvas").GetComponent<UIBagButton>();
        GameManager.instance.player.DisChargeHandsEquipment(); 
        bagButton.ReFreshBag();
        Destroy(gameObject);
    }
}
