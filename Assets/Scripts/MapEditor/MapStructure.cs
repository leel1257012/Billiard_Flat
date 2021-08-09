using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlatform 
{
    public float x;
    public float y;
    public FloorType type;
    public SinglePlatform(){}
    public SinglePlatform(float x, float y, FloorType type){
        this.x = x;
        this.y = y;
        this.type = type;
    }
}
public class MapStructure
{
    public float mapWidth;
    public float mapHeight;
    public List<BallType> balls;
    public List<SinglePlatform> platforms;

    public MapStructure(){}
    public MapStructure(float mapWidth, float mapHeight, List<BallType> balls, List<SinglePlatform> platforms){
        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;
        this.balls = balls;
        this.platforms = platforms;
    }
}