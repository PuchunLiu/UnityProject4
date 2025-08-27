using System;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    Idle,
    Run,
    Jump,
    Fall,
    Dash,
    Attack,
    Skill1,
    GetHit,
    Block,
    Dead
}

public class Player : MonoBehaviour
{
    public PlayerState currentState;
    public PlayerState lastState;
    [Header("Player Move")]
    public Rigidbody2D body;
    public float xInput;
    public int moveSpeed = 8;
    public Animator ani;
    public bool isRight = true;
    public SpriteRenderer spriteRenderer;

    [Header("Player Jump")]
    public float jumpForce = 400f; 
    public LayerMask groundLayer;
    public bool isGround = true;
    public bool canJump2 = false;

    [Header("Player Dash")]
    public float dashSpeed = 100f;
    public float dashTime = 0.2f;
    public float dashCoolDown = 0.5f;
    public bool isDashing = false;
    public bool canDash = true;
    public GameObject dashPrefab;

    [Header("Player Attack")]
    public int combo = 1;
    public int ATK = 10;
    public int skillATK = 20;
    public Transform attackbox1;
    public Transform attackbox2;
    public Transform attackbox3;
    public bool isAttackOrSkill = false;
    public bool canSkill1 = false;

    [Header("Player Dead")]
    public int playHpMax = 100;
    public int playHpNow = 100;
    public bool isDead = false;
    public Slider hpSlider;
    public Text hpText;

    public float hitAddForce = 600f;
    public float getHitTime = 0.5f;
    public GameObject skillSword;
    public int HPBase = 100;
    public int ATKBase = 10;
    public int SpeedBase = 8;
    public int skillATKBase = 20;
    public bool isATKUP = false;
    public bool isHPUP = false;
    public bool isSpeedUP = false;
    public bool isSkillUP = false;
    public bool isEffectUP = false;

    public bool isCanSkill1 = false;
    public bool isCanSkill2 = false;
    public bool isCanSkill3 = false;
    public bool isCanSkill4 = false;
    public bool isCanSkill5 = false;

    public bool handsisEquipment = false;
    public string currentHandsName = "";
    public bool isBlocked = false;

    private void Awake()
    {
        GameManager.instance.player = this;
        isHPUP = GameManager.instance.isHPUP;
        isATKUP = GameManager.instance.isATKUP;
        isSpeedUP = GameManager.instance.isSpeedUP;
        isSkillUP = GameManager.instance.isSkillUP;
        isEffectUP = GameManager.instance.isEffectUP;
        isCanSkill1 = GameManager.instance.isCanSkill1;
        isCanSkill2 = GameManager.instance.isCanSkill2;
        isCanSkill3 = GameManager.instance.isCanSkill3;
        isCanSkill4 = GameManager.instance.isCanSkill4;
        isCanSkill5 = GameManager.instance.isCanSkill5;
        handsisEquipment = GameManager.instance.handsisEquipment;
        currentHandsName = GameManager.instance.handsEquipmentName;
        SetAttributes();
        DoPlayerHandsEquipment(currentHandsName);
        transform.position = GameManager.instance.playerPos;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playHpNow = playHpMax;
        hpSlider.value = (float)playHpNow / playHpMax;
        hpText.text = playHpNow.ToString() + "/" + playHpMax.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        CheckisGround();
        StateUpDate();
    }

    private void FixedUpdate()
    {
        PlayerMoveFix();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HPBottle"))
        {
            if (collision.gameObject.GetComponent<ItemCreate>().canEat)
            {
                GameManager.instance.HPBottleNum += 1;
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("HHPBottle"))
        {
            if (collision.gameObject.GetComponent<ItemCreate>().canEat)
            {
                GameManager.instance.HHPBottleNum += 1;
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Sword"))
        {
            if (collision.gameObject.GetComponent<ItemCreate>().canEat)
            {
                if (GameManager.instance.handsEquipmentName != "Ìú½£")
                {
                    if (GameManager.instance.Sword1Num == 0)
                    {
                        GameManager.instance.Sword1Num += 1;
                    }
                }
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Sword1"))
        {
            if (collision.gameObject.GetComponent<ItemCreate>().canEat)
            {
                if (GameManager.instance.handsEquipmentName != "±¦½£")
                {
                    if (GameManager.instance.Sword2Num == 0)
                    {
                        GameManager.instance.Sword2Num += 1;
                    }
                }
                Destroy(collision.gameObject);
            }
        }
    }

    public void StateUpDate()
    {
        switch (currentState)
        {
            case PlayerState.Dead: DeadUpdate(); break;
            case PlayerState.Attack: AttackUpdate(); break;
            case PlayerState.Skill1: Skill1Update(); break;
            case PlayerState.Block: BlockUpdate(); break;
            case PlayerState.Idle: IdleUpdate(); break;
            case PlayerState.Fall: FallUpdate(); break;
            case PlayerState.Jump: JumpUpdate(); break;
            case PlayerState.Run: RunUpdate(); break;
            case PlayerState.Dash: DashUpdate(); break;
            case PlayerState.GetHit: GetHitUpdate(); break;
        }
    }
    #region playerAttributes
    public void SetAttributes()
    {
        if (isATKUP)
        {
            ATK = ATKBase + 10;
        }
        if (isHPUP)
        {
            playHpMax = HPBase + 100;
            //playHpNow += 100;
            hpSlider.value = (float)playHpNow / playHpMax;
            hpText.text = playHpNow.ToString() + "/" + playHpMax.ToString();
        }
        if (isSpeedUP)
        {
            moveSpeed = SpeedBase * 2;
        }
        if (isSkillUP)
        {
            skillATK = skillATKBase + 10;
        }
}

    public void SetAttributesBool(string boolName)
    {
        if(boolName == "isHPUP")
        {
            isHPUP = true;
            GameManager.instance.isHPUP = true;
        }
        else if(boolName == "isATKUP")
        {
            isATKUP = true;
            GameManager.instance.isATKUP = true;
        }
        else if (boolName == "isSpeedUP")
        {
            isSpeedUP = true;
            GameManager.instance.isSpeedUP = true;
        }
        else if (boolName == "isSkillUP")
        {
            isSkillUP = true;
            GameManager.instance.isSkillUP = true;
        }
        else if (boolName == "isEffectUP")
        {
            isEffectUP = true;
            GameManager.instance.isEffectUP = true;
        }
        else if (boolName == "isCanSkill1")
        {
            isCanSkill1 = true;
            GameManager.instance.isCanSkill1 = true;
        }
        else if (boolName == "isCanSkill2")
        {
            isCanSkill2 = true;
            GameManager.instance.isCanSkill2 = true;
        }
        else if (boolName == "isCanSkill3")
        {
            isCanSkill3 = true;
            GameManager.instance.isCanSkill3 = true;
        }
        else if (boolName == "isCanSkill4")
        {
            isCanSkill4 = true;
            GameManager.instance.isCanSkill4 = true;
        }
        else if (boolName == "isCanSkill5")
        {
            isCanSkill5 = true;
            GameManager.instance.isCanSkill5 = true;
        }
        SetAttributes();
    }

    #endregion
    #region playerStates

    public void IdleEnter()
    {
        ani.SetFloat("IdleAndMove", 0f);
    }

    public void IdleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround && !isDead)
        {
            ChangeState(PlayerState.Jump);
            return;
        }
        if (Input.GetKeyDown(KeyCode.L) && canDash && !isDead && isCanSkill5)
        {
            ChangeState(PlayerState.Dash);
            return;
        }
        if(Input.GetKeyDown(KeyCode.J) && !isAttackOrSkill && !isDead)
        {
            ChangeState(PlayerState.Attack);
            return;
        }
        if (Input.GetKeyDown(KeyCode.K) && !isAttackOrSkill && !isDead && isCanSkill3)
        {
            ChangeState(PlayerState.Block);
            return;
        }
        if (Input.GetKeyDown(KeyCode.U) && !isAttackOrSkill && !isDead && isCanSkill1)
        {
            ChangeState(PlayerState.Skill1);
            return;
        }
        xInput = Input.GetAxisRaw("Horizontal");
        if (xInput != 0)
        {
            ChangeState(PlayerState.Run);
            return;
        }
        if (!isGround)
        {
            ChangeState(PlayerState.Fall);
            return;
        }
        
    }

    public void IdleExit()
    {
    }


    public void RunEnter()
    {
        ani.SetFloat("IdleAndMove", 1f);
    }

    public void RunUpdate()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && isGround && !isDead)
        {
            ChangeState(PlayerState.Jump);
            return;
        }
        if (Input.GetKeyDown(KeyCode.L) && canDash && !isDead && isCanSkill5)
        {
            ChangeState(PlayerState.Dash);
            return;
        }
        if (!isGround && !isDead)
        {
            ChangeState(PlayerState.Fall);
            return;
        }
        if (Input.GetKeyDown(KeyCode.J) && !isAttackOrSkill && !isDead)
        {
            ChangeState(PlayerState.Attack);
            return;
        }
        if (Input.GetKeyDown(KeyCode.K) && !isAttackOrSkill && !isDead && isCanSkill3)
        {
            ChangeState(PlayerState.Block);
            return;
        }
        if (Input.GetKeyDown(KeyCode.U) && !isAttackOrSkill && !isDead && isCanSkill1)
        {
            ChangeState(PlayerState.Skill1);
            return;
        }
        xInput = Input.GetAxisRaw("Horizontal");
        if (xInput != 0)
        {
            //ani.SetBool("isRun", true);
            if (xInput > 0)
            {
                isRight = true;
                spriteRenderer.flipX = false;
            }
            else if (xInput < 0)
            {
                isRight = false;
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            ChangeState(PlayerState.Idle);
        }

        
    }

    public void RunExit()
    {

    }

    public void JumpEnter()
    {
        body.linearVelocityY = 0;
        body.AddForce(new Vector2(body.linearVelocityX, jumpForce));
        ani.SetTrigger("isJump");
        //body.linearVelocityY = 0.3f;
    }

    public void JumpUpdate()
    {
        if (!isGround && !isDead)
        {
            xInput = Input.GetAxisRaw("Horizontal");
            //ani.SetBool("isRun", true);
            if (xInput > 0)
            {
                isRight = true;
                spriteRenderer.flipX = false;
            }
            else if (xInput < 0)
            {
                isRight = false;
                spriteRenderer.flipX = true;
            }

            if (Input.GetKeyDown(KeyCode.L) && canDash && !isDead && isCanSkill5)
            {
                ChangeState(PlayerState.Dash);
            }
        }
        //if (!isGround && body.linearVelocityY < 0)
        //{
        //    ChangeState(PlayerState.Fall);
        //    return;
        //}

        if (isGround && body.linearVelocityY <= 0)
        {
            ChangeState(PlayerState.Idle);
            ani.SetTrigger("isGround");
            return;
        }

    }

    public void JumpExit()
    {

    }

    public void FallEnter()
    {
        ani.SetBool("isAir", true);
    }

    public void FallUpdate()
    {
        if (!isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isDead && canJump2 && isCanSkill4)
            {
                canJump2 = false;
                ChangeState(PlayerState.Jump);
            }
            xInput = Input.GetAxisRaw("Horizontal");
            //ani.SetBool("isRun", true);
            if (xInput > 0)
            {
                isRight = true;
                spriteRenderer.flipX = false;
            }
            else if (xInput < 0)
            {
                isRight = false;
                spriteRenderer.flipX = true;
            }
            if (Input.GetKeyDown(KeyCode.L) && canDash && !isDead && isCanSkill5)
            {
                ChangeState(PlayerState.Dash);
            }
        }

        if (isGround && body.linearVelocityY < 0)
        {
            ChangeState(PlayerState.Idle);
            ani.SetTrigger("isGround");
            return;
        }
    }

    public void FallExit()
    {
        ani.SetBool("isAir", false);
    }

    public void DashEnter()
    {
        canDash = false;
        ani.SetBool("isDash", true);
        Invoke("DashEnd", dashTime);
        Invoke("DashCoolDown", dashCoolDown);
    }

    public void DashUpdate()
    {
        GameObject go = Instantiate(dashPrefab, transform.position, transform.rotation);
        go.GetComponent<SpriteRenderer>().flipX = !isRight;
    }

    public void DashExit()
    {
        CancelInvoke("DashEnd");
        ani.SetBool("isDash", false);
    }

    public void AttackEnter()
    {
        //xInput = 0;
        body.linearVelocityX = 0f;
        isAttackOrSkill = true;
        CancelInvoke("ComboReset");
        Invoke("ComboReset", 1f);
        switch (combo)
        {
            case 1:
                {
                    ani.SetTrigger("Attack1");
                    Invoke("AttackBox1Active", 7 / 30);
                    Invoke("AttackEnd", 0.3f);
                    Invoke("PlayAttackSound", 7 / 30);
                    combo = 2;
                    return;
                }
            case 2:
                {
                    ani.SetTrigger("Attack2");
                    Invoke("AttackBox2Active", 7 / 60);
                    Invoke("AttackEnd", 0.3f);
                    Invoke("PlayAttackSound", 7 / 60);
                    if (isCanSkill2)
                    {
                        combo = 3;
                    }
                    else
                    {
                        combo = 1;
                    }
                    return;
                }
            case 3:
                {
                    ani.SetTrigger("Attack3");
                    Invoke("AttackBox3Active", 1 / 15);
                    Invoke("AttackEnd", 0.3f);
                    Invoke("PlayAttackSound", 1 / 15);
                    combo = 1;
                    return;
                }
        }
    }

    public void AttackUpdate()
    {
        
    }

    public void AttackExit()
    {
        CancelInvoke("AttackBox1Active");
        CancelInvoke("AttackBox2Active");
        CancelInvoke("AttackBox3Active");
        CancelInvoke("PlayAttackSound");
        CancelInvoke("AttackEnd");
        isAttackOrSkill = false;
    }
    
    public void Skill1Enter()
    {
        ani.SetBool("isSkill1", true);
        Invoke("SetCanSkill", 0.3f);
        body.linearVelocityX = 0;
        isAttackOrSkill = true;
    }

    public void Skill1Update()
    {
        if (Input.GetKeyUp(KeyCode.U))
        {
            CancelInvoke("SetCanSkill");
            if (canSkill1)
            {
                GameObject obj = Instantiate(skillSword, transform.position, Quaternion.identity);
                obj.GetComponent<SwordScript>().SetParameter(isRight, gameObject);
                obj.GetComponent<PlayerAttackBox>().SetDamage(skillATK, 1);
                ani.SetTrigger("isSkill12");
                SoundManager.instance.PlayShortSound(2);
            }

            canSkill1 = false;
            ChangeState(PlayerState.Idle);
            return;
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            isRight = true;
            spriteRenderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            isRight = false;
            spriteRenderer.flipX = true;
        }
    }
    public void Skill1Exit()
    {
        isAttackOrSkill = false;
        ani.SetBool("isSkill1", false);
    }

    public void BlockEnter()
    {
        isBlocked = true;
        body.linearVelocityX = 0f;
        isAttackOrSkill = true;
        ani.SetBool("isBlock", true);
    }

    public void BlockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            ChangeState(PlayerState.Idle);
        }
    }
    public void BlockExit()
    {
        ani.SetBool("isBlock", false);
        isAttackOrSkill = false;
        Invoke("SetIsBlocked", 0.34f);
    }

    public void GetHitEnter()
    {
        ani.SetBool("getHit2", true);
        body.linearVelocityX = (isRight ? -1 : 1) * hitAddForce;
        combo = 1;
        //body.AddForce(new Vector2((isRight ? -1 : 1) * hitAddForce, 0));
        Invoke("GetHitStateEnd", getHitTime);
        CinemaShake.instance.ShakeCamera();
    }

    public void GetHitUpdate()
    {

    }

    public void GetHitExit()
    {
        ani.SetBool("getHit2", false);
    }

    public void DeadEnter()
    {
        isDead = true;
        //xInput = 0f;
        body.linearVelocityX = 0f;
        ani.SetTrigger("isDead");
    }

    public void DeadUpdate()
    {

    }

    public void DeadExit()
    {

    }
    #endregion

    public void ChangeState(PlayerState newState)
    {
        String statestring = currentState.ToString();
        statestring = statestring + "Exit";
        Invoke(statestring, 0f);
        lastState = currentState;
        currentState = newState;
        statestring = currentState.ToString();
        statestring = statestring + "Enter";
        Invoke(statestring, 0f);
    }

    public void PlayerMoveFix()
    {
        switch (currentState)
        {
            case PlayerState.Dash:
                body.linearVelocityX = (isRight ? 1 : -1)* dashSpeed;
                body.linearVelocityY = 0f;
                break;
            case PlayerState.Run:
                body.linearVelocityX = xInput * moveSpeed;
                break;
            case PlayerState.Jump:
                body.linearVelocityX = xInput * moveSpeed;
                break;
            case PlayerState.Fall:
                body.linearVelocityX = xInput * moveSpeed;
                break;
            case PlayerState.Idle:
                body.linearVelocityX = 0f;
                break;
            case PlayerState.Block:
                body.linearVelocityX = 0f;
                break;
            case PlayerState.Skill1:
                body.linearVelocityX = 0f;
                break;
        }
    }

    public void CheckisGround()
    {
        Vector2 startPos = transform.position;
        Vector2 endPos = transform.position + Vector3.down;
        RaycastHit2D hit = Physics2D.Linecast(startPos, endPos, groundLayer);
        if (hit.collider != null)
        {
            isGround = true;
            canJump2 = true;
        }
        else
        {
            isGround = false;
        }
    }

    public void DashEnd()
    {
        //ChangeState(lastState);
        ChangeState(PlayerState.Idle);
    }

    public void DashCoolDown()
    {
        canDash = true;
    }

    public void AttackEnd()
    {
        isAttackOrSkill = false;
        ChangeState(PlayerState.Idle);
    }

    public void ComboReset()
    {
        combo = 1;
    }

    public void AttackBox1Active()
    {
        Transform t = Instantiate(attackbox1, transform);
        t.localScale = new Vector3((isRight? 1:-1)*attackbox1.localScale.x, attackbox1.localScale.y, attackbox1.localScale.z);
        t.GetComponent<PlayerAttackBox>().SetDamage(ATK, 1);
    }
    public void AttackBox2Active()
    {
        Transform t = Instantiate(attackbox2, transform);
        t.localScale = new Vector3((isRight ? 1 : -1) * attackbox2.localScale.x, attackbox2.localScale.y, attackbox2.localScale.z);
        t.GetComponent<PlayerAttackBox>().SetDamage(ATK, 1);
    }
    public void AttackBox3Active()
    {
        Transform t = Instantiate(attackbox3, transform);
        t.localScale = new Vector3((isRight ? 1 : -1) * attackbox3.localScale.x, attackbox3.localScale.y, attackbox3.localScale.z);
        t.GetComponent<PlayerAttackBox>().SetDamage(ATK + 5, 2);
    }

    public void GetHit(int damage, int hitLevel)
    {
        if (isDead) 
        { 
            return; 
        }
        if (currentState == PlayerState.Dash)
        {
            return;
        }
        if (currentState == PlayerState.Block)
        {
            SoundManager.instance.PlayShortSound(3);
            combo = 2;
            ChangeState(PlayerState.Attack);
            return;
        }
        if (isBlocked)
        {
            return;
        }
        
        playHpNow -= damage;
        hpSlider.value = (float)playHpNow / playHpMax;
        hpText.text = playHpNow.ToString() + "/" + playHpMax.ToString();
        if (playHpNow <= 0)
        {
            ChangeState(PlayerState.Dead);
        }
        else
        {
            if (hitLevel == 1)
            {
                ani.SetBool("isGetHit", true);
                Invoke("GetHitEnd", 0.3f);
            }
            else if (hitLevel == 2)
            {
                ChangeState(PlayerState.GetHit);
            }
        }
          
    }

    public void GetHitEnd()
    {
        ani.SetBool("isGetHit", false);
    }

    public void GetHitStateEnd()
    {
        ChangeState(PlayerState.Idle);
    }

    public void PlayAttackSound()
    {
        SoundManager.instance.PlayShortSound(0);
    }

    public void SetCanSkill()
    {
        canSkill1 = true;
    }

    public void ADDHP(int hp)
    {
        playHpNow += (hp * (isEffectUP? 2:1));
        if(playHpNow > playHpMax)
        {
            playHpNow = playHpMax;
        }
        hpSlider.value = (float)playHpNow / playHpMax;
        hpText.text = playHpNow.ToString() + "/" + playHpMax.ToString();
    }

    public void PlayerHandsEquipment(string swordName)
    {
        if (handsisEquipment)
        {
            DisChargeHandsEquipment();
        }
        DoPlayerHandsEquipment(swordName);
    }

    public void DisChargeHandsEquipment()
    {
        if (handsisEquipment)
        {
            if (currentHandsName == "Ìú½£")
            {
                ATK = ATK - 10;
                GameManager.instance.Sword1Num = 1;
            }
            else if (currentHandsName == "±¦½£")
            {
                ATK = ATK - 20;
                GameManager.instance.Sword2Num = 1;
            }
            currentHandsName = "";
            handsisEquipment = false;
            GameManager.instance.handsisEquipment = false;
            GameManager.instance.handsEquipmentName = "";
        }
    }

    public void DoPlayerHandsEquipment(string swordName)
    {
        if (swordName == "Ìú½£")
        {
            handsisEquipment = true;
            GameManager.instance.handsisEquipment = true;
            GameManager.instance.handsEquipmentName = swordName;
            currentHandsName = swordName;
            ATK = ATK + 10;
        }
        else if (swordName == "±¦½£")
        {
            handsisEquipment = true;
            GameManager.instance.handsisEquipment = true;
            GameManager.instance.handsEquipmentName = swordName;
            currentHandsName = swordName;
            ATK = ATK + 20;
        }
    }

    public void SetIsBlocked()
    {
        isBlocked = false;
    }
}
