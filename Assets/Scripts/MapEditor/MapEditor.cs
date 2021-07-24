using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapEditor : MonoBehaviour
{

    FloorType floorMode = 0;
    GameObject currentFloor = null;
    public GameObject[] floors;
    Vector3 prevMousePoint;
    Transform walls, platforms, devices, items;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        prevMousePoint = new Vector3(0, 0, -1);
        cam = GetComponent<Camera>();
    }

    private void Awake()
    {
        walls = GameObject.Find("Walls").transform;
        platforms = GameObject.Find("Platforms").transform;
        devices = GameObject.Find("Devices").transform;
        items = GameObject.Find("Items").transform;
        for(int i=0; i<floors.Length; i++)
        {
            floors[i] = Instantiate(floors[i]);
            floors[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool isValid;
        Vector3 mousePoint = GetMousePoint();
        Debug.Log("cur: " + mousePoint);
        //Debug.Log(prevMousePoint);
        if (currentFloor != null && floorMode != 0)     // 블록 선택 확인
        {
            isValid = false;
            currentFloor.transform.position = mousePoint;
            if (prevMousePoint != mousePoint)
            {
                if (Input.GetMouseButton(0))            // 블록 선택이 되어있으면 좌클릭으로 설치
                {
                    if (CheckWall(mousePoint.x, mousePoint.y)) Destroy(CheckWall(mousePoint.x, mousePoint.y).gameObject);
                    if (CheckPlatform(mousePoint.x, mousePoint.y)) Destroy(CheckPlatform(mousePoint.x, mousePoint.y).gameObject);
                    if (CheckDevice(mousePoint.x, mousePoint.y)) Destroy(CheckDevice(mousePoint.x, mousePoint.y).gameObject);
                    if (CheckItem(mousePoint.x, mousePoint.y)) Destroy(CheckItem(mousePoint.x, mousePoint.y).gameObject);

                    GameObject newFloor = null;
                    if (floorMode == FloorType.Wall) newFloor = Instantiate(currentFloor, mousePoint, Quaternion.identity, walls);
                    if (floorMode == FloorType.DisposableFloor || floorMode == FloorType.HookFloor || floorMode == FloorType.MovingFloor
                        || floorMode == FloorType.SlipFloor || floorMode == FloorType.JumpFloor || floorMode == FloorType.SlowFloor
                        || floorMode == FloorType.TimedFloor || floorMode == FloorType.Spawn || floorMode == FloorType.Goal) newFloor = Instantiate(currentFloor, mousePoint, Quaternion.identity, platforms);
                    if (floorMode == FloorType.Device) newFloor = Instantiate(currentFloor, mousePoint, Quaternion.identity, devices);
                    if (floorMode == FloorType.Item) newFloor = Instantiate(currentFloor, mousePoint, Quaternion.identity, items);

                    newFloor.GetComponent<MapEditorFloor>().mapPos = new Vector2(mousePoint.x, mousePoint.y);
                    newFloor.GetComponent<BoxCollider2D>().enabled = true;

                    prevMousePoint = mousePoint;
                } else if (Input.GetMouseButton(1))     // 블록 선택한 상태에서 우클릭으로 삭제
                {
                    Vector3 tempPos = Input.mousePosition;
                    tempPos = Camera.main.ScreenToWorldPoint(tempPos);

                    RaycastHit2D hit = Physics2D.Raycast(tempPos, transform.forward, 15f);
                    if (hit && hit.transform.gameObject != currentFloor) 
                        Destroy(hit.transform.gameObject);
                }
            }

        }
        else if(prevMousePoint != mousePoint)
        {
            if (Input.GetMouseButton(0))            // 블록 선택이 안되어있을 때 좌클릭으로 삭제
            {
                Vector3 tempPos = Input.mousePosition;
                tempPos = Camera.main.ScreenToWorldPoint(tempPos);

                RaycastHit2D hit = Physics2D.Raycast(tempPos, transform.forward, 15f);
                if (hit && hit.transform.gameObject != currentFloor)
                    Destroy(hit.transform.gameObject);
            }
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) prevMousePoint = new Vector3(0, -1, 0);
    }

    Vector3 GetMousePoint()
    {
        Vector3 originPos = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        Vector3 mousePoint = new Vector3(Mathf.Round(originPos.x), Mathf.Round(originPos.y));
        return mousePoint;
    }

    public void ChangeFloorType(int _floorMode)
    {
        floorMode = (FloorType)_floorMode;
        if (currentFloor != null) currentFloor.SetActive(false);
        if (_floorMode != 0)
        {
            currentFloor = floors[_floorMode - 1];
            currentFloor.SetActive(true);
        }
        else currentFloor = null;
        Debug.Log("Current Floor " + (FloorType)_floorMode);
    }

    public MapEditorFloor CheckWall(float x, float y)
    {
        for (int i = 0; i < walls.childCount; i++)
        {
            MapEditorFloor temp = walls.GetChild(i).GetComponent<MapEditorFloor>();
            if (temp.mapPos.x == x && temp.mapPos.y == y) return temp;
        }
        return null;
    }

    public MapEditorFloor CheckPlatform(float x, float y)
    {
        for (int i = 0; i < platforms.childCount; i++)
        {
            MapEditorFloor temp = platforms.GetChild(i).GetComponent<MapEditorFloor>();
            if (temp.mapPos.x == x && temp.mapPos.y == y) return temp;
        }
        return null;
    }

    public MapEditorFloor CheckDevice(float x, float y)
    {
        for (int i = 0; i < devices.childCount; i++)
        {
            MapEditorFloor temp = devices.GetChild(i).GetComponent<MapEditorFloor>();
            if (temp.mapPos.x == x && temp.mapPos.y == y) return temp;
        }
        return null;
    }

    public MapEditorFloor CheckItem(float x, float y)
    {
        for (int i = 0; i < items.childCount; i++)
        {
            MapEditorFloor temp = items.GetChild(i).GetComponent<MapEditorFloor>();
            if (temp.mapPos.x == x && temp.mapPos.y == y) return temp;
        }
        return null;
    }

    public void deSelect()
    {
        if (currentFloor != null) currentFloor.SetActive(false);
        floorMode = 0;
    }
}

