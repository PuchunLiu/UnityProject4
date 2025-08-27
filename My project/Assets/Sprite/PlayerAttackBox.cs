using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{
    public int damage = 10;
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
        if (other.CompareTag("Enemy"))
        {
            EnemyBased enemy = other.GetComponent<EnemyBased>();
            enemy.getHitBox = gameObject;
            enemy.GetHit(damage, hitLevel);
            
        }
    }

    public void SetDamage(int damage, int hitLevel)
    {
        this.damage = damage;
        this.hitLevel = hitLevel;
    }
}
