using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject Robj;
    public bool isOpen = false;
    public bool playerEnter = false;
    public GameObject HHPBottle;
    public GameObject Sword1;
    public Sprite openedChest;
    public SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerEnter)
        {
            if (!isOpen)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    isOpen = true;
                    Instantiate(HHPBottle, transform.position, Quaternion.identity);
                    Instantiate(Sword1, transform.position, Quaternion.identity);
                    Robj.SetActive(false);
                    sr.sprite = openedChest;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isOpen)
            {
                Robj.SetActive(true);
                playerEnter = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isOpen)
            {
                Robj.SetActive(false);
                playerEnter = false;
            }
        }
    }
}
