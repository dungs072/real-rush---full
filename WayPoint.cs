using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    pathFinder pathfinder;
    [SerializeField] tower towerPrefab;
    [SerializeField] bool isPlaceable;
    Node node;
    Vector2Int coordinate = new Vector2Int();
    gridManager gridmanager;
    // void Start()
    // {
    //     Debug.Log(coordinate);
    // }
    public bool IsPlaceable
    {
        get{
            return isPlaceable;
        }
    } 
    void  Awake() {
        pathfinder = FindObjectOfType<pathFinder>();
        gridmanager = FindObjectOfType<gridManager>();
    }
    void Start()
    {
        if(gridmanager!=null)
        {
            coordinate = gridmanager.getCoordinatesFromPosition(transform.position);
            if(!isPlaceable)
            {
                gridmanager.BlockNode(coordinate);
            }
        }
    }
    // void Start()
    // {
    //     if(node == null)
    //     {
    //         return;
    //     }
    //     Debug.Log(1);
    //     node.isWalkable = isPlaceable;
    // }

    void OnMouseDown() {
        if(gridmanager.GetNode(coordinate).isWalkable && !pathfinder.WillBlockPath(coordinate))
           {
               
               bool isSuccessful = towerPrefab.CreateTower(towerPrefab,transform.position);
               if(isSuccessful)
               {
                    gridmanager.BlockNode(coordinate);
                    pathfinder.NotifyReceivers();
                    
               }
              
              
           }
    }
}
