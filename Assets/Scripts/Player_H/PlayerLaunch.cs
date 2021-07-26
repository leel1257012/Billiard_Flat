using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaunch : MonoBehaviour
{
    Camera Camera;
    Vector2 Direction;
    Vector2 MousePosition;
    private Rigidbody2D rb;
    public float speed, deceleration;
    public float maxSpeed, minSpeed;
    bool UpDown, moved;
    public bool stop;

    Vector3 colPos;
    Vector3 colLocalScale;
    int check;
    
    // Start is called before the first frame update
    void Start()
    {
        moved = false;
        stop = false; //플레이가 움직이는 중인지
        UpDown = true; //up == true, Down == false
        maxSpeed = 10;
        speed = minSpeed = 3;
        deceleration = 0.99f;
        rb = GetComponent<Rigidbody2D>();
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moved == false)
        {
            if(Input.GetMouseButtonDown(0))
            {
                MousePosition = Input.mousePosition;
                MousePosition = Camera.ScreenToWorldPoint(MousePosition);
                Direction = MousePosition - rb.position;
                Direction = Direction.normalized;
            }
            if(Input.GetMouseButton(0))
            {
                if(UpDown == true)
                {
                    speed *= 1.05f;
                    if(speed >= maxSpeed) UpDown = false;
                }
                else
                {
                    speed *= 0.95f;
                    if(speed <= minSpeed) UpDown = true;
                }

            }
            if(Input.GetMouseButtonUp(0))
            {
                speed = minSpeed;
                rb.gravityScale = 0;
                moved = true;
            }
        }
    }

    void FixedUpdate()
    {
        if(moved == true) 
        {
            if(speed > 0.3)
            {
                rb.MovePosition(rb.position + Direction * speed * Time.fixedDeltaTime);
                speed *= deceleration;
            }
            else 
            {
                moved = false;
                stop = true;
            }
        }
    }

    void CheckCollisionSide() // 충돌 방향
    {
        if(Mathf.Abs(colPos.x - transform.position.x) < (float)(transform.localScale.x + colLocalScale.x)/2 - 0.1) check = 1; //up & down
        else check = 2; //right & left
    }

    void OnCollisionEnter2D(Collision2D col) // 충돌 시 방향전환
    {
        colPos = col.transform.position;
        colLocalScale = col.transform.localScale;
        CheckCollisionSide();
        if(check == 1)
        {
            Direction.y *= -1;
            
        }
        else
        {
            Direction.x *= -1;
        }
    }

    public void SetStopFalse()
    {
        this.stop = false;
    }
}
