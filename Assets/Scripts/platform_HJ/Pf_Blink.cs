using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pf_Blink : MonoBehaviour
{
    public float changeTime = 2.0f;
    public float currentTime = 0;
    public bool enabled = true;

    // Start is called before the first frame update
    void Start()
    {
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= changeTime)
        {
            currentTime = 0;
            togglePlatform();
        }
    }

    void togglePlatform()
    {
        enabled = !enabled;
        GameObject child = transform.Find("realTimedPlatform").gameObject;
        child.SetActive(enabled);
    }
}
