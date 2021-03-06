using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerLaunch_HS : MonoBehaviour
{
    public SerialMovement_HS SerialMovement;

    Camera Camera;
    Vector2 Direction;
    Vector2 MousePosition;
    Rigidbody2D rbPlayer;
    private Rigidbody2D rb;
    public float speed, deceleration;
    public float maxSpeed, minSpeed;
    public bool UpDown, moved;
    public bool stop;
    public bool singular = false;
    public bool mouseDown = false; //점프 중이 아닐 때 마우스가 눌렸는지
    public draw_UI draw;
    private LevelManager levelManager;


    Vector3 colPos;
    Vector3 colLocalScale;
    int check;
    PlatformSpawn_HS spawn;
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
        levelManager = LevelManager.instance;
        draw = GameObject.Find("GameManager").GetComponent<draw_UI>();
        SerialMovement = GameObject.Find("SerialMoving").GetComponent<SerialMovement_HS>();
        levelManager.isLaunching = false;
        moved = false;
        stop = false; //플레이가 움직이는 중인지
        UpDown = true; //up == true, Down == false
        maxSpeed = 10;
        speed = minSpeed = 1;
        deceleration = 0.99f;
        rb = GetComponent<Rigidbody2D>();
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //platform = GameObject.Find("TestPlatform");
        spawn = GameObject.Find("PlatformSpawner").GetComponent<PlatformSpawn_HS>();
        arrow = GameObject.Find("ArrowSpawner").GetComponent<ArrowController>(); ////수정
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime; ////수정
        if (moved == false && !SerialMovement.isJumping() && levelManager.playerCount > 1)
        {
            if(Input.GetMouseButtonDown(0))
            {
                levelManager.isLaunching = true;
                speed = minSpeed;
                MousePosition = Input.mousePosition;
                MousePosition = Camera.ScreenToWorldPoint(MousePosition);
                Direction = MousePosition - rb.position;
                Direction = Direction.normalized;
                minTime = currentTime; //// 수정
                //arrow.Instant(transform.position, Direction);
                arrow.chargebarSpawn(transform.position); ////
                mouseDown = true;
            }
            if(Input.GetMouseButton(0) && mouseDown)
            {
                MousePosition = Input.mousePosition; //중간에 발사위치 조정 가능하도록
                MousePosition = Camera.ScreenToWorldPoint(MousePosition);
                //Direction = MousePosition - rb.position;
                //Direction = Direction.normalized;
                //arrow.Setting(transform.position, Direction);

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
            if(Input.GetMouseButtonUp(0) && mouseDown)
            {
                levelManager.playerCount--;
                levelManager.gameUI.GetComponent<draw_UI>().BallImageUpdate();
                //draw.UseCurrentBall();
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
                //arrow.DisInstant();
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
                levelManager.isLaunching = false;
                moved = false;
                stop = true;
                mouseDown = false;
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
            //얼음 플랫폼
            if (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.SlipFloor)
            {

            }

            //한 번 밟으면 2초 후 사라지는 플랫폼
            if (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.DisposableFloor)
            {
                if (collision.gameObject.GetComponent<Pf_Disappearing>().disappear == 0)
                {
                    Destroy(collision.transform.parent.gameObject, 2.0f);
                    collision.gameObject.GetComponent<Pf_Disappearing>().disappear = 1;
                }
            }

            //점프력 향상 플랫폼
            if (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.JumpFloor && !SerialMovement.isJumping())
            {
                SerialMovement.JumpForce = 7.5f;
            }

            //슬로우 플랫폼
            if (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.SlowFloor && !SerialMovement.isJumping())
            {
                SerialMovement.speed = 2f;
            }

            //움직이는 플랫폼
            if (((collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.MovingFloorUpDown) ||
                (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.MovingFloorLeftRight) ||
                (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.MovingFloorCircle)) && !SerialMovement.isJumping())
            {
                transform.SetParent(collision.transform);
            }

        }
        #endregion

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        #region platforms
        if (collision.gameObject.GetComponent<MapEditorFloor>() != null && !moved)
        {
            //점프력 향상 플랫폼
            if (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.JumpFloor)
            {
                SerialMovement.JumpForce = 5.0f;
            }

            //슬로우 플랫폼
            if (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.SlowFloor)
            {
                SerialMovement.speed = 3f;
            }

            //움직이는 플랫폼
            if (((collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.MovingFloorUpDown) ||
                (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.MovingFloorLeftRight) ||
                (collision.gameObject.GetComponent<MapEditorFloor>().thisFloor == FloorType.MovingFloorCircle)))
            {
                GameObject players = GameObject.Find("Players");
                transform.SetParent(players.transform);
            }

        }
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "GameOverZone" && !moved)
        {
            SceneManager.LoadScene("LevelSelect");
        }
        if (collision.gameObject.CompareTag("Star"))
        {

            SerialMovement.OnTriggerstar(collision);

        }
        if (collision.gameObject.CompareTag("Hole") && levelManager.isLaunching) // 구멍
        {
            levelManager.isLaunching = false;
            moved = false;
            stop = true;
            mouseDown = false;
            Destroy(gameObject);

            //levelManager.playerCount--;
            //levelManager.gameUI.GetComponent<draw_UI>().BallImageUpdate();

            // PlatformSpawn.cs에서 복사됨
            FloorType cur = levelManager.curPlayers[levelManager.curPlayers.Count - 1];
            levelManager.curPlayers.RemoveAt(levelManager.curPlayers.Count - 1);
            SerialMovement.camera.Player = SerialMovement.Players[SerialMovement.top];
        }
        if (collision.gameObject.CompareTag("Wind"))
        {
            switch(collision.transform.parent.gameObject.GetComponent<DeviceWind>().direction){
                case 1:
                    //Debug.Log("0");
                    var velocity = SerialMovement.rb2D.velocity;
                    velocity.y = 0f;
                    SerialMovement.rb2D.velocity = velocity;
                    
                    SerialMovement.rb2D.gravityScale=0f;
                    break;
            }
            
        }
        if (collision.gameObject.CompareTag("LaserBullet"))
        {
            SceneManager.LoadScene("LevelSelect");
        }
        if (collision.gameObject.CompareTag("Portal"))
        {
            var component = collision.gameObject.GetComponent<DevicePortal>();
            if(component.inOut)
            {
                var pairLocation = component.pair.transform.position;
                gameObject.transform.position = pairLocation;
            }
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == "CheckGoal" && !moved)
        {
            SceneManager.LoadScene("LevelSelect");
        }
        if (collision.gameObject.CompareTag("Wind"))
        {
            switch(collision.transform.parent.gameObject.GetComponent<DeviceWind>().direction){
                case 0:
                    SerialMovement.modifyVelocity(new Vector2(-0.1f,0));
                    break;
                case 2:
                    SerialMovement.modifyVelocity(new Vector2(0.1f,0));
                    break;
            }
            
        }

    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wind"))
        {
            switch(collision.transform.parent.gameObject.GetComponent<DeviceWind>().direction){
                case 1:
                    //Debug.Log("1");
                    SerialMovement.rb2D.gravityScale=1f;
                    break;
            }
            
        }
    }

    public void SetStopFalse()
    {
        this.stop = false;
    }

}
