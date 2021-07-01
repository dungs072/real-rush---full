using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBool : MonoBehaviour
{
    [SerializeField][Range(0,50)] int poolSize = 5;
    GameObject [] pool;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField][Range(0f,30f)] float spawnTimer = 1f;
    void  Awake() {
        populatePool();
    }
    void populatePool()
    {
        pool = new GameObject[poolSize];
        for(int i =0;i<pool.Length;i++)
        {
            pool[i] = Instantiate(enemyPrefab,transform);
            pool[i].SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy());
    }
    IEnumerator spawnEnemy()
    {
       while(true)
       {
           EnableObjectInPool();
           //Instantiate(enemyPrefab,transform);
           yield return new WaitForSeconds(spawnTimer);
       }
    }
    void EnableObjectInPool()
    {
        for(int i =0;i<pool.Length;i++)
        {
            if(pool[i].activeInHierarchy == false) {
                pool[i].SetActive(true);
                return;
            }
        }
    }
}
