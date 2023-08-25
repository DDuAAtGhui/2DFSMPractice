using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _AnimationBoolName)
        : base(_player, _stateMachine, _AnimationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.counterAttackDuration;
        //SuccessFullCounter 애니메이션 bool값 초기화
        player.anim.SetBool("SuccessFullCounter", false);
    }
    public override void Update()
    {
        base.Update();
        //카운터 자세 잡을시 이동 불가
        player.VectorIsZero();
        //여러명 동시타격
        //player.attackCheck.position에서 player.attackCheckRadius만큼의 반지름을 가진 원 안에 있는 콜라이더들을 배열로 가져옴
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        //배열에 들어간 콜라이더들 공격
        foreach (var hit in colliders)
        {
            //배열에 들어간 콜라이더들 중에 Enemy스크립트 달린애들 있으면
            if (hit.GetComponent<Enemy>() != null)
            {
                //stateTimer 시간 안에 적 스크립트 달린애들 있으면
                //스턴으로 만들어 버린 다음에 공격 애니메이션 발생
                if (hit.GetComponent<Enemy>().CanBeStunned())
                {
                    //테스트용
                    stateTimer = 10;

                    player.anim.SetBool("SuccessFullCounter", true);
                }
            }
        }

        //반격 실패 혹은 공격 모션 끝나면 종료
        if (stateTimer < 0 || istriggerCalled) stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
