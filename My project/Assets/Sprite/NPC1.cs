using UnityEngine;
using UnityEngine.UI;

public class NPC1 : MonoBehaviour
{
    public GameObject Robj;
    public bool playerEnter = false;
    public GameObject shopCanvas;
    public Text playerCoins;

    void Update()
    {
        playerCoins.text = GameManager.instance.coin.ToString();
        if (playerEnter)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                shopCanvas.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Robj.SetActive(true);
            playerEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Robj.SetActive(false);
            playerEnter = false;
        }
    }

    public void HideShopCanvas()
    {
        shopCanvas.SetActive(false);
    }
}
