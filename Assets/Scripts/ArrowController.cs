using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameObject Arrow;
    public GameObject Chargebar;
    GameObject arrow;
    GameObject chargebar;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Instant(Vector3 Player, Vector3 Direction)
    {
        arrow = Instantiate(Arrow, Player + Direction, Quaternion.identity);
    }

    public void DisInstant()
    {
        Destroy(arrow);
    }

    public void Setting(Vector3 Player, Vector3 Direction)
    {
        arrow.transform.position = Player+Direction*2;
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(0, 0, Mathf.Atan2(Direction.y,Direction.x) * Mathf.Rad2Deg - 90);
        arrow.transform.rotation = rot;

    }

    public void chargebarSpawn(Vector3 Player)
    {
        chargebar = Instantiate(Chargebar, Player + new Vector3(-0.5f, 0.5f,0), Quaternion.identity);
    }

    public void chargebarDestroy()
    {
        Destroy(chargebar);
    }
}
