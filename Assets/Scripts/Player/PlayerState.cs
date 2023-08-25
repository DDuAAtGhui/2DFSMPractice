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
    string AnimationBoolName; //���߿� ����Ƽ ������ �ִϸ����Ͷ� �����ϴ� �κ�
                              //�ִϸ����Ͷ� �ֿܼ��� ������ ���� ���ص� �ż� �ſ� ����

    //���߿� ���� �� Enter�� stateTimer = 0 �Ѵٴ��� �߰��ؼ�
    //�� ���� �ð����� �� �Ѵٴ��� ���� ����
    public float stateTimer;

    //istriggerCalled true���¸� idle�� ���ư��� �Ұ�
    protected bool istriggerCalled;


    //�ٸ� ������ ��ü �����Ҷ� ������ ���� ������ �޴� ������ ����
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _AnimationBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.AnimationBoolName = _AnimationBoolName;
    }
    #endregion
    // ������Ʈ �����ֱ�ó�� ���� ����
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

        //x�� �̵� üũ
        X_Input = Input.GetAxisRaw("Horizontal");
        //y�� �̵� üũ
        Y_Input = Input.GetAxisRaw("Vertical");
        //y�� ���� �ӵ��� �ִϸ����Ϳ� JumpAndFall ���� Ʈ���� ����
        player.anim.SetFloat("yVelocity", rb.velocity.y);


    }
    public virtual void Exit()
    {
    //    Debug.Log("Method - Exit " + AnimationBoolName);

        //���� ������ �Ķ���� �̸��� AnimationBoolName string ���� ��ġ�ϴ� �ִϸ��̼� bool�� false�� ��ȯ
        player.anim.SetBool(AnimationBoolName, false);

    }

    public virtual void AnimationFinishTrigger()
    {
        istriggerCalled = true;
    }
}
