using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceWind : MonoBehaviour
{
    public GameObject fan;
    public GameObject wind;
    public int direction; // 좌상우 012
    float scale;
    // Start is called before the first frame update
    void Start()
    {
        scale = this.gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RefreshWindSize()
    {   
        float boxsize = (float)0.5*scale;
        var collider = wind.GetComponent<BoxCollider2D>();
        
        collider.enabled = false;
        var hit = Physics2D.BoxCast(
            new Vector2((float)(this.gameObject.transform.position.x-boxsize*2),this.gameObject.transform.position.y),
            new Vector2(boxsize,boxsize),
            0,
            new Vector2(-1,0)
            );
        collider.enabled = true;

        var hitBounds = hit.collider.bounds;
        var width = this.gameObject.transform.position.x - 0.25*scale - (hitBounds.center.x + hitBounds.extents.x);
        Debug.Log(width);
        wind.transform.position = new Vector3((float)(this.gameObject.transform.position.x-(width/2.0 + 0.25)*scale),0,0);
        wind.transform.localScale = new Vector3((float)width*2*scale,1,0);
        
    }
}
