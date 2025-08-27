using UnityEngine;

public class ItemCreate : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 force = new Vector2(0f, 150f);
    public bool canEat = false;

    private void Start()
    {
        force.x = Random.Range(-150f, 150f);
        rb.AddForce(force);
        Invoke("Eat", 0.2f);
    }

    public void Eat()
    {
        canEat = true;
    }
}
