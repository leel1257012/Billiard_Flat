using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaunch_H : MonoBehaviour
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
    PlatformSpawn spawn;
    //public GameObject platform;

    float minTime = 0, currentTime = 0, time = 0; ////수정
    public float chargeTime = 1.5f;
    ArrowController arrow; ////

    
    // Start is called before the first frame update
    void Start()
    {
        moved = false;
        stop = false; //플레이가 움직이는 중인지
        UpDown = true; //up == true, Down == false
        maxSpeed = 10;
        speed = minSpeed = 1;
        deceleration = 0.99f;
        rb = GetComponent<Rigidbody2D>();
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //platform = GameObject.Find("TestPlatform");
        spawn = GameObject.Find("PlatformSpawner").GetComponent<PlatformSpawn>();
        arrow = GameObject.Find("ArrowSpawner").GetComponent<ArrowController>(); ////수정
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime; ////수정
        if(moved == false)
        {
            if(Input.GetMouseButtonDown(0))
            {
                speed = minSpeed;
                MousePosition = Input.mousePosition;
                MousePosition = Camera.ScreenToWorldPoint(MousePosition);
                Direction = MousePosition - rb.position;
                Direction = Direction.normalized;
                minTime = currentTime; //// 수정
                //arrow.Instant(transform.position,Direction); 
                arrow.chargebarSpawn(transform.position); ////
            }
            if(Input.GetMouseButton(0)) ////수정
            {
                MousePosition = Input.mousePosition; //중간에 발사위치 조정 가능하도록
                MousePosition = Camera.ScreenToWorldPoint(MousePosition);
                Direction = MousePosition - rb.position;
                Direction = Direction.normalized; 
                arrow.Setting(transform.position,Direction);

                if(UpDown == true) //speed 기준에서 시간 기준으로 바꿈
                {
                    if(currentTime - minTime >= chargeTime) 
                    {
                        UpDown = false;
                        minTime = currentTime = 0;
                    }
                }
                else
                {
                    if(currentTime - minTime >= chargeTime) 
                    {
                        UpDown = true;
                        minTime = currentTime = 0;
                    }
                }////


            }
            if(Input.GetMouseButtonUp(0))
            {
                rb.gravityScale = 0;
                moved = true;
                time = currentTime - minTime; ////수정
                if(UpDown == true)
                {
                    speed = minSpeed + (maxSpeed - minSpeed) * (time / chargeTime);
                }
                else
                {
                    speed = maxSpeed - (maxSpeed - minSpeed) * (time / chargeTime);
                }
                arrow.DisInstant();
                arrow.chargebarDestroy(); ////
            }
        }
    }

    void FixedUpdate()
    {
        if(moved == true) 
        {
            if(speed > 0.3)
            {
                //rb.MovePosition(rb.position + Direction * speed * Time.fixedDeltaTime);
                rb.velocity = Direction * speed;
                speed *= deceleration;
            }
            else 
            {
                moved = false;
                stop = true;
                //this.platform = GameObject.Find("TestPlatform");
                //Instantiate(platform, transform.position, Quaternion.identity);
                spawn.Platform1(transform.position);
            }
        }
    }

    //void CheckCollisionSide() // 충돌 방향
    //{
    //    if(Mathf.Abs(colPos.x - transform.position.x) < (float)(transform.localScale.x + colLocalScale.x)/2 - 0.1) check = 1; //up & down
    //    else check = 2; //right & left
    //}

    //void OnCollisionEnter2D(Collision2D col) // 충돌 시 방향전환
    //{
    //    colPos = col.transform.position;
    //    colLocalScale = col.transform.localScale;
    //    CheckCollisionSide();
    //    if(check == 1)
    //    {
    //        Direction.y *= -1;

    //    }
    //    else
    //    {
    //        Direction.x *= -1;
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normalVector = collision.contacts[0].normal;
        Vector2 reflectVector = Vector2.Reflect(Direction, normalVector);
        Direction = reflectVector.normalized;
    }



    public void SetStopFalse()
    {
        this.stop = false;
    }
}
