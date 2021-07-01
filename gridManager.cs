using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridManager : MonoBehaviour
{
    [SerializeField] int UnityGridSize = 10;
    [SerializeField] Vector2Int gridSize;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public int  unityGridSize
    {
        get{
            return UnityGridSize;
        }
    }
    void Awake()
    {
        CreateGrid();
    }
    public Node GetNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }
     
         return null;
    }
    public Dictionary<Vector2Int,Node> Grid
    {
        get
        {
            return grid;
        }
    }
    void CreateGrid()
    {
        for(int x = 0;x<gridSize.x;x++)
        {
            for(int y =0; y< gridSize.y;y++)
            {
                Vector2Int coordinates = new Vector2Int(x,y);
                grid.Add(coordinates, new Node(coordinates,true));
                //Debug.Log(grid[coordinates].coordinates + " = " + grid[coordinates].isWalkable );
            }
        }
    }
    public void BlockNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }
    public Vector2Int getCoordinatesFromPosition(Vector3 Position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(Position.x/UnityGridSize);
        coordinates.y = Mathf.RoundToInt(Position.z/UnityGridSize);
        return coordinates;
    } 
    public Vector3 getPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 Position = new Vector3();
        Position.x = coordinates.x * UnityGridSize;
        Position.z = coordinates.y * UnityGridSize;
        return Position;
    }
    public void ResetNodes()
    {
        foreach( KeyValuePair<Vector2Int,Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;

        }
    }
}
