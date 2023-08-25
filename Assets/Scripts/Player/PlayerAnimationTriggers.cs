using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnmationTriggers : MonoBehaviour
{
    Player player => GetComponentInParent<Player>();

    //Player에 붙어있는 AnimationTrigger 애니메이션 이벤트에 함수추가 할거임
    void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    void AttackTrigger()
    {
        //여러명 동시타격
        //player.attackCheck.position에서 player.attackCheckRadius만큼의 반지름을 가진 원 안에 있는 콜라이더들을 배열로 가져옴
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position,player.attackCheckRadius);
        
        //배열에 들어간 콜라이더들 공격
        foreach(var hit in colliders)
        {
            //배열에 들어간 콜라이더들 중에 Enemy스크립트 달린애들 있으면
            if (hit.GetComponent<Enemy>() != null)
                //Enemy 스크립트 달린 객체들 그 스크립트의 Damage 메소드 발동
                hit.GetComponent<Enemy>().Damage();
        }
    }

    void ThrowSword()
    {
        SkillManager.instance.sword_Skill.CreatSword();
    }
}
