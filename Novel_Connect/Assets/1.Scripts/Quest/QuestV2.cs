using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestV2 : MonoBehaviour
{
    public List<Goal> goals = new List<Goal>();
    public string questName;
    public string description;
    public int experienceReward;
    public Item itemReward;
    public bool completed;


    public void CheckGoals()
    {
        completed = goals.All(g => g.completed);
        if (completed)
            GiveReward();
    }

    void GiveReward()
    {
 
        //º¸»ó

    }
}
