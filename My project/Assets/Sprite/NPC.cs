using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject Robj;
    public bool playerEnter = false;
    public bool hasTalking = false;
    public GameObject talkCanvas;

    void Update()
    {
        if (playerEnter && !hasTalking)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Robj.SetActive(false);
                talkCanvas.SetActive(true);
                hasTalking = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!hasTalking)
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
            if (!hasTalking)
            {
                Robj.SetActive(false);
                playerEnter = false;
            }
        }
    }
}
