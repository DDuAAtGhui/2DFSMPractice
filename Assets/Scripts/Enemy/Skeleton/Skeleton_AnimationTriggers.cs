using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_AnimationTriggers : MonoBehaviour
{
    Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>();

    //enemy���� Enemystate�� AnimationFinishTrigger�� ȣ���ϴ� �޼ҵ带 ȣ���ϴ� AnimationFinishTrigger �޼ҵ带 ȣ�� �ϴ� �޼ҵ�
    //�̰� �ִϸ����Ϳ� �ٿ��ְ� Ű �̺�Ʈ�� ����Ұ���. 
    //���� ��� ������ ȣ���ؼ� triggerCalled�� true�� ������ٰ�
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
    //Ű �̺�Ʈ���� enemy.OpenCounterAttackWindow() ȣ��
    protected void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    protected void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
