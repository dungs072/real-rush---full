using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] int delayBuild = 1;
    
     void  Start() {
      StartCoroutine(Build());
     }
     IEnumerator Build()
     {
        foreach(Transform child in  transform)
            {
               child.gameObject.SetActive(false);
               foreach(Transform grandChildren in child)
               {
                   grandChildren.gameObject.SetActive(false);
               }
            }
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);//hoc thuoc transform.gameObject.SetActive(true);
            yield return new WaitForSeconds(delayBuild);
            foreach(Transform grandChildren in child)
            {
                grandChildren.gameObject.SetActive(true);
            }
        }
        
     }
    public bool CreateTower(tower towerr,Vector3 position)
    {
        
        bank money = FindObjectOfType<bank>();
        if(money==null)
        {
            return false;
        }
        if(money.CurrentBalance>=cost)
        {
            
            money.withDraw(cost);
           Instantiate(towerr,position,Quaternion.identity);
                      
            return true;
        }
        return false;
    }
}
