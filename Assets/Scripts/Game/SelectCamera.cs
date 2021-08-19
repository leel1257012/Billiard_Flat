using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCamera : MonoBehaviour
{

    public Camera player;
    public Camera map;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            map.gameObject.SetActive(true);
            player.gameObject.SetActive(false);
        }
        if (Input.GetKeyUp("m"))
        {
            map.gameObject.SetActive(false);
            player.gameObject.SetActive(true);
        }
    }
}
