using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    

    //기본 공격 3연타 콤보에 사용할 int 변수
    int comboCounter;
    //현재 공격중인지  알려주는 변수
    public bool isAttacking;
    float lastTimeAttacked;
    float comboWindow = 0.5f;
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _AnimationBoolName)
        : base(_player, _stateMachine, _AnimationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        X_Input = 0;

        isAttacking = true;

        //공격 하면 아주살짝 움직이고 멈춤
        stateTimer = 0.2f;

        //3번째 콤보 진입하면 카운터 0으로 초기화
        //현재 시간이 Exit에 있는 lastTimeAttacked(마지막으로 공격하고 끝났을때 시간) + comboWindow(콤보 허용 시간) 커지면 초기화
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow) comboCounter = 0;

        player.anim.SetInteger("comboCounter", comboCounter);

        //공격 사이사이 방향 전환 빠르게 가능하게해줌
        float attackDir = player.facingDir;
        if(X_Input != 0) attackDir = X_Input;


        //공격 속도 증가
        //player.anim.speed = 3;

        //땅에 붙어서 공격마다 이동
        //player.SetVelocity(player.attackMovement[comboCounter].x * player.facingDir, rb.velocity.y);

        //공격시 이동할 X, Y축 벡터 float값
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir,
            player.attackMovement[comboCounter].y);

    }
    public override void Update()
    {
        base.Update();

        //공격 하면 아주살짝 움직이고 멈춤
        if (stateTimer < 0)
            rb.velocity = new Vector2(0, 0);

        if (istriggerCalled) stateMachine.ChangeState(player.idleState);
    }

    //기본 공격 끝나면 콤보 카운터++
    public override void Exit()
    {
        base.Exit();

        comboCounter++;
        //공격속도 원상복귀
        //player.anim.speed = 1;

        player.StartCoroutine("ImBusyFor", 0.1f);

        //현재 시간
        lastTimeAttacked = Time.time;
        isAttacking = false;


    }

}
