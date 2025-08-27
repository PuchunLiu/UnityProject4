using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Boss : EnemyBased
{
    public int combo = 1;
    public GameObject attack2Box;
    public float upOffset = 1f;
    public UITaskButton uiTask;
    public override void AttackUpdate()
    {
        

        if (!isAttack && canAttack && !isGetHit)
        {
            canAttack = false;
            isAttack = true;
            combo = Random.Range(0, 10);
            //combo = 2;
            if (combo < 7)
            {
                ani.SetTrigger("Attack");
                Invoke("AttackBoxActive", attackBoxTime);
            }
            else
            {
                ani.SetTrigger("Attack2");
                ani.SetBool("Dominating", true);
                Invoke("Attack2BoxActive", 0.5f);
                SoundManager.instance.PlayShortSound(4);
            }   
            Invoke("SetisAttack", attackTime);
            Invoke("SetCanAttack", attackCoolDown);
            Invoke("PlayAttackSound", 0.4f);
        }
           
        if (!isAttack)
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

    public override void AttackExit()
    {
        base.AttackExit();
        ani.SetBool("Dominating", false);
        combo = 0;
    }

    public void Attack2BoxActive()
    {
        if ((currentState != EnemyState.Death))
        {
            Transform t = Instantiate(attack2Box.transform, transform);
            t.localScale = new Vector3((isRight ? -1 : 1) * t.localScale.x, t.localScale.y, t.localScale.z);
            t.position = player.transform.position + new Vector3(0f, upOffset, 0f);
            t.GetComponent<BossAttack2>().SetDL(50, 2);
        }
    }

    public override void GetHit(int damage, int hitLevel)
    {
        if (currentState != EnemyState.Death)
        {
            if(combo != 2)
            {
                ChangeCurrentState(EnemyState.GetHit);
            }
            
            HPNow -= damage;
            Transform t = Instantiate(damageNum.transform, Canvas);
            if (getHitBox.transform.position.x > transform.position.x)
            {
                t.localScale = new Vector3(-t.localScale.x, t.localScale.y, t.localScale.z);
                Transform child = t.GetChild(0).GetChild(0);
                child.localScale = new Vector3(-child.localScale.x, child.localScale.y, child.localScale.z);
            }
            if (HPNow <= 0)
            {
                ChangeCurrentState(EnemyState.Death);
                if(GameManager.instance.isGetTask1 && !GameManager.instance.task1IsFinished)
                {
                    uiTask.Task1Finish();
                }
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
                    if (getHitBox.transform.position.x <= transform.position.x)
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
