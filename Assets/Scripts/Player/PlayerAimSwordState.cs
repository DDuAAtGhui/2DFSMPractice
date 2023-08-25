using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _AnimationBoolName)
        : base(_player, _stateMachine, _AnimationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skill.sword_Skill.DotsActive(true);
    }
    public override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Mouse1))
            stateMachine.ChangeState(player.idleState);

        //조준할때 멈추고싶으면 
        player.VectorIsZero();

        //마우스 월드 위치 구하기
        Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //조준해서 검 던질때 마우스 위치로 확인함
        //플레이어가 오른쪽 보고있는데 마우스는 왼쪽에 있을때
        //마우스 우클릭하면 마우스 우클릭한 방향으로 플립
        if (player.transform.position.x > MousePosition.x && player.facingDir == 1)
            player.flip();

        //같은 논리
        else if (player.transform.position.x < MousePosition.x && player.facingDir == -1)
            player.flip();


    }

    public override void Exit()
    {
        base.Exit();
        player.skill.sword_Skill.DotsActive(false);

        //회수 후 0.2초 동안은 못건드리게 
        player.StartCoroutine("ImBusyFor", 0.2f);
    }

}
