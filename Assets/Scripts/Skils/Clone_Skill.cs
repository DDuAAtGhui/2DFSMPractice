    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�뽬�� �����ؼ� �нż� ���� ��ų
public class Clone_Skill : Skill
{
    [Header("Clone Info")]
    [SerializeField] GameObject clonePrefab;
    [SerializeField] protected float cloneDuration;
    [Space]
    [SerializeField] protected bool canAttack;
    public void CreateClone(Transform _ClonePosition)
    {
        
        GameObject newClone = Instantiate(clonePrefab);

        newClone.GetComponent<Clone_Skill_Controller>().SetupClone(_ClonePosition, cloneDuration, canAttack);
    }
}
