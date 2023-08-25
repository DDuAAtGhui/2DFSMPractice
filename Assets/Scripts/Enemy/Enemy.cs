using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    //적 목표물 : 플레이어
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Stun Info")]
    public float stunDuration = 1.0f;
    public Vector2 stunDir = new Vector2(1, 0);
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;

    [Header("Move Info")]
    public float MoveSpeed;
    //정해진 시간만큼 idle상태
    public float idleTime;

    [Header("Battle Info")]
    //battleTime만큼 전투시간.
    //이 시간이 지나면 전투상태 해제하게 만들기
    public float battleTime;
    public float SightDistance = 10f;

    [Header("Attack Info")]
    public float attackDistance;
    public float attackCoolDown;
    public float lastTimeAttacked;


    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

    }
    //카운터 당하면 스턴
    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        //스턴 당하고 카운터 표시 이미지 보여주기
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);

    }
    //외부에서 CanBeStunned 사용가능하게
    //자식이 오버라이드로 사용도 가능
    public virtual bool CanBeStunned()
    {
        if(canBeStunned)
        {
            //카운터 표시 이미지 닫아줌
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }

    //EnemyState에 있는 AnimationFinishTrigger 메소드를 호출하는 메소드
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, SightDistance, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,
            new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }
}
