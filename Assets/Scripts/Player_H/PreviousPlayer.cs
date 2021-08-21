using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousPlayer : MonoBehaviour
{
    public GameObject Previous;
    public GameObject Next;
    //public bool last = false;
    public Vector3[] Pos = new Vector3[10];
    int ten = 0, SavePos = 0;

    private LevelManager levelManager;
    public bool movingPlatform = false;
    // Start is called before the first frame update


    void Start()
    {
        levelManager = LevelManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (!levelManager.isLaunching)
        {
            if (ten < 10)
            {
                Pos[ten] = Previous.transform.position;
                ten++;
            }
            else if (ten == 10)
            {
                ten++;
            }
            if (ten > 10)
            {
                SavePos %= 10;
                if(movingPlatform == false) this.transform.position = Pos[SavePos] - new Vector3(0, 0, -1);
                else this.transform.position = (Previous.transform.position + this.transform.position)/2 - new Vector3(0, 0, -1);
                Pos[SavePos++] = Previous.transform.position;
            }
        }

    }

    /*public void SetLastTrue()
    {
        this.last = true;
    }*/
}
