using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pf_MovingLR : MonoBehaviour
{
    public bool LeftRight; //L = true, R = false; 맵 시작 시 true로 설정되어있으면 왼쪽으로 먼저 올라감
    public float speed;
    public float distance; //출발점과 도착점 사이의 거리. Scene에 플랫폼이 설치된 초기 위치를 중점으로 함
    Vector2 pos0, pos1;

    // Start is called before the first frame update
    void Start()
    {
        if (LeftRight)
        {
            pos0 = new Vector2(transform.position.x - distance / 2, transform.position.y);
            pos1 = new Vector2(transform.position.x + distance / 2, transform.position.y);
        }
        else
        {
            pos0 = new Vector2(transform.position.x + distance / 2, transform.position.y);
            pos1 = new Vector2(transform.position.x - distance / 2, transform.position.y);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, pos0, Time.deltaTime * speed);

        if (Vector2.Distance(transform.position, pos0) <= 0.05f)
        {
            Vector2 temp_pos = pos0;
            pos0 = pos1;
            pos1 = temp_pos;
        }
    }
}