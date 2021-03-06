using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_PlayerLaunch : MonoBehaviour
{
    public test_SerialMovement test_SerialMovement;

    Camera Camera;
    Vector2 Direction;
    Vector2 MousePosition;
    Rigidbody2D rbPlayer;
    private Rigidbody2D rb;
    public float speed, deceleration;
    public float maxSpeed, minSpeed;
    public bool UpDown, moved;
    public bool stop;
    public bool isLaunching;
    public bool singular = false;

    Vector3 colPos;
    Vector3 colLocalScale;
    int check;
    PlatformSpawn spawn;
    //public GameObject platform;

    float minTime = 0, currentTime = 0, time = 0; ////수정
    public float chargeTime = 1.5f;
    ArrowController arrow; ////

    //움직이는 플랫폼 용
    bool onPlatform = false;
    GameObject contactedPlatform;
    Vector3 platformPos;
    Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        test_SerialMovement = GameObject.Find("SerialMoving").GetComponent<test_SerialMovement>();
        isLaunching = false;
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
        checkPlayerCount();
        currentTime += Time.deltaTime; ////수정
        if (moved == false && !test_SerialMovement.isJumping() && !singular)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isLaunching = true;
                speed = minSpeed;
                MousePosition = Input.mousePosition;
                MousePosition = Camera.ScreenToWorldPoint(MousePosition);
                Direction = MousePosition - rb.position;
                Direction = Direction.normalized;
                minTime = currentTime; //// 수정
                arrow.Instant(transform.position, Direction);
                arrow.chargebarSpawn(transform.position); ////
            }
            if (Input.GetMouseButton(0))
            {
                MousePosition = Input.mousePosition; //중간에 발사위치 조정 가능하도록
                MousePosition = Camera.ScreenToWorldPoint(MousePosition);
                Direction = MousePosition - rb.position;
                Direction = Direction.normalized;
                arrow.Setting(transform.position, Direction);

                if (UpDown == true) //speed 기준에서 시간 기준으로 바꿈
                {
                    if (currentTime - minTime >= chargeTime)
                    {
                        UpDown = false;
                        minTime = currentTime = 0;
                    }
                }
                else
                {
                    if (currentTime - minTime >= chargeTime)
                    {
                        UpDown = true;
                        minTime = currentTime = 0;
                    }
                }////

            }
            if (Input.GetMouseButtonUp(0))
            {
                isLaunching = false;
                rb.gravityScale = 0;
                moved = true;
                time = currentTime - minTime; ////수정
                if (UpDown == true)
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

        //움직이는 플랫폼
        float h = Input.GetAxisRaw("Horizontal"); // 키 입력 (A, D)
        if ((onPlatform) && (h == 0)) //좌우 이동이 없을 때 플랫폼 탑승 위치 고정
        {
            transform.position = contactedPlatform.transform.position - distance;
        }
    }

    void FixedUpdate()
    {
        if (moved == true)
        {
            if (speed > 0.3)
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
                Destroy(gameObject);
                spawn.Platform1(transform.position);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.GetComponent<MapEditorFloor>() != null && !moved)
        //{
        //    if(collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.Goal)
        //    {
        //        Debug.Log("Goal Reached!");
        //    }
        //}

        Vector2 normalVector = collision.contacts[0].normal;
        Vector2 reflectVector = Vector2.Reflect(Direction, normalVector);
        Direction = reflectVector.normalized;

        #region platforms
        if (collision.gameObject.GetComponent<MapEditorFloor>() != null)
        {

            //한 번 밟으면 2초 후 사라지는 플랫폼
            if (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.DisposableFloor)
            {
                if (collision.gameObject.GetComponent<Pf_Disappearing>().disappear == 0)
                {
                    Destroy(collision.transform.parent.gameObject, 2.0f);
                    collision.gameObject.GetComponent<Pf_Disappearing>().disappear = 1;
                }
            }

            //움직이는 플랫폼
            if (((collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.MovingFloorUpDown) ||
                (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.MovingFloorLeftRight) ||
                (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.MovingFloorCircle)) && !test_SerialMovement.isJumping())
            {
                transform.SetParent(collision.transform);
            }

        }
        #endregion

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        #region platforms

        if (collision.gameObject.GetComponent<MapEditorFloor>() != null)
        {
            //점프력 향상 플랫폼
            if (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.JumpFloor && !test_SerialMovement.isJumping())
            {
                test_SerialMovement.JumpForce = 7.5f;
            }

            //슬로우 플랫폼
            if (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.SlowFloor && !test_SerialMovement.isJumping())
            {
                test_SerialMovement.MaxSpeed = 2f;
            }

        }
        #endregion

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        #region platforms
        if (collision.gameObject.GetComponent<MapEditorFloor>() != null)
        {
            //점프력 향상 플랫폼
            if (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.JumpFloor)
            {
                test_SerialMovement.JumpForce = 5.0f;
            }

            //슬로우 플랫폼
            if (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.SlowFloor)
            {
                test_SerialMovement.MaxSpeed = 3f;
            }

            //움직이는 플랫폼
            if ((collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.MovingFloorUpDown) ||
                (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.MovingFloorLeftRight) ||
                (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.MovingFloorCircle))
            {
                transform.SetParent(null);
            }

        }
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "GameOverZone" && !moved) Debug.Log("GameOver!");
        if (collision.gameObject.CompareTag("Star"))
        {
            Debug.Log("star");
            test_SerialMovement.OnTriggerstar(collision);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "CheckGoal" && !moved) Debug.Log("Goal Reached!");


    }

    public void SetStopFalse()
    {
        this.stop = false;
    }

    private void checkPlayerCount()
    {
        GameObject temp = GameObject.Find("Players");
        List<GameObject> Players;
        Players = new List<GameObject>();
        Transform[] tempPlayers = temp.GetComponentsInChildren<Transform>();
        if (tempPlayers.Length <= 2)
        {
            singular = true;
        }
    }
}
