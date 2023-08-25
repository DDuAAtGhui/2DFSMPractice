using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : Entity
{
    public bool isNowBusy { get; set; }

    [Header("Attack Details")]
    //콤보마다 움직이게 할 변수. 배열로 해서 콤보 int 스택마다 다른 거리로 움직이게 가능
    public Vector2[] attackMovement;
    public float counterAttackDuration = 0.2f;

    [Header("Move Info")]
    public float moveSpeed = 12f;
    public float jumpforce = 7f;
    public float swordReturnImpact;

    [Header("Dash Info")]
    //대쉬
    public float dash_duration = 0.5f;
    public float dash_addSpeed = 7f;

    //대쉬 방향에 사용 
    public float dashDir { get; private set; } = 1;

    //필요한 스킬 호출해서 사용
    public SkillManager skill { get; private set; }
    public GameObject sword;

    //접었다 폈다 할 수 있게 구분선 가능
    #region States 
    //{ get; private set; } = 정보는 읽을 수 있지만 덮어 씌울수는 없게. 읽기전용으로
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
        airState = new PlayerAirState(this, stateMachine, "Jump"); //블렌더 트리에서 사용할거라 같은 Jump로
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "Slide_Wall");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump"); //점프 애니메이션 사용
        playerPrimaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        aimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
    }
    #endregion

    protected override void Start()
    {
        //Player 빈 오브젝트고 자식에 스프라이트 달고 거기에 Animator 달려있는 형태
        base.Start();
        skill = SkillManager.instance;
        stateMachine.Intialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        //GetAxisRaw 키 입력 받아서 공중에서도 플립 가능하게 할 수 있는 버전
        //Player - Update쪽이라 어떤 상태에서든지 플립됨
        //이거 쓸꺼면 Setvelocity안에 플립컨트롤러 주석처리해야함
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


    //코드가 일정 시간동안 바쁜상태 설정하는 메소드
    //이 시간동안엔 상태 변화 불가
    public IEnumerator ImBusyFor(float _seconds)
    {
        isNowBusy = true;
        Debug.Log("바쁨");
        yield return new WaitForSeconds(_seconds);
        Debug.Log("안바쁨");
        isNowBusy = false;
    }

    //PlayerState에서 바로 못가져오니 스테이트 머신 경유해서 가져옴
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();


    //어떤 상태에서든지 대쉬 사용 가능하게 한 버전
    void CheckForDashInput()
    {
        //벽 감지되면 대쉬 수행X
        if (IsWallDetected()) return;


        if ((Input.GetKeyDown(KeyCode.LeftShift))
            && SkillManager.instance.dash.CanUseSkill())
        {

            dashDir = Input.GetAxisRaw("Horizontal");
            #region 게임 방향성따라 대쉬 어떻게 하게 할건지 설정
            //if (dashDir == 0) dashDir = facingDir;

            //if(dashDir != 0)
            #endregion
            stateMachine.ChangeState(dashState);

        }
    }
}
