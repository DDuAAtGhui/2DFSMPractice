using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    #region 상태
    public Skeleton_IdleState idleState { get; private set; }
    public Skeleton_MoveState moveState { get; private set; }
    public Skeleton_BattleState battleState { get; private set; }
    public Skeleton_AttackState attackState { get; private set; }
    public Skeleton_StunState stunState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();

        idleState = new Skeleton_IdleState(this, stateMachine, "Idle", this);
        moveState = new Skeleton_MoveState(this, stateMachine, "Move", this);
        battleState = new Skeleton_BattleState(this, stateMachine, "Move", this);
        attackState = new Skeleton_AttackState(this, stateMachine, "Attack", this);
        stunState = new Skeleton_StunState(this, stateMachine, "Stun", this);
    }

    protected override void Start()
    {
        base.Start();
        //클라이언트가 idle상태로 시작
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

    }

    //부모 오버라이드해서 사용
    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunState);
            return true;
        }
        return false;
    }
}
