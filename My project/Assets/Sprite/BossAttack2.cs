using UnityEngine;

public class BossAttack2 : MonoBehaviour
{
    public GameObject hitBox;
    public int damage;
    public int hitLevel;

    private void Start()
    {
        Invoke("SetHitBox", 0.5f);
        Destroy(gameObject, 1f);
    }

    public void SetHitBox()
    {
        hitBox.SetActive(true);
        hitBox.GetComponent<EnemyAttackBox>().SetDamage(damage, hitLevel);
    }

    public void SetDL(int damage, int hitLevel)
    {
        this.damage = damage;
        this.hitLevel = hitLevel;
    }
}
