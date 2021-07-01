using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(enemy))]
public class enemyHealth : MonoBehaviour
{
    enemy enemi;
    [Tooltip("adds amount to maxHitPoints When enemy dies")]
    [SerializeField] int difficultyRamp = 10;
    [SerializeField] int maxHitPoints= 5;
    int currentHitPoints = 0;
    void OnEnable() {
        currentHitPoints = maxHitPoints;    
    }
    void Start()
    {
        enemi = GetComponent<enemy>();
    }
    void OnParticleCollision(GameObject other) {
        processHit();
    }
    void processHit()
    {
        currentHitPoints--;
        if(currentHitPoints==0)
        {
            enemi.rewardGold();
            gameObject.SetActive(false);
            maxHitPoints += difficultyRamp;
            
            
        }
    }
}
