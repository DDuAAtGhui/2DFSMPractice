using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    //�ܺ�(Enemy ����������) �÷��̾ ���� �ʿ��Ҷ� PlayerManager.instance.player�� ������ ��������
    //Find�� ã�°ͺ��� �ڿ� �Ҹ� �ſ� ����
    public Player player;

    private void Awake()
    {
        //�̱����� �ϳ��� ����������Ѵ�
        //���� �̱��� �޸� ������Ʈ �������϶� �װ͵� �� �ı��ϰ� �Ѱ��� ����
        if (instance != null) Destroy(instance.gameObject);
        else instance = this;
    }


}
