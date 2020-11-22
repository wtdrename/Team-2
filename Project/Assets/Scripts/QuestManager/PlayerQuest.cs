using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    public Quest quest;

    public QuestManager questManager;

    public UIQuestStatus uiQuestStatus;



    private void Start()
    {
        
        if (GameObject.FindGameObjectWithTag("QuestManager"))
        {
            questManager = GameObject.FindGameObjectWithTag("QuestManager").GetComponent<QuestManager>();
            quest = questManager.GivePlayerQuest();
        }

        uiQuestStatus.QuestObjectiveListSpawn();
    }
    
    public void IncrementKillQuestGoal(string targetName)
    {
        foreach (var goal  in quest.goal)
        {
            if(goal.targetType.ToString()== targetName)
            {
               

                goal.EnemyKilled();
                if(!goal.finished)
                uiQuestStatus.UpdateUI(goal.targetType.ToString(), goal.isReached());

                if (goal.isReached())
                {
                    goal.finished = true;                   
                }
                    


                
            }
        }
    }


}
