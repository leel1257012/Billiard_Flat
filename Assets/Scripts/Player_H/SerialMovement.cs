using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialMovement : MonoBehaviour
{
    public float JumpForce = 5.0f;
    public Rigidbody2D rb2D;
    //public GameObject[] Players = new GameObject[8];
    public List<GameObject> Players;
    int MaxSize = 7;
    public int top = 0;
    public int bottom = 0;
    public float speed = 3f;
    PlayerLaunch topPlayer;

    public CameraController camera; /////����
    public GameObject temp;

    // Start is called before the first frame update
    void Start()
    {
        temp = GameObject.Find("Players");
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
        Players[top].AddComponent<PlayerLaunch>();
        topPlayer = Players[top].GetComponent<PlayerLaunch>();
        camera = GameObject.Find("Main Camera").GetComponent<CameraController>(); ////����
        camera.Player = Players[top]; ////����
    }

    // Update is called once per frame
    void Update()
    {
        if (!topPlayer.moved && !topPlayer.isLaunching)
        {
            if (Input.GetKey(KeyCode.A)) Players[top].transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
            if (Input.GetKey(KeyCode.D)) Players[top].transform.Translate(1 * speed * Time.deltaTime, 0, 0);
        }
        
        if (Input.GetMouseButtonDown(0) && (top != bottom) && !isJumping())
        {
            PreviousPlayer previousPlayer = Players[--top].GetComponent<PreviousPlayer>();
            Destroy(previousPlayer);
            camera.Player = Players[top]; ////����
        }

        if (topPlayer.stop == true)
        {
            //Destroy(Players[top + 1]);
            Players.Remove(Players[top+1]);
            Players[top].GetComponent<CircleCollider2D>().isTrigger = false;
            rb2D = Players[top].GetComponent<Rigidbody2D>();
            rb2D.gravityScale = 1.0f;
            if (top != bottom)
            {
                Players[top].AddComponent<PlayerLaunch>();
                topPlayer = Players[top].GetComponent<PlayerLaunch>();
                topPlayer.SetStopFalse();
            }
            else
            {
                Players[top].AddComponent<PlayerLaunch>();
                topPlayer = Players[top].GetComponent<PlayerLaunch>();
                topPlayer.SetStopFalse();

            }
        }
        if (!isJumping() && !topPlayer.isLaunching)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rb2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }
    public bool isJumping()
    {
        if (rb2D.velocity.y == 0) return false;
        else return true;
    }

    public void OnTriggerstar(Collider2D other)
    {
        Players[top].GetComponent<CircleCollider2D>().isTrigger = true;
        GameObject newTop = Instantiate(other.gameObject.GetComponent<StarSetting>().Player, Players[top].transform.position, Quaternion.identity, temp.transform);
        other.gameObject.SetActive(false);
        Players.Add(newTop);

        Destroy(topPlayer);

        Players[top].gameObject.AddComponent<PreviousPlayer>();
        PreviousPlayer pre = Players[top].GetComponent<PreviousPlayer>();
        pre.Previous = Players[top + 1];
        if(top != 0) pre.Next = Players[top - 1];

        Vector3 currentVelocity = rb2D.velocity;
        rb2D.velocity = new Vector3(0, 0, 0);
        rb2D.gravityScale = 0.0f;
        rb2D = Players[++top].GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 1.0f;
        rb2D.AddForce(currentVelocity, ForceMode2D.Impulse);

        Players[top].gameObject.AddComponent<PlayerLaunch>();
        topPlayer = Players[top].GetComponent<PlayerLaunch>();
        camera.Player = Players[top];

    }
}
