using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialMovement : MonoBehaviour
{
    public float JumpForce = 5.0f;
    public Rigidbody2D rb2D;
    public GameObject[] Players = new GameObject[8];
    int MaxSize = 7;
    public int top = 0;
    public int bottom = 0; 
    public float speed;
    PlayerLaunch topPlayer;

    // Start is called before the first frame update
    void Start()
    {
        top = 2;
        Players[0].GetComponent<PreviousPlayer>().Previous = Players[1];
        Players[0].GetComponent<CircleCollider2D>().isTrigger = true;
        for(int i = 1; i < top; i++) 
        {
            Players[i].GetComponent<CircleCollider2D>().isTrigger = true;
            PreviousPlayer pre = Players[i].GetComponent<PreviousPlayer>();
            pre.Previous = Players[i+1];
            pre.Next = Players[i-1];
        }
        //Players[bottom].GetComponent<PreviousPlayer>().SetLastTrue();
        rb2D = Players[top].GetComponent<Rigidbody2D>();
        Players[top].AddComponent<PlayerLaunch>();
        topPlayer = Players[top].GetComponent<PlayerLaunch>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A)) Players[top].transform.Translate(-1*speed*Time.deltaTime,0,0);
        if(Input.GetKey(KeyCode.D)) Players[top].transform.Translate(1*speed*Time.deltaTime,0,0);

        if(Input.GetMouseButtonDown(0) && (top != bottom) && !isJumping())
        {
            PreviousPlayer previousPlayer = Players[--top].GetComponent<PreviousPlayer>();
            Destroy(previousPlayer);
        }

        if(topPlayer.stop == true)
        {
            Destroy(Players[top+1]);
            Players[top].GetComponent<CircleCollider2D>().isTrigger = false;
            rb2D = Players[top].GetComponent<Rigidbody2D>();
            rb2D.gravityScale = 1.0f;
            if(top != bottom) 
            {
                Players[top].AddComponent<PlayerLaunch>();
                topPlayer = Players[top].GetComponent<PlayerLaunch>();
                topPlayer.SetStopFalse();
            }
        }

        if (!isJumping())
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
}
