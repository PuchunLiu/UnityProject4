using UnityEngine;

public class EnemyTer : MonoBehaviour
{
    public EnemyBased enemyBased;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            enemyBased.FindPlayer(collision.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyBased.PlayerOut();
        }
    }
}
