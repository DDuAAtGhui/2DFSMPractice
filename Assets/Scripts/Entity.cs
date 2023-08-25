using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾ ���� ���� ���Ŵϱ� ��� protected�� ��������
public class Entity : MonoBehaviour
{
    #region Components 
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    #endregion

    [Header("Knock Back")]
    [SerializeField] protected Vector2 knockbackDir = new Vector2(1,0);
    [SerializeField] protected float knockbackpower_X = 5f;
    [SerializeField] protected float knockbackpower_Y = 5f;
    [SerializeField] protected float knockbackduration = 0.07f;
    protected bool isKnockback;

    [Header("Collision Info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatisground;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatiswall;

    //���� ����
    //
    public Transform attackCheck;
    //attackCheck �� ����
    public float attackCheckRadius;


    //�ܺο��� ���� ���� ����ϱ� ������ public ���
    //������ �����ִ� ���°� 1
    public int facingDir { get; set; } = 1;
    //���鶧 ��������Ʈ �ٶ󺸴� ���� ����
    public bool isfacingRight = true;


    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
      //  fx = GetComponentInChildren<EntityFX>();
        fx = GetComponent<EntityFX>();

    }
    protected virtual void Update()
    {

    }

    //������ �޴� �޼ҵ�
    public virtual void Damage()
    {
        Debug.Log(gameObject.name + "�� �������� �Ծ���");
        fx.StartCoroutine("FlashFX");

        //�������� ���� ����
        if(knockbackduration != 0)
          StartCoroutine("HitKnockBack");
    }

    protected virtual IEnumerator HitKnockBack()
    {
        isKnockback = true;

        //�ٶ󺸴� ������ �ݴ�������� �з�������
        rb.velocity = new Vector2(knockbackDir.x * -facingDir * knockbackpower_X, knockbackDir.y * knockbackpower_Y);
        yield return new WaitForSeconds(knockbackduration);
        isKnockback = false;
    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        //�˹� ���¸� ����X
        if (isKnockback) return;

        rb.velocity = new Vector2(_xVelocity, _yVelocity);

        //FlipController(rb.velocity.x);

    }



    #region ���� ���°� �޼ҵ�� ����
    public void VectorIsZero()
    {
        //�˹�����϶��� ���� �ʱ�ȭ �Ұ����ϰ�
        if (isKnockback) return;

        rb.velocity = new Vector2(0, 0);
    }
    #endregion

    #region ���̶� ��üũ
    //����¥�� �޼ҵ� ���ٽ����� ���
    public virtual bool isGroundedDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatisground);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position,
        Vector2.right * facingDir, wallCheckDistance, whatisground);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x,
            groundCheck.position.y - groundCheckDistance));

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance,
            wallCheck.position.y));

        //���� ���� ���� �׷��ֱ�
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion

    #region Flip
    public virtual void flip()
    {
        //ȸ���� �� �� ������ ���
        facingDir = -facingDir;
        isfacingRight = !isfacingRight;
        //���߿� velocity���� facing������ if�ɾ �����ø� �����Ҷ� ���
        transform.Rotate(0, 180, 0);
    }
    //���� �ӵ� ������ üũ�ؼ� ���߿��� �ø� �ȵǴ� ����
    public virtual void FlipController(float velocity_x)
    {
        if (velocity_x > 0 && !isfacingRight) flip();
        else if (velocity_x < 0 && isfacingRight) flip();
    }
    //GetAxisRaw Ű �Է� �޾Ƽ� ���߿����� �ø� �����ϰ� �� �� �ִ� ����
    public virtual void FlipController2()
    {
        //�������϶� ȸ�� �Ұ���
        //if (!playerPrimaryAttack.isAttacking)
        //{
        if (Input.GetAxisRaw("Horizontal") > 0 && !isfacingRight) flip();
        else if (Input.GetAxisRaw("Horizontal") < 0 && isfacingRight) flip();
        //}
    }
    #endregion

}
