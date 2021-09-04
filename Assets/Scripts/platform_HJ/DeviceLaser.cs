using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceLaser : MonoBehaviour
{
    public GameObject bullet;
    public GameObject laser;
    int state = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("shoot",1f,2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void shoot()
    {
        Instantiate(bullet,gameObject.transform.position,Quaternion.identity,gameObject.transform);
    }
}
