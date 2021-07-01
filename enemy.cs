using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenalty = 25;
    bank money;
    // Start is called before the first frame update
    void Start()
    {
        money = FindObjectOfType<bank>();
    }
    public void rewardGold()
    {
        if(money==null)
        {
            return;
        }
        money.deposit(goldReward);
    }
    public void stealGold()
    {
        if(money == null)
        {
            return;
        }
        money.withDraw(goldPenalty);
    }

    
}
