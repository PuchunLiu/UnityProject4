using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 10f;
    public bool isRight;

    private void Awake()
    {
        isRight = transform.parent.GetComponent<Enemy3>().isRight;
    }

    void Start()
    {
        rb.linearVelocityX = isRight ? speed : -speed;
    }
        
}
