using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetLocation : MonoBehaviour
{
    [SerializeField] float range = 15f;
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] Transform weapon;
     Transform target;
    // Start is called before the first frame update
    void Start()
    {
       target = FindObjectOfType<enemyMover>().transform; 
    }

    // Update is called once per frame
    void Update()
    {
        findClosetTarget();
        aimWeapon();
    }
    void findClosetTarget()
    {
        enemy[ ] enemies = FindObjectsOfType<enemy>();
        Transform closetTarget = null;
        float maxDistance = Mathf.Infinity;
        foreach(enemy enemi in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position,enemi.transform.position);
            if(targetDistance<maxDistance)
            {
                closetTarget = enemi.transform;
                maxDistance = targetDistance;
            }
        }
        target = closetTarget;
    }
    void aimWeapon()
    {
        
        
        float targetDistance = Vector3.Distance(transform.position,target.position);
        if(targetDistance<range)
        {
            weapon.LookAt(target);
            attackEnemy(true);            
        }
        else
        {
            attackEnemy(false);
        }
    }
    void attackEnemy(bool isAttacking)
    {
        
            var emissionModule = projectileParticles.emission;
            emissionModule.enabled = isAttacking ;
        
    }
}
