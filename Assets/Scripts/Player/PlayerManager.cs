using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    //외부(Enemy 같은곳에서) 플레이어에 접근 필요할때 PlayerManager.instance.player로 접근이 가능해짐
    //Find로 찾는것보다 자원 소모 매우 적음
    public Player player;

    private void Awake()
    {
        //싱글톤은 하나만 만들어져야한다
        //같은 싱글톤 달린 오브젝트 여러개일때 그것들 다 파괴하고 한개만 남김
        if (instance != null) Destroy(instance.gameObject);
        else instance = this;
    }


}
