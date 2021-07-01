using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class coordinate : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f,0.5f,0f);
   
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    gridManager gridmanager;
    void Awake() {
        //Debug.Log(coordinates);
        label = GetComponent<TextMeshPro>();
       
        gridmanager = FindObjectOfType<gridManager>();
        label.enabled = label.IsActive();
        DisPlayCoordinates();
    }
    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisPlayCoordinates();
            updateObjectName();
            label.enabled = true;
        }
        setLabelColor();
        toggleLabel();
    }
    void setLabelColor()
    {
        // if(wayPoint == null)
        // {
        //     return;
        // }
        // if(wayPoint.IsPlaceable)
        // {
        //     label.color = defaultColor;
        // }
        // else
        // {
        //     label.color = blockColor;
        // }

        if(gridmanager == null)
        {
            return ;
        }
        Node node = gridmanager.GetNode(coordinates);
        if(node== null)
        {
            return ;
        }
        if(!node.isWalkable)
        {
            label.color = blockColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if(node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }
    void toggleLabel()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
    void DisPlayCoordinates()
    {
        if(gridmanager == null)
        {
            return;
        }
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x/UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z/UnityEditor.EditorSnapSettings.move.z);
        label.text = coordinates.x + "," + coordinates.y;
    }
    void updateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
