using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    

    //�⺻ ���� 3��Ÿ �޺��� ����� int ����
    int comboCounter;
    //���� ����������  �˷��ִ� ����
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

        //���� �ϸ� ���ֻ�¦ �����̰� ����
        stateTimer = 0.2f;

        //3��° �޺� �����ϸ� ī���� 0���� �ʱ�ȭ
        //���� �ð��� Exit�� �ִ� lastTimeAttacked(���������� �����ϰ� �������� �ð�) + comboWindow(�޺� ��� �ð�) Ŀ���� �ʱ�ȭ
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow) comboCounter = 0;

        player.anim.SetInteger("comboCounter", comboCounter);

        //���� ���̻��� ���� ��ȯ ������ �����ϰ�����
        float attackDir = player.facingDir;
        if(X_Input != 0) attackDir = X_Input;


        //���� �ӵ� ����
        //player.anim.speed = 3;

        //���� �پ ���ݸ��� �̵�
        //player.SetVelocity(player.attackMovement[comboCounter].x * player.facingDir, rb.velocity.y);

        //���ݽ� �̵��� X, Y�� ���� float��
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir,
            player.attackMovement[comboCounter].y);

    }
    public override void Update()
    {
        base.Update();

        //���� �ϸ� ���ֻ�¦ �����̰� ����
        if (stateTimer < 0)
            rb.velocity = new Vector2(0, 0);

        if (istriggerCalled) stateMachine.ChangeState(player.idleState);
    }

    //�⺻ ���� ������ �޺� ī����++
    public override void Exit()
    {
        base.Exit();

        comboCounter++;
        //���ݼӵ� ���󺹱�
        //player.anim.speed = 1;

        player.StartCoroutine("ImBusyFor", 0.1f);

        //���� �ð�
        lastTimeAttacked = Time.time;
        isAttacking = false;


    }

}
