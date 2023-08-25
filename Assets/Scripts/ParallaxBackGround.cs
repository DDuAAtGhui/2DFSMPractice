using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxBackGround : MonoBehaviour
{
    GameObject camera;

    [SerializeField] float parallaxEffect;

    float X_Pos;
    void Start()
    {
        camera= GameObject.Find("Main Camera");

        X_Pos = transform.position.x;
    }

    void Update()
    {
        float distanceToMove = camera.transform.position.x * parallaxEffect;

        transform.position = new Vector3(X_Pos + distanceToMove, transform.position.y);
    }
}
