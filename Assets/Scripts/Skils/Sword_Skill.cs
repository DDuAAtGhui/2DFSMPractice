using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill : Skill
{
    [Header("Skill Info")]
    [SerializeField] GameObject swordPrefab;
    [SerializeField] Vector2 launchForce;
    [SerializeField] float swordGravity;

    //���������� Į ���ư� ����
    Vector2 finalDir;

    [Header("Aim dots")]
    //�������� ������ ǥ���Ұ���
    [SerializeField] int numberOfDots;
    //��Ʈ �� ���� ����
    [SerializeField] float spaceBetweenDots;
    [SerializeField] GameObject dotPrefab;
    [SerializeField] Transform dotsParent;

    GameObject[] Dots;
    protected override void Start()
    {
        base.Start();

        GenerateDots();
    }
    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            //���Ⱚ���� ��ȯ�ϸ鼭 ������.
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x,
                AimDirection().normalized.y * launchForce.y);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < Dots.Length; i++)
            {
                Dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
            }
        }
    }
    public void CreatSword()
    {
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        Sword_Skill_Controller newSwordScript = newSword.GetComponent<Sword_Skill_Controller>();

        newSwordScript.SetupSword(finalDir, swordGravity, player);

        //�÷��̾�� Į �Ѱ��� ������ ȸ�������� �������� ����
        player.AssignNewSword(newSword);

        //Į ���� �� ������ ��Ʈ ����
        DotsActive(false);
    }

    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;

        //���콺 ��ġ�� ������. UI���� ���� ��ǥ�� ��ȯ���ִ� �ڵ�
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //�÷��̾�� ���� ���콺 ��ġ�� �ٶ󺸴� ���Ͱ�
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    //��Ʈ�� Ȱ��ȭ
    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < Dots.Length; i++)
        {
            Dots[i].SetActive(_isActive);
        }
    }
    void GenerateDots()
    {
        Dots = new GameObject[numberOfDots];

        for (int i = 0; i < numberOfDots; i++)
        {
            //�θ� �ڽ����� �����鼭 �����ΰ�
            Dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);

            //������ �� �� ���;��ϴϱ� �׶� SetActive Ǯ����
            Dots[i].SetActive(false);

        }
    }

    //���������� ��Ʈ ����ֱ�
    Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position +
            new Vector2(AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) *
            t + 0.5f * (Physics2D.gravity * swordGravity) * (t * t); //������ ������ ����

        return position;
    }
}
