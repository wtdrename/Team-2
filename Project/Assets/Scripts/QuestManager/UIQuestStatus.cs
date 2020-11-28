using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIQuestStatus : MonoBehaviour
{
    public PlayerQuest playerQuest;
    public GameObject StatusButtomTemplate;
    GameObject newButton;

    private void Awake()
    {
        playerQuest = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerQuest>();
    }


    public void QuestObjectiveListSpawn()
    {
        StatusButtomTemplate.SetActive(true);

        foreach (var goal in playerQuest.quest.goal)
        {
            newButton = Instantiate(StatusButtomTemplate, transform);
            newButton.name = goal.targetType.ToString();
            newButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 
                goal.targetType.ToString()+ "s to "+ goal.goalType.ToString() + " " + goal.requiredAmount+ " / " + goal.currentAmount;

            newButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;


        }
      


        StatusButtomTemplate.SetActive(false);
    }

    public void UpdateUI(string elementName, bool questCompleted)
    {
        foreach (Transform child in transform)
        {
            if (child.name == elementName)
            {
                foreach (var goal in playerQuest.quest.goal)
                {
                    if (goal.targetType.ToString() == elementName)
                    {
                        
                        child.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                            goal.targetType.ToString() + "s to " + goal.goalType.ToString() + " " + goal.requiredAmount + " / " + goal.currentAmount;

                        if(questCompleted)
                            newButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.green;
                        break;
                    }

                }
                break;
            }                
         
        }


    }
}
