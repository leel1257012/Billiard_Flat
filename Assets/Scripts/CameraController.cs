using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    private Vector3 offset;

    public Camera playerCam;
    public Camera map;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0,0,-10);
        map.gameObject.SetActive(false);
        playerCam.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        playerCam.transform.position = Player.transform.position + offset;

        //if (Input.GetKeyDown("m"))
        //{
        //    map.gameObject.SetActive(true);
        //    playerCam.gameObject.SetActive(false);
        //}
        //if (Input.GetKeyUp("m"))
        //{
        //    map.gameObject.SetActive(false);
        //    playerCam.gameObject.SetActive(true);
        //}
    }

    public void SetPlayer(GameObject newPlayer)
    {
        Player = newPlayer;
    }

    public void switchToMap()
    {
        map.gameObject.SetActive(true);
        playerCam.gameObject.SetActive(false);
    }

    public void switchToPlayer()
    {
        map.gameObject.SetActive(false);
        playerCam.gameObject.SetActive(true);
    }
}
