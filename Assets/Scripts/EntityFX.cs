using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    [Header("Flash FX")]
    [SerializeField] Material hitFX;
    [SerializeField] float BlinkInterval = 0.2f;
    Material originFX;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originFX = spriteRenderer.material;
    }

    //�ڷ�ƾ�� �κ�ũ�� private���ص� �ܺο��� ȣ���� ����
    IEnumerator FlashFX()
    {

        spriteRenderer.material = hitFX;
        yield return new WaitForSeconds(BlinkInterval);
        spriteRenderer.material = originFX;
    }

    //�������� - ����� �ݺ��ϸ鼭 ��ũ
    private void Blink_Yellow()
    {
        if (spriteRenderer.color != Color.white) spriteRenderer.color = Color.white;
        else spriteRenderer.color = Color.yellow;
    }

    private void CancleInvoke_Blink_Yellow()
    {
        CancelInvoke("Blink_Yellow");
        spriteRenderer.color = Color.white;
    }
}
