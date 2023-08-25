using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sword �θ� �� ��ü�� ����ġ��
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
    }

    //�浹������ = ��������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ȸ�� �ϸ鼭 ���ƿ��� �ϰ������ ���Ͷ� �ε����� �ȱ���
        if (isReturning) return;

        anim.SetBool("Rotation", false);
        canRotate = false;
        circleCollider.enabled = false;

        //������ٵ� Ű�׸�ƽ true �ؼ� ���� ȿ�� �ȹް�
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        transform.parent = collision.transform;
    }
}
