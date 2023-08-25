using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Clone_Skill_Controller : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator anim;
    [SerializeField] private float colorLoosingSpeed;
    float cloneTimer;
    //�뽬 �ϴ� �������� ��ġ �� �� �ֵ���


    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = 0.8f;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if (cloneTimer < 0)
        {
            spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a - (Time.deltaTime * colorLoosingSpeed));

            if (spriteRenderer.color.a < 0) Destroy(gameObject);
        }
    }

    public void SetupClone(Transform _newTransform, float _cloneDuration, bool _canAttack)
    {
        if (_canAttack) anim.SetInteger("AttackNumber", Random.Range(1, 4));
        transform.position = _newTransform.position;
        cloneTimer = _cloneDuration;

        FaceClosesToTarget();
    }
    //���ù� ���� ���� �ذ��ϱ����� �÷��̾��ʿ��� ������
    void AnimationTrigger()
    {
        cloneTimer = -0.1f;
    }

    void AttackTrigger()
    {
        //������ ����Ÿ��
        //player.attackCheck.position���� player.attackCheckRadius��ŭ�� �������� ���� �� �ȿ� �ִ� �ݶ��̴����� �迭�� ������
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        //�迭�� �� �ݶ��̴��� ����
        foreach (var hit in colliders)
        {
            //�迭�� �� �ݶ��̴��� �߿� Enemy��ũ��Ʈ �޸��ֵ� ������
            if (hit.GetComponent<Enemy>() != null)
                //Enemy ��ũ��Ʈ �޸� ��ü�� �� ��ũ��Ʈ�� Damage �޼ҵ� �ߵ�
                hit.GetComponent<Enemy>().Damage();
        }
    }

    //����� �� ���� ����
    private Transform closestEnemy;
    void FaceClosesToTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

        float closestDistance = Mathf.Infinity;

        foreach(var hit in colliders)
        {
            if(hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                if(distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }

        if(closestEnemy != null)
        {
            if(transform.position.x > closestEnemy.position.x)
            {
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
