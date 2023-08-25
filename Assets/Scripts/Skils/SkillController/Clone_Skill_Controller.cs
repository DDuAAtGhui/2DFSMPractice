using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Clone_Skill_Controller : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator anim;
    [SerializeField] private float colorLoosingSpeed;
    float cloneTimer;
    //대쉬 하는 지점에서 설치 될 수 있도록


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
    //리시버 없는 버그 해결하기위해 플레이어쪽에서 가져옴
    void AnimationTrigger()
    {
        cloneTimer = -0.1f;
    }

    void AttackTrigger()
    {
        //여러명 동시타격
        //player.attackCheck.position에서 player.attackCheckRadius만큼의 반지름을 가진 원 안에 있는 콜라이더들을 배열로 가져옴
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        //배열에 들어간 콜라이더들 공격
        foreach (var hit in colliders)
        {
            //배열에 들어간 콜라이더들 중에 Enemy스크립트 달린애들 있으면
            if (hit.GetComponent<Enemy>() != null)
                //Enemy 스크립트 달린 객체들 그 스크립트의 Damage 메소드 발동
                hit.GetComponent<Enemy>().Damage();
        }
    }

    //가까운 적 방향 추적
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
