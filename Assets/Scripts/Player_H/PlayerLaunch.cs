using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaunch : MonoBehaviour
{
    SerialMovement SerialMovement;

    Camera Camera;
    Vector2 Direction;
    Vector2 MousePosition;
    Rigidbody2D rbPlayer;
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
    
    // Start is called before the first frame update
    void Start()
    {
        SerialMovement = GameObject.Find("SerialMoving").GetComponent<SerialMovement>();
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
    }

    // Update is called once per frame
    void Update()
    {
        if(moved == false && !SerialMovement.isJumping())
        {
            if(Input.GetMouseButtonDown(0))
            {
                speed = minSpeed;
                MousePosition = Input.mousePosition;
                MousePosition = Camera.ScreenToWorldPoint(MousePosition);
                Direction = MousePosition - rb.position;
                Direction = Direction.normalized;
            }
            if(Input.GetMouseButton(0))
            {
                if(UpDown == true)
                {
                    speed *= 1.02f;
                    if(speed >= maxSpeed) UpDown = false;
                }
                else
                {
                    speed *= 0.98f;
                    if(speed <= minSpeed) UpDown = true;
                }

            }
            if(Input.GetMouseButtonUp(0))
            {
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
