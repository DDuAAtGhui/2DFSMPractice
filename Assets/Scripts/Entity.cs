using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어도 쓰고 적도 쓸거니까 모두 protected로 선언해줌
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

    //공격 관련
    //
    public Transform attackCheck;
    //attackCheck 원 간격
    public float attackCheckRadius;


    //외부에서 방향 변수 사용하기 때문에 public 사용
    //오른쪽 보고있는 상태가 1
    public int facingDir { get; set; } = 1;
    //만들때 스프라이트 바라보는 방향 기준
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

    //데미지 받는 메소드
    public virtual void Damage()
    {
        Debug.Log(gameObject.name + "가 데미지를 입었다");
        fx.StartCoroutine("FlashFX");

        //쓸데없는 실행 방지
        if(knockbackduration != 0)
          StartCoroutine("HitKnockBack");
    }

    protected virtual IEnumerator HitKnockBack()
    {
        isKnockback = true;

        //바라보는 방향의 반대방향으로 밀려나야함
        rb.velocity = new Vector2(knockbackDir.x * -facingDir * knockbackpower_X, knockbackDir.y * knockbackpower_Y);
        yield return new WaitForSeconds(knockbackduration);
        isKnockback = false;
    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        //넉백 상태면 실행X
        if (isKnockback) return;

        rb.velocity = new Vector2(_xVelocity, _yVelocity);

        //FlipController(rb.velocity.x);

    }



    #region 자주 쓰는거 메소드로 정리
    public void VectorIsZero()
    {
        //넉백상태일때는 벡터 초기화 불가능하게
        if (isKnockback) return;

        rb.velocity = new Vector2(0, 0);
    }
    #endregion

    #region 땅이랑 벽체크
    //한줄짜리 메소드 람다식으로 축약
    public virtual bool isGroundedDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatisground);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position,
        Vector2.right * facingDir, wallCheckDistance, whatisground);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x,
            groundCheck.position.y - groundCheckDistance));

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance,
            wallCheck.position.y));

        //공격 판정 범위 그려주기
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion

    #region Flip
    public virtual void flip()
    {
        //회전할 때 쓸 변수들 토글
        facingDir = -facingDir;
        isfacingRight = !isfacingRight;
        //나중에 velocity값과 facing값으로 if걸어서 무한플립 방지할때 사용
        transform.Rotate(0, 180, 0);
    }
    //현재 속도 값으로 체크해서 공중에서 플립 안되는 버전
    public virtual void FlipController(float velocity_x)
    {
        if (velocity_x > 0 && !isfacingRight) flip();
        else if (velocity_x < 0 && isfacingRight) flip();
    }
    //GetAxisRaw 키 입력 받아서 공중에서도 플립 가능하게 할 수 있는 버전
    public virtual void FlipController2()
    {
        //공격중일땐 회전 불가능
        //if (!playerPrimaryAttack.isAttacking)
        //{
        if (Input.GetAxisRaw("Horizontal") > 0 && !isfacingRight) flip();
        else if (Input.GetAxisRaw("Horizontal") < 0 && isfacingRight) flip();
        //}
    }
    #endregion

}
