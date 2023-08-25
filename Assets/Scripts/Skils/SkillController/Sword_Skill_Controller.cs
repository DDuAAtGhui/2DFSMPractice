using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sword 부모 빈 객체에 어태치됨
public class Sword_Skill_Controller : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    Player player;

    private bool canRotate = true;
    bool isReturning;
    [SerializeField] float ReturningSpeed = 12f;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

    }

    public void SetupSword(Vector2 _dir, float _gravityScale, Player player)
    {
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
        this.player = player;

        anim.SetBool("Rotation", true);
    }

    //칼 회수
    public void ReturnSword()
    {
        //칼 공중에서 돌아오게할때는 회전하는 애니메이션 나오면서 돌아오게
        rb.constraints = RigidbodyConstraints2D.FreezeAll;


        //칼 공중에서 회수할때도 굳어서 돌아오게
        // rb.isKinematic = false;

        //자식화 해제
        transform.parent = null;
        isReturning = true;
    }

    private void Update()
    {
        if (canRotate)
            //날아갈때 살짝 돌면서 날아가게
            transform.right = rb.velocity;


        if (isReturning)
        {
            //검 위치에서 플레이어 쪽으로 ReturningSpeed * Time.deltaTime 속도로 이동
            transform.position = Vector2.MoveTowards(transform.position,
                player.transform.position,
                ReturningSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < 1)
                player.CatchSword();


        }
    }

    //충돌했을때 = 박혔을때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //회전 하면서 돌아오게 하고싶을때 몬스터랑 부딪혀도 안굳게
        if (isReturning) return;

        anim.SetBool("Rotation", false);
        canRotate = false;
        circleCollider.enabled = false;

        //리지드바디 키네마틱 true 해서 물리 효과 안받게
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        transform.parent = collision.transform;
    }
}
