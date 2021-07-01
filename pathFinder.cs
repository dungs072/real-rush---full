using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates{get{return startCoordinates;}}
    [SerializeField] Vector2Int destinateCoordinates;
    public Vector2Int DestinateCoordinates{get{return destinateCoordinates;}}
    Node startNode;
    Node destinationNode;
    Node currentSearchNode;
    Dictionary<Vector2Int,Node> reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();
    
    Vector2Int[] directions = {Vector2Int.right,Vector2Int.left,Vector2Int.up,Vector2Int.down};
    gridManager gridmanager;
    Dictionary<Vector2Int,Node> grid = new Dictionary<Vector2Int, Node>();
    void Awake()
    {
        gridmanager = FindObjectOfType<gridManager>();
        if(gridmanager != null)
        {
            grid = gridmanager.Grid;
            startNode = grid[startCoordinates];
            destinationNode = grid[destinateCoordinates];
            // startNode.isWalkable = true;
            // destinationNode.isWalkable = true;
        }
        
    }
    void Start()
    {
        //startNode = new Node(startCoordinates,true);
        //destinationNode = new Node(destinateCoordinates,true);
       
        //ExploreNeighbors();
        GetNewPath();
    }
     public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);
    }
    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridmanager.ResetNodes();
        breadthFirstSearch(coordinates);
        return BuildPath();
    }
    public bool WillBlockPath(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;
            if(newPath.Count<=1)//cai nay de tranh khong cho nguoi choi dat bit het duong di cua enemy
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }
    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;
            if(grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
                //Debug.Log(grid[neighborCoords]);
                //grid[neighborCoords].isExplored = true;
                //grid[currentSearchNode.coordinates].isPath = true;
            }
        }
        foreach(Node neighbor in neighbors)
        {
            if(!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates,neighbor);
                frontier.Enqueue(neighbor);
            }
        }

    }
    void breadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;
        frontier.Clear();
        reached.Clear();
        bool isRunning = true;
        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates,grid[coordinates]);
        while(frontier.Count>0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            //Debug.Log(frontier.Count);
            currentSearchNode.isExplored = true;
            //Debug.Log(currentSearchNode.coordinates +" = "+ currentSearchNode.isExplored);
            ExploreNeighbors();
             
            if(currentSearchNode.coordinates==destinateCoordinates)
            {
                isRunning = false;
            }
        }
    }
    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;
        path.Add(currentNode);
        currentNode.isPath = true;
        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;

        }
        path.Reverse();
        return path;
    }
    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath",false,SendMessageOptions.DontRequireReceiver);
    }
        
}
