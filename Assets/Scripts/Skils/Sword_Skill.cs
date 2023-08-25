using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill : Skill
{
    [Header("Skill Info")]
    [SerializeField] GameObject swordPrefab;
    [SerializeField] Vector2 launchForce;
    [SerializeField] float swordGravity;

    //조준했을때 칼 날아갈 방향
    Vector2 finalDir;

    [Header("Aim dots")]
    //점선으로 조준점 표현할거임
    [SerializeField] int numberOfDots;
    //도트 간 공백 간격
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
            //방향값으로 변환하면서 가져옴.
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

        //플레이어에서 칼 한개만 던지고 회수전까지 못던지게 관리
        player.AssignNewSword(newSword);

        //칼 던질 때 포물선 도트 끄기
        DotsActive(false);
    }

    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;

        //마우스 위치를 구해줌. UI에서 월드 좌표로 변환해주는 코드
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //플레이어에서 현재 마우스 위치를 바라보는 벡터값
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    //도트들 활성화
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
            //부모에 자식으로 넣으면서 생성인가
            Dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);

            //조준할 때 점 나와야하니까 그때 SetActive 풀려고
            Dots[i].SetActive(false);

        }
    }

    //포물선으로 도트 찍어주기
    Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position +
            new Vector2(AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) *
            t + 0.5f * (Physics2D.gravity * swordGravity) * (t * t); //포물선 방정식 적용

        return position;
    }
}
