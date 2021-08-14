using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pf_MovingCircle : MonoBehaviour
{
    public bool counterClock; //반시계방향 = true, 시계방향 = false;
    public float speed; //각 변화 속도
    public float r; //원 궤도 반지름
    float angle = 0; //현재 플랫폼과 Center 사이의 상대적 위치를 각도 0도로 잡음
    private Vector2 init;

    private LevelManager levelManager;

    private void Start()
    {
        levelManager = LevelManager.instance;
        init = new Vector2(transform.position.x, transform.position.y);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!levelManager.isLaunching)
        {
            if (counterClock) angle += speed;
            else angle -= speed;

            if (angle > 360) angle -= 360;
            if (angle < 0) angle += 360;

            transform.position = new Vector2(init.x + r * Mathf.Cos(Mathf.Deg2Rad * angle), init.y + r * Mathf.Sin(Mathf.Deg2Rad * angle));
        }

    }
}
