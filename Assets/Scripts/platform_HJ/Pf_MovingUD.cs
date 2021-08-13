using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pf_MovingUD : MonoBehaviour
{
    public bool UpDown; //up = true, down = false; 맵 시작 시 true로 설정되어있으면 위쪽으로 먼저 올라감
    public float speed;
    public float distance; //출발점과 도착점 사이의 거리. Scene에 플랫폼이 설치된 초기 위치를 중점으로 함
    Vector2 pos0, pos1;

    // Start is called before the first frame update
    void Start()
    {
        if (UpDown)
        {
            pos0 = new Vector2(transform.position.x, transform.position.y + distance / 2);
            pos1 = new Vector2(transform.position.x, transform.position.y - distance / 2);
        }
        else
        {
            pos0 = new Vector2(transform.position.x, transform.position.y - distance / 2);
            pos1 = new Vector2(transform.position.x, transform.position.y + distance / 2);
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
