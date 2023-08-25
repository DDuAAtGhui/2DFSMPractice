using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    //������ �㶧 ��Ʈ�� + . �ϸ� �� �������
    //������ �����ϰ� �θ�Ŭ������ ������ ����
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _AnimationBoolName)
                            : base(_player, _stateMachine, _AnimationBoolName)
    {

    }
    //PlayerIdelState �巡���ϰ� ��Ʈ ���� - Create Override
    public override void Enter()
    {
        base.Enter();
        rb.velocity = Vector2.zero;
    }
    public override void Update()
    {
        base.Update();
        //�� ��� �����Ӱ��� �ԷµǸ� Move ���·� ��ȯ
        
        //�ٶ󺸴� �������� Ű �Է� �ϴµ� ���� �����ǰ� ������ �ٷ� return�� moveState���� ���� ����
        if (X_Input == player.facingDir && player.IsWallDetected()) return;

        //�⺻ ���ݿ��� Exit�ϸ� 0.1�� �ٻۻ��·� ������
        if (X_Input != 0 && !player.isNowBusy)
            player.stateMachine.ChangeState(player.moveState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
