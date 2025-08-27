using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{
    public int damage = 5;
    public float destroyTime = 0.2f;
    public int hitLevel = 1;
    public bool isNeedDestroy = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isNeedDestroy)
        {
            Destroy(gameObject, destroyTime);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("TriggerActive");
        //Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.GetHit(damage, hitLevel);
            
        }
    }

    public void SetDamage(int damage, int hitLevel)
    {
        this.damage = damage;
        this.hitLevel = hitLevel;
    }
}
