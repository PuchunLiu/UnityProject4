using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 10f;
    public bool isRight;
    public bool moveEnd = false;
    public Vector3 MoveDir = new Vector3(1, 0, 0);
    public float changeTime = 3f;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //player = GameObject.FindWithTag("Player");
        Invoke("ChangeDir", changeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveEnd)
        {
            MoveDir = (player.transform.position - transform.position).normalized;
        }
        else
        {
            MoveDir = (isRight ? 1 : -1) * new Vector3(1, 0, 0);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = speed * MoveDir;
    }
    
    public void ChangeDir()
    {
        moveEnd = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (moveEnd)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetParameter(bool isRight, GameObject player)
    {
        this.isRight = isRight;
        this.player = player;
    }
}
