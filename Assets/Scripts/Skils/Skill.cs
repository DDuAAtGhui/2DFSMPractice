using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;

    //player 자주 쓸거같으니
    protected Player player;

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }
    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if(cooldownTimer <0)
        {
            //스킬 사용 가능
            cooldownTimer = cooldown;
            return true;
        }
        Debug.Log("스킬 아직 쿨다운 상태");
        return false;
    }

    public virtual void UseSkill()
    {
        //사용할 스킬 만들기


    }
}
