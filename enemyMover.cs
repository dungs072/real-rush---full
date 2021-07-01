using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMover : MonoBehaviour
{
    
    enemy enemi;
    [SerializeField][Range(0f,5f)] float speed = 1f;
    List<Node> path = new List<Node>();
    gridManager gridmanager;
    pathFinder pathfinder;

    void Awake()
    {
        enemi = GetComponent<enemy>();
        gridmanager = FindObjectOfType<gridManager>();
        pathfinder = FindObjectOfType<pathFinder>();
    }
    void OnEnable()
    {
        returnToStart();
        RecalculatePath(true);
        
    }
    IEnumerator FollowPath()
    {
        for(int i =1;i<path.Count;++i)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridmanager.getPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;
            transform.LookAt(endPosition);
            while(travelPercent<1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition,endPosition,travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        finishPath();
    }
    void finishPath()
    {
        enemi.stealGold();
        gameObject.SetActive(false);
    }
    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        if(resetPath)
        {
           coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridmanager.getCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }
    // Update is called once per frame
    void returnToStart()
    {
        transform.position = gridmanager.getPositionFromCoordinates(pathfinder.StartCoordinates);
    }
    // void printWayPointName()
    // {
    //     foreach(WayPoint wayPoint in path)
    //     {
    //         Debug.Log(wayPoint.name);
    //     }
    // }
}
