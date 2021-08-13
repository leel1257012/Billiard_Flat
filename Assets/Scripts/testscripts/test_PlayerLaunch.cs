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

    float minTime = 0, currentTime = 0, time = 0; ////����
    public float chargeTime = 1.5f;
    ArrowController arrow; ////

    // Start is called before the first frame update
    void Start()
    {
        test_SerialMovement = GameObject.Find("SerialMoving").GetComponent<test_SerialMovement>();
        isLaunching = false;
        moved = false;
        stop = false; //�÷��̰� �����̴� ������
        UpDown = true; //up == true, Down == false
        maxSpeed = 10;
        speed = minSpeed = 1;
        deceleration = 0.99f;
        rb = GetComponent<Rigidbody2D>();
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //platform = GameObject.Find("TestPlatform");
        spawn = GameObject.Find("PlatformSpawner").GetComponent<PlatformSpawn>();
        arrow = GameObject.Find("ArrowSpawner").GetComponent<ArrowController>(); ////����
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerCount();
        currentTime += Time.deltaTime; ////����
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
                minTime = currentTime; //// ����
                arrow.Instant(transform.position, Direction);
                arrow.chargebarSpawn(transform.position); ////
            }
            if (Input.GetMouseButton(0))
            {
                MousePosition = Input.mousePosition; //�߰��� �߻���ġ ���� �����ϵ���
                MousePosition = Camera.ScreenToWorldPoint(MousePosition);
                Direction = MousePosition - rb.position;
                Direction = Direction.normalized;
                arrow.Setting(transform.position, Direction);

                if (UpDown == true) //speed ���ؿ��� �ð� �������� �ٲ�
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
                time = currentTime - minTime; ////����
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
        if (collision.gameObject.GetComponent<MapEditorFloor>() != null && !moved)
        {
            //���� �÷���
            if (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.SlipFloor)
            {

            }

            //�� �� ������ 2�� �� ������� �÷���
            if (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.DisposableFloor)
            {
                Destroy(collision.gameObject, 2.0f);
                collision.gameObject.GetComponent<Pf_Disappearing>().disappear = 1;
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
