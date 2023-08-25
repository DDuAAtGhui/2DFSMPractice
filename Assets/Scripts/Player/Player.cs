using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : Entity
{
    public bool isNowBusy { get; set; }

    [Header("Attack Details")]
    //�޺����� �����̰� �� ����. �迭�� �ؼ� �޺� int ���ø��� �ٸ� �Ÿ��� �����̰� ����
    public Vector2[] attackMovement;
    public float counterAttackDuration = 0.2f;

    [Header("Move Info")]
    public float moveSpeed = 12f;
    public float jumpforce = 7f;
    public float swordReturnImpact;

    [Header("Dash Info")]
    //�뽬
    public float dash_duration = 0.5f;
    public float dash_addSpeed = 7f;

    //�뽬 ���⿡ ��� 
    public float dashDir { get; private set; } = 1;

    //�ʿ��� ��ų ȣ���ؼ� ���
    public SkillManager skill { get; private set; }
    public GameObject sword;

    //������ ��� �� �� �ְ� ���м� ����
    #region States 
    //{ get; private set; } = ������ ���� �� ������ ���� ������� ����. �б���������
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState playerPrimaryAttack { get; private set; }
    public PlayerCounterAttackState counterState { get; private set; }
    public PlayerAimSwordState aimSwordState { get; private set; }
    public PlayerCatchSwordState catchSwordState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump"); //���� Ʈ������ ����ҰŶ� ���� Jump��
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "Slide_Wall");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump"); //���� �ִϸ��̼� ���
        playerPrimaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        aimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
    }
    #endregion

    protected override void Start()
    {
        //Player �� ������Ʈ�� �ڽĿ� ��������Ʈ �ް� �ű⿡ Animator �޷��ִ� ����
        base.Start();
        skill = SkillManager.instance;
        stateMachine.Intialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        //GetAxisRaw Ű �Է� �޾Ƽ� ���߿����� �ø� �����ϰ� �� �� �ִ� ����
        //Player - Update���̶� � ���¿������� �ø���
        //�̰� ������ Setvelocity�ȿ� �ø���Ʈ�ѷ� �ּ�ó���ؾ���
        FlipController2();
        CheckForDashInput();
    }

    public void AssignNewSword(GameObject _newSword)
    {
        sword = _newSword;
    }

    public void CatchSword()
    {
        stateMachine.ChangeState(catchSwordState);
        Destroy(sword);
    }


    //�ڵ尡 ���� �ð����� �ٻۻ��� �����ϴ� �޼ҵ�
    //�� �ð����ȿ� ���� ��ȭ �Ұ�
    public IEnumerator ImBusyFor(float _seconds)
    {
        isNowBusy = true;
        Debug.Log("�ٻ�");
        yield return new WaitForSeconds(_seconds);
        Debug.Log("�ȹٻ�");
        isNowBusy = false;
    }

    //PlayerState���� �ٷ� ���������� ������Ʈ �ӽ� �����ؼ� ������
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();


    //� ���¿������� �뽬 ��� �����ϰ� �� ����
    void CheckForDashInput()
    {
        //�� �����Ǹ� �뽬 ����X
        if (IsWallDetected()) return;


        if ((Input.GetKeyDown(KeyCode.LeftShift))
            && SkillManager.instance.dash.CanUseSkill())
        {

            dashDir = Input.GetAxisRaw("Horizontal");
            #region ���� ���⼺���� �뽬 ��� �ϰ� �Ұ��� ����
            //if (dashDir == 0) dashDir = facingDir;

            //if(dashDir != 0)
            #endregion
            stateMachine.ChangeState(dashState);

        }
    }
}
