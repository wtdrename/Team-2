using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal  
{
    public GoalType goalType;
    public TargetType targetType;

    public int requiredAmount;
    public int currentAmount;
    public bool finished=false;

    public bool isReached()
    {
        return (currentAmount >= requiredAmount);
    }



    public void EnemyKilled()
    {
        if (goalType == GoalType.Kill)
            currentAmount++;
    }

    public void ItemDestroyed()
    {
        if (goalType == GoalType.Destroy)
            currentAmount++;
    }

    public void ItemActivated()
    {
        if (goalType == GoalType.Activate)
            currentAmount++;
    }
}

public enum GoalType
{
    Kill,
    Destroy,
    Activate
}

public enum TargetType 
{ 
    Zombie
}
