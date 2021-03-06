using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_SerialMovement : MonoBehaviour
{
    public float MaxSpeed = 3.0f; // 최대 속력 변수 
    public float JumpForce = 5.0f; // 점프 가속 변수
    public Rigidbody2D rb2D;
    //public GameObject[] Players = new GameObject[8];
    public List<GameObject> Players;
    int MaxSize = 7;
    public int top = 0;
    public int bottom = 0;
    public float speed = 3f;
    test_PlayerLaunch topPlayer;

    public CameraController camera; /////����
    public GameObject PlayersGameObject;

    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = GameObject.Find("Players");
        Players = new List<GameObject>();
        Transform[] tempPlayers = temp.GetComponentsInChildren<Transform>();
        for (int i = 0; i < tempPlayers.Length; i++)
        {
            Transform child = tempPlayers[i];
            if (child != temp.transform)
                Players.Add(tempPlayers[i].gameObject);
        }
        top = Players.Count - 1;
        for (int i = 0; i < top; i++)
        {
            Players[i].gameObject.AddComponent<PreviousPlayer>();
        }
        Players[top].GetComponent<Rigidbody2D>().gravityScale = 1;
        Players[0].GetComponent<PreviousPlayer>().Previous = Players[1];
        Players[0].GetComponent<CircleCollider2D>().isTrigger = true;
        for (int i = 1; i < top; i++)
        {
            Players[i].GetComponent<CircleCollider2D>().isTrigger = true;
            PreviousPlayer pre = Players[i].GetComponent<PreviousPlayer>();
            pre.Previous = Players[i + 1];
            pre.Next = Players[i - 1];
        }

        rb2D = Players[top].GetComponent<Rigidbody2D>();
        Players[top].AddComponent<test_PlayerLaunch>();
        topPlayer = Players[top].GetComponent<test_PlayerLaunch>();
        camera = GameObject.Find("Main Camera").GetComponent<CameraController>(); ////����
        camera.Player = Players[top]; ////����
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (top != bottom) && !isJumping())
        {
            PreviousPlayer previousPlayer = Players[--top].GetComponent<PreviousPlayer>();
            Destroy(previousPlayer);
            camera.Player = Players[top]; ////����
        }

        if (topPlayer.stop == true)
        {
            //Destroy(Players[top + 1]);
            Players[top].GetComponent<CircleCollider2D>().isTrigger = false;
            rb2D = Players[top].GetComponent<Rigidbody2D>();
            rb2D.gravityScale = 1.0f;
            if (top != bottom)
            {
                Players[top].AddComponent<test_PlayerLaunch>();
                topPlayer = Players[top].GetComponent<test_PlayerLaunch>();
                topPlayer.SetStopFalse();
            }
            else
            {
                Players[top].AddComponent<test_PlayerLaunch>();
                topPlayer = Players[top].GetComponent<test_PlayerLaunch>();
                topPlayer.SetStopFalse();

            }
        }
        
        // 좌우 이동
        float h = Input.GetAxisRaw("Horizontal"); // 키 입력 (A, D)
 
        if (h > 0 && rb2D.velocity.x < 0) rb2D.velocity = new Vector2(-rb2D.velocity.x, rb2D.velocity.y); // 방향 바꿀 때 속도 유지
        else if (h < 0 && rb2D.velocity.x > 0) rb2D.velocity = new Vector2(-rb2D.velocity.x, rb2D.velocity.y);

        rb2D.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        
        if (rb2D.velocity.x > MaxSpeed) // 속도 제한
            rb2D.velocity = new Vector2(MaxSpeed, rb2D.velocity.y);
        else if (rb2D.velocity.x < -MaxSpeed)
            rb2D.velocity = new Vector2(-MaxSpeed, rb2D.velocity.y);
        
        if (Input.GetButtonUp("Horizontal")) // 방향키 떼면 정지
        {
            rb2D.AddForce(new Vector2(-0.7f*rb2D.velocity.x, 0), ForceMode2D.Impulse);
        }

        // 점프
        if (!isJumping() && !topPlayer.isLaunching) 
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rb2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }

    public bool isJumping()
    {
        if (rb2D.velocity.y == 0) return false; //상하, 원형으로 움직이는 플랫폼에서 y축 속력이 생겨서 점프가 안됨
        else return true;
    }

    public void OnTriggerstar(Collider2D other)
    {
        Players[top].GetComponent<CircleCollider2D>().isTrigger = true;
        GameObject newTop = Instantiate(other.gameObject.GetComponent<StarSetting>().Player, Players[top].transform.position, Quaternion.identity, PlayersGameObject.transform);
        other.gameObject.SetActive(false);
        Players.Add(newTop);

        Destroy(topPlayer);

        Players[top].gameObject.AddComponent<PreviousPlayer>();
        PreviousPlayer pre = Players[top].GetComponent<PreviousPlayer>();
        pre.Previous = Players[top + 1];
        pre.Next = Players[top - 1];

        Vector3 currentVelocity = rb2D.velocity;
        rb2D.velocity = new Vector3(0, 0, 0);
        rb2D.gravityScale = 0.0f;
        rb2D = Players[++top].GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 1.0f;
        rb2D.AddForce(currentVelocity, ForceMode2D.Impulse);

        Players[top].gameObject.AddComponent<test_PlayerLaunch>();
        topPlayer = Players[top].GetComponent<test_PlayerLaunch>();
        camera.Player = Players[top];

    }
}
