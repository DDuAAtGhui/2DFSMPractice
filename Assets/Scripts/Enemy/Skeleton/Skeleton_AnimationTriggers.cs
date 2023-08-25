using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_AnimationTriggers : MonoBehaviour
{
    Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>();

    //enemy에서 Enemystate의 AnimationFinishTrigger를 호출하는 메소드를 호출하는 AnimationFinishTrigger 메소드를 호출 하는 메소드
    //이걸 애니메이터에 붙여주고 키 이벤트로 사용할거임. 
    //공격 모션 끝날때 호출해서 triggerCalled를 true로 만들어줄것
    public void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach(var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
                hit.GetComponent<Player>().Damage();
            
        }
    }
    //키 이벤트에서 enemy.OpenCounterAttackWindow() 호출
    protected void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    protected void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
