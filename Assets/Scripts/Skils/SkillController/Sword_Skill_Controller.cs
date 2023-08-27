using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sword �θ� �� ��ü�� ����ġ��
//�� �� ��ų�� ���� �����ϴ� �ڵ嵵 �ʿ�


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

        //������ �ƴҶ� ȸ�� �ִϸ��̼� ���ֱ�
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

    //Į ȸ��
    public void ReturnSword()
    {
        //Į ���߿��� ���ƿ����Ҷ��� ȸ���ϴ� �ִϸ��̼� �����鼭 ���ƿ���
        rb.constraints = RigidbodyConstraints2D.FreezeAll;


        //Į ���߿��� ȸ���Ҷ��� ��� ���ƿ���
        // rb.isKinematic = false;

        //�ڽ�ȭ ����
        transform.parent = null;
        isReturning = true;
    }

    private void Update()
    {
        if (canRotate)
            //���ư��� ��¦ ���鼭 ���ư���
            transform.right = rb.velocity;


        if (isReturning)
        {
            //�� ��ġ���� �÷��̾� ������ ReturningSpeed * Time.deltaTime �ӵ��� �̵�
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

    //�浹������ = ��������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ȸ�� �ϸ鼭 ���ƿ��� �ϰ������ ���Ͷ� �ε����� �ȱ���
        if (isReturning) return;

        //if�� ���� null�� üũ
        collision.GetComponent<Enemy>()?.Damage();

        //���� ��ġ �����س��ٰ� �� ��ġ�� ���̸� Į�� ƨ�ܴٴϰ�
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

    //Į�� ��򰡿� �����Ը���
    private void StuckInto(Collider2D collision)
    {
        if (pierceAmount > 0 && collision.GetComponent<Enemy>() != null)
        {
            pierceAmount--;
            return;
        }

        canRotate = false;
        circleCollider.enabled = false;


        //������ٵ� Ű�׸�ƽ true �ؼ� ���� ȿ�� �ȹް�
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        //ƨ�ܴٴϰ� �ҰŸ� ȸ�� �ȸ��߰��ؾ��ϴϱ�
        //���� ��ã������
        if (isBouncing && enemyTraget.Count > 0)
            return;

        anim.SetBool("Rotation", false);
        transform.parent = collision.transform;
    }
}
