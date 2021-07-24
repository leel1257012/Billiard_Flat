using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStructure 
{
    public float x;
    public float y;
    public FloorType type;
    public MapStructure(){}
    public MapStructure(float x, float y, FloorType type){
        this.x = x;
        this.y = y;
        this.type = type;
    }
}
