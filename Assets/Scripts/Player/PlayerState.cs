using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected float X_Input;
    protected float Y_Input;
    #region State
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;
    string AnimationBoolName; //나중에 유니티 내에서 애니메이터랑 연결하는 부분
                              //애니메이터랑 콘솔에서 일일이 지정 안해도 돼서 매우 편함

    //나중에 상태 쪽 Enter에 stateTimer = 0 한다던지 추가해서
    //그 상태 시간따라 뭘 한다던지 설정 가능
    public float stateTimer;

    //istriggerCalled true상태면 idle로 돌아가게 할것
    protected bool istriggerCalled;


    //다른 곳에서 객체 선언할때 변수에 인자 데이터 받는 생성자 생성
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _AnimationBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.AnimationBoolName = _AnimationBoolName;
    }
    #endregion
    // 오브젝트 생명주기처럼 상태 생명
    public virtual void Enter()
    {
      //  Debug.Log("Method - Enter " + AnimationBoolName);

        rb = player.rb;
        player.anim.SetBool(AnimationBoolName, true);

        istriggerCalled = false;
    }
    public virtual void Update()
    {
     //   Debug.Log("Method - Update " + AnimationBoolName);

        stateTimer -= Time.deltaTime;

        //x축 이동 체크
        X_Input = Input.GetAxisRaw("Horizontal");
        //y축 이동 체크
        Y_Input = Input.GetAxisRaw("Vertical");
        //y축 현재 속도값 애니메이터에 JumpAndFall 블렌드 트리에 연결
        player.anim.SetFloat("yVelocity", rb.velocity.y);


    }
    public virtual void Exit()
    {
    //    Debug.Log("Method - Exit " + AnimationBoolName);

        //상태 끝나면 파라미터 이름과 AnimationBoolName string 변수 일치하는 애니메이션 bool값 false로 전환
        player.anim.SetBool(AnimationBoolName, false);

    }

    public virtual void AnimationFinishTrigger()
    {
        istriggerCalled = true;
    }
}
