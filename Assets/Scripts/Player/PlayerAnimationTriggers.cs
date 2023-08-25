using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnmationTriggers : MonoBehaviour
{
    Player player => GetComponentInParent<Player>();

    //Player�� �پ��ִ� AnimationTrigger �ִϸ��̼� �̺�Ʈ�� �Լ��߰� �Ұ���
    void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    void AttackTrigger()
    {
        //������ ����Ÿ��
        //player.attackCheck.position���� player.attackCheckRadius��ŭ�� �������� ���� �� �ȿ� �ִ� �ݶ��̴����� �迭�� ������
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position,player.attackCheckRadius);
        
        //�迭�� �� �ݶ��̴��� ����
        foreach(var hit in colliders)
        {
            //�迭�� �� �ݶ��̴��� �߿� Enemy��ũ��Ʈ �޸��ֵ� ������
            if (hit.GetComponent<Enemy>() != null)
                //Enemy ��ũ��Ʈ �޸� ��ü�� �� ��ũ��Ʈ�� Damage �޼ҵ� �ߵ�
                hit.GetComponent<Enemy>().Damage();
        }
    }

    void ThrowSword()
    {
        SkillManager.instance.sword_Skill.CreatSword();
    }
}
