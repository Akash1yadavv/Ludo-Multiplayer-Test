using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathObjectParent : PathPoint
{

    public PathPoint[] CommonPathPoint;
    public PathPoint[] RedPathPoint;
    public PathPoint[] GreenPathPoint;
    public PathPoint[] BluePathPoint;
    public PathPoint[] YellowPathPoint;

    [Header("scale and positing difference")]
    public float[] scales;
    public float[] positionDifference;
    public PathPoint[] BasePoint;
    public List<PathPoint> savePoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
