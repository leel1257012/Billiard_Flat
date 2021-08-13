using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pf_MovingCircle : MonoBehaviour
{
    public bool counterClock; //반시계방향 = true, 시계방향 = false;
    public float speed; //각 변화 속도
    public float r; //원 궤도 반지름
    float angle = 0; //현재 플랫폼과 Center 사이의 상대적 위치를 각도 0도로 잡음

    // Update is called once per frame
    void FixedUpdate()
    {
        if (counterClock) angle += speed;
        else angle -= speed;

        if (angle > 360) angle -= 360;
        if (angle < 0) angle += 360;

        transform.position = new Vector2(r * Mathf.Cos(Mathf.Deg2Rad * angle), r * Mathf.Sin(Mathf.Deg2Rad * angle));
    }
}
