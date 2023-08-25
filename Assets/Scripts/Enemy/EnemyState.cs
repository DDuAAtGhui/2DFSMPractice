using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;

    //triggerCalled�� ���� ������ ���ٰ���
    //���̷����� triggerCalled�� Skeleton_AnimationTriggers ���� ���� ����
    protected bool triggerCalled;
    protected float stateTimer;
    private string animBoolName;


    public EnemyState(Enemy _enemyBase, EnemyStateMachine _enemystatemachine, string _animBoolName)
    {
        enemyBase = _enemyBase;
        stateMachine = _enemystatemachine;
        animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        //��� ���µ� �����ϸ� triggerCalled false����
        triggerCalled = false;

        enemyBase.anim.SetBool(animBoolName, true);
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);

    }
    //triggerCalled�� true�� ������ִ� �޼ҵ�. �̰� �ۿ��� ȣ���ؼ� ����Ұ���
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
