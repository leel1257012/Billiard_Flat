using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialMovement : MonoBehaviour
{
    public GameObject[] Players = new GameObject[8];
    //public Script[] Players = new Script[8];
    int MaxSize = 7;
    public int top = 0;
    int bottom = 0;
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
        Players[bottom].GetComponent<PreviousPlayer>().SetLastTrue();
        Players[top-1].GetComponent<PreviousPlayer>().SetStartTrue();
        Players[top].AddComponent<PlayerLaunch>();
        topPlayer = Players[top].GetComponent<PlayerLaunch>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A)) Players[top].transform.Translate(-1*speed*Time.deltaTime,0,0);
        if(Input.GetKey(KeyCode.D)) Players[top].transform.Translate(1*speed*Time.deltaTime,0,0);
        if(Input.GetMouseButtonDown(0))
        {
            PreviousPlayer previousPlayer = Players[top-1].GetComponent<PreviousPlayer>();
            Destroy(previousPlayer);
        }

        if(topPlayer.stop == true)
        {
            Destroy(Players[top--]);
            Players[top].AddComponent<PlayerLaunch>();
            topPlayer = Players[top].GetComponent<PlayerLaunch>();
            topPlayer.SetStopFalse();
        }
    }
}
