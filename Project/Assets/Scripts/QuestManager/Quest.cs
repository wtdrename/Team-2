using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public bool isActive;

    public string questTile;
    public string questDescription;
    public int experienceReward;
    public int goldReward;

    public QuestGoal[] goal;

    public void Complete()
    {
        isActive = false;
        Debug.Log("quest Complete");
    }

   

}
