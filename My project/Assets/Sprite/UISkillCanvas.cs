using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISkillCanvas : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UIShowMessage itemMessage;
    public string itemName;
    public string message;
    public Image itemImage;
    public bool isStudy = false;
    public Text skillPointText;
    public Player player;
    public string boolString;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemMessage.ShowMessage(itemName, message);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemMessage.HideMessage();
    }

    public void OnSkillButtonClick()
    {
        if(GameManager.instance.skillpoints >= 1 && !isStudy)
        {
            GameManager.instance.skillpoints -= 1;
            itemImage.color = Color.green;
            isStudy = true;
            skillPointText.text = "¼¼ÄÜµã:" + GameManager.instance.skillpoints.ToString();
            player.SetAttributesBool(boolString);
        }
    }

    public void SetIsStudy()
    {
        isStudy = true;
        itemImage.color = Color.green;
    }
}
