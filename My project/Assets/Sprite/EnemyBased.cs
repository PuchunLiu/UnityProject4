using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyState
{
    Idle,
    Patrol,
    Pursuit,
    Attack,
    GetHit,
    Death
}

public class EnemyBased : MonoBehaviour
{
    [Header("EnemyIdle")]
    public float idleTime = 2f;
    public SpriteRenderer spriteRenderer;
    public Animator ani;
    public int HPMax = 100;
    public int HPNow = 100;

    [Header("EnemyPatrol")]
    public EnemyState currentState = EnemyState.Patrol;
    public Transform left;
    public Transform right;
    public bool isRight = false;
    public Rigidbody2D rb;
    public float speed = 1f;
    public bool canMove = true;

    [Header("EnemyPursuit")]
    public GameObject player;

    [Header("EnemyAttack")]
    public float enemyAttackDis = 1f;
    public bool isAttack = false;
    public float attackCoolDown = 3f;
    public bool canAttack = true;
    public float attackTime = 1.5f;
    public Transform attackBox;
    public int hitLevel = 1;
    public int damage = 10;
    public float attackBoxTime = 0.4f;

    [Header("EnemyGetHit")]
    public float getHitTime = 0.17f;
    public bool isGetHit = false;
    public float getHitAddForce = 500f;
    public GameObject EnemyandPosition;
    public Slider hpSlider;
    public Text hpText;
    public GameObject damageNum;
    public Transform Canvas;
    public GameObject getHitBox;

    public GameObject item1;
    public GameObject item2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        HPNow = HPMax;
        PatrolEnter();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        hpSlider.value = (float)HPNow / HPMax;
        hpText.text = HPNow.ToString() + "/" + HPMax.ToString();
        switch (currentState)
        {
            case EnemyState.Idle:
                IdleUpdate();
                break;
            case EnemyState.Patrol:
                PatrolUpdate();
                break;
            case EnemyState.Pursuit:
                PursuitUpdate();
                break;
            case EnemyState.Attack:
                AttackUpdate();
                break;
            case EnemyState.GetHit:
                GetHitUpdate();
                break;
            case EnemyState.Death:
                DeathUpdate();
                break;
        }
    }

    public virtual void FixedUpdate()
    {
        if(currentState != EnemyState.GetHit)
        {
            if (canMove)
            {
                rb.linearVelocityX = (isRight ? 1 : -1) * speed;
            }
            else
            {
                rb.linearVelocityX = 0;
            }
        }
        
    }

    public virtual void ChangeCurrentState(EnemyState state)
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                IdleExit();
                break;
            case EnemyState.Patrol:
                PatrolExit();
                break;
            case EnemyState.Pursuit:
                PursuitExit();
                break;
            case EnemyState.Attack:
                AttackExit();
                break;
            case EnemyState.GetHit:
                GetHitExit();
                break;
            case EnemyState.Death:
                DeathExit();
                break;
        }
        currentState = state;
        switch (currentState)
        {
            case EnemyState.Idle:
                IdleEnter();
                break;
            case EnemyState.Patrol:
                PatrolEnter();
                break;
            case EnemyState.Pursuit:
                PursuitEnter();
                break;
            case EnemyState.Attack:
                AttackEnter();
                break;
            case EnemyState.GetHit:
                GetHitEnter();
                break;
            case EnemyState.Death:
                DeathEnter();
                break;
        }
    }

    #region
    public virtual void IdleEnter()
    {
        canMove = false;
        ani.SetBool("isRun", false);
        Invoke("ChangeIdletoPatrolState", idleTime);
    }

    public virtual void IdleUpdate()
    {

    }

    public virtual void IdleExit()
    {
        CancelInvoke("ChangeIdletoPatrolState");
    }

    public virtual void PatrolEnter()
    {
        canMove = true;
        ani.SetBool("isRun", true);
    }

    public virtual void PatrolUpdate()
    {
        if(transform.position.x <= left.position.x && !isRight)
        {
            ChangeCurrentState(EnemyState.Idle);
        }
        else if(transform.position.x >= right.position.x && isRight)
        {
            ChangeCurrentState(EnemyState.Idle);
        }
    }

    public virtual void PatrolExit()
    {

    }

    public virtual void PursuitEnter()
    {
        canMove = true;
        ani.SetBool("isRun", true);
    }

    public virtual void PursuitUpdate()
    {
        if(player != null)
        {
            if(player.transform.position.x <= transform.position.x)
            {
                isRight = false;
                spriteRenderer.flipX = isRight;
            }
            else
            {
                isRight = true;
                spriteRenderer.flipX = isRight;
            }
            if (Mathf.Abs(player.transform.position.x - transform.position.x) <= enemyAttackDis)
            {
                if(player.transform.position.x <= transform.position.x && !isRight)
                {
                    ChangeCurrentState(EnemyState.Attack);
                }
                else if (player.transform.position.x > transform.position.x && isRight)
                {
                    ChangeCurrentState(EnemyState.Attack);
                }
            }
        }
        else
        {
            ChangeCurrentState(EnemyState.Patrol);
        }
    }

    public virtual void PursuitExit()
    {

    }

    public virtual void AttackEnter()
    {
        canMove = false;
        ani.SetBool("isRun",false);
    }

    public virtual void AttackUpdate() 
    {
        if (!isAttack  && canAttack && !isGetHit)
        {
            ani.SetTrigger("Attack");
            isAttack = true;
            canAttack = false;
            Invoke("AttackBoxActive", attackBoxTime);
            Invoke("SetisAttack", attackTime);
            Invoke("SetCanAttack", attackCoolDown);
            Invoke("PlayAttackSound", 0.4f);
        }
        if(!isAttack)
        {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) > enemyAttackDis)
            {
                ChangeCurrentState(EnemyState.Pursuit);
            }
            else
            {
                if (player.transform.position.x >= transform.position.x && !isRight)
                {
                    ChangeCurrentState(EnemyState.Pursuit);
                }
                else if (player.transform.position.x < transform.position.x && isRight)
                {
                    ChangeCurrentState(EnemyState.Pursuit);
                }
            }
        }  
    }

    public virtual void AttackExit() { }

    public virtual void GetHitEnter()
    {
        canMove=false;
        CancelInvoke("ChangeStatetoPursuit");
        CancelInvoke("AttackBoxActive");
        ani.SetBool("getHit", true);
        ani.SetBool("isRun", false);
        Invoke("ChangeStatetoPursuit", getHitTime);
    }

    public virtual void GetHitUpdate() 
    {
        if (!isGetHit)
        {
            isGetHit = true;
        }
    }

    public virtual void GetHitExit() 
    {
        isGetHit = false;
        rb.linearVelocityX = 0;
        ani.SetBool("getHit", false);
    }

    public virtual void DeathEnter()
    {
        canMove = false;
        CancelInvoke("ChangeStatetoPursuit");
        CancelInvoke("AttackBoxActive");
        ani.SetTrigger("isDead");
        ani.SetBool("isRun", false);
        GameManager.instance.coin += Random.Range(5, 10);
        GameManager.instance.skillpoints += 1;
        CreateItems();
        Destroy(EnemyandPosition, 2f);
    }

    public virtual void DeathUpdate() 
    {
        
    }

    public virtual void DeathExit() { }
    #endregion

    public virtual void ChangeIdletoPatrolState()
    {
        isRight = !isRight;
        spriteRenderer.flipX = isRight;
        ChangeCurrentState(EnemyState.Patrol);
    }

    public virtual void FindPlayer(GameObject player)
    {
        if(currentState != EnemyState.Death)
        {
            this.player = player;
            ChangeCurrentState(EnemyState.Pursuit);
        }  
    }

    public virtual void PlayerOut()
    {
        if (currentState != EnemyState.Death)
        {
            ChangeCurrentState(EnemyState.Patrol);
            player = null;
        }
    }

    public virtual void SetisAttack()
    {
        isAttack = false;
    }

    public virtual void SetCanAttack()
    {
        canAttack = true;
    }

    public virtual void GetHit(int damage, int hitLevel)
    {

        if(currentState != EnemyState.Death)
        {
            ChangeCurrentState(EnemyState.GetHit);
            //Debug.Log(damage);
            HPNow -= damage;
            Transform t = Instantiate(damageNum.transform, Canvas);
            t.GetComponent<DamageNum>().SetDamageNum(damage);
            if (getHitBox.transform.position.x > transform.position.x)
            {
                t.localScale = new Vector3(-t.localScale.x, t.localScale.y, t.localScale.z);
                Transform child = t.GetChild(0).GetChild(0);
                child.localScale = new Vector3(-child.localScale.x, child.localScale.y, child.localScale.z);
            }
            if (HPNow <= 0)
            {
                ChangeCurrentState(EnemyState.Death);
            }
            else
            {
                if (hitLevel == 1)
                {
                    if (getHitBox.transform.position.x <= transform.position.x)
                    {
                        //rb.AddForce(new Vector2(getHitAddForce, 0));
                        rb.linearVelocityX = getHitAddForce;
                    }
                    else
                    {
                        //rb.AddForce(new Vector2(-getHitAddForce, 0));
                        rb.linearVelocityX = -getHitAddForce;
                    }
                }
                else if (hitLevel == 2)
                {
                    if (player != null)
                    {
                        if (player.transform.position.x <= transform.position.x)
                        {
                            //rb.AddForce(new Vector2(getHitAddForce, 0));
                            rb.linearVelocityX = getHitAddForce * hitLevel;
                        }
                        else
                        {
                            //rb.AddForce(new Vector2(-getHitAddForce, 0));
                            rb.linearVelocityX = -getHitAddForce * hitLevel;
                        }
                    }
                }
                }
        }  
    }

    public virtual void ChangeStatetoPursuit()
    {
        if(currentState != EnemyState.Death)
        {
            ChangeCurrentState(EnemyState.Pursuit);
        }
        
    }

    public virtual void AttackBoxActive()
    {
        if ((currentState != EnemyState.Death))
        {
            Transform t = Instantiate(attackBox.transform, transform);
            t.localScale = new Vector3((isRight ? -1 : 1) * t.localScale.x, t.localScale.y, t.localScale.z);
            t.GetComponent<EnemyAttackBox>().SetDamage(damage, hitLevel);
        }
    }

    public virtual void PlayAttackSound()
    {
        SoundManager.instance.PlayShortSound(1);
    }

    public virtual void CreateItems()
    {
        int random = Random.Range(1, 3);
        for (int i = 0; i < random; i++)
        {
            Instantiate(item1, transform.position, transform.rotation);
        }
        random = Random.Range(0, 100);
        if(random >= 90)
        {
            Instantiate(item2, transform.position, transform.rotation);
        }
    }
}
