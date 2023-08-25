using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;

    //triggerCalled는 따로 관리를 해줄거임
    //스켈레톤은 triggerCalled를 Skeleton_AnimationTriggers 에서 관리 해줌
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
        //모든 상태들 진입하면 triggerCalled false상태
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
    //triggerCalled를 true로 만들어주는 메소드. 이걸 밖에서 호출해서 사용할거임
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
