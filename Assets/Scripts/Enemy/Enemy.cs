using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    //�� ��ǥ�� : �÷��̾�
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Stun Info")]
    public float stunDuration = 1.0f;
    public Vector2 stunDir = new Vector2(1, 0);
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;

    [Header("Move Info")]
    public float MoveSpeed;
    //������ �ð���ŭ idle����
    public float idleTime;

    [Header("Battle Info")]
    //battleTime��ŭ �����ð�.
    //�� �ð��� ������ �������� �����ϰ� �����
    public float battleTime;
    public float SightDistance = 10f;

    [Header("Attack Info")]
    public float attackDistance;
    public float attackCoolDown;
    public float lastTimeAttacked;


    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

    }
    //ī���� ���ϸ� ����
    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        //���� ���ϰ� ī���� ǥ�� �̹��� �����ֱ�
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);

    }
    //�ܺο��� CanBeStunned ��밡���ϰ�
    //�ڽ��� �������̵�� ��뵵 ����
    public virtual bool CanBeStunned()
    {
        if(canBeStunned)
        {
            //ī���� ǥ�� �̹��� �ݾ���
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }

    //EnemyState�� �ִ� AnimationFinishTrigger �޼ҵ带 ȣ���ϴ� �޼ҵ�
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, SightDistance, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,
            new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }
}
