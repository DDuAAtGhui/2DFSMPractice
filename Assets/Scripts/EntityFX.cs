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

    //코루틴과 인보크는 private로해도 외부에서 호출이 가능
    IEnumerator FlashFX()
    {

        spriteRenderer.material = hitFX;
        yield return new WaitForSeconds(BlinkInterval);
        spriteRenderer.material = originFX;
    }

    //원래색깔 - 노란색 반복하면서 블링크
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
