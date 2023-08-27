using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sword 부모 빈 객체에 어태치됨
//각 검 스킬이 뭔지 구분하는 코드도 필요


public class Sword_Skill_Controller : MonoBehaviour
{

    Animator anim;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    Player player;

    private bool canRotate = true;
    bool isReturning;
    [SerializeField] float ReturningSpeed = 12f;

    [Header("Pierce Info")]
    [SerializeField] float pierceAmount;


    [Header("Bounce Info")]
    [SerializeField] float BounceSpeed = 10f;
    bool isBouncing;
    int bounceAmount = 4;
    List<Transform> enemyTraget;
    int targetIndex;


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

        //관통형 아닐때 회전 애니메이션 안주기
        if (pierceAmount <= 0)
            anim.SetBool("Rotation", true);
    }

    public void SetupBounce(bool _isBouncing, int _amountOfBouncing)
    {
        isBouncing = _isBouncing;
        bounceAmount = _amountOfBouncing;

        enemyTraget = new List<Transform>();
    }

    public void SetupPierce(int _pierceAMount)
    {
        pierceAmount = _pierceAMount;

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

        BounceLogic();
    }

    private void BounceLogic()
    {
        if (isBouncing && enemyTraget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                enemyTraget[targetIndex].position, BounceSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, enemyTraget[targetIndex].position) < 0.5f)
            {
                targetIndex++;
                bounceAmount--;

                if (bounceAmount < 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }

                if (targetIndex >= enemyTraget.Count)
                    targetIndex = 0;
            }
        }
    }

    //충돌했을때 = 박혔을때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //회전 하면서 돌아오게 하고싶을때 몬스터랑 부딪혀도 안굳게
        if (isReturning) return;

        //if문 없이 null값 체크
        collision.GetComponent<Enemy>()?.Damage();

        //몬스터 위치 저장해놨다가 그 위치들 사이를 칼이 튕겨다니게
        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBouncing && enemyTraget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                        enemyTraget.Add(hit.transform);
                }
            }
        }

        StuckInto(collision);
    }

    //칼이 어딘가에 박히게만듦
    private void StuckInto(Collider2D collision)
    {
        if (pierceAmount > 0 && collision.GetComponent<Enemy>() != null)
        {
            pierceAmount--;
            return;
        }

        canRotate = false;
        circleCollider.enabled = false;


        //리지드바디 키네마틱 true 해서 물리 효과 안받게
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        //튕겨다니게 할거면 회전 안멈추게해야하니까
        //몬스터 못찾을때도
        if (isBouncing && enemyTraget.Count > 0)
            return;

        anim.SetBool("Rotation", false);
        transform.parent = collision.transform;
    }
}
