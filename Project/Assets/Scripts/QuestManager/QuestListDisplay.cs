using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class ButtonExtension

{
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate () {
            OnClick(param);
        });
    }

}

public class QuestListDisplay : MonoBehaviour
{
    public GameObject buttonTemplate;
    GameObject newButton;
    public QuestManager questManager;

    public GameObject infoPanel;
    public TextMeshProUGUI questTitle;

    int missionSelected;

    private void Start()
    {
        buttonSpawn();
    }
    void buttonSpawn()
    {
        buttonTemplate.SetActive(true);
        int N = questManager.quest.Length;

        for (int i = 0; i < N; i++)
        {

            newButton = Instantiate(buttonTemplate, transform);

            newButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questManager.quest[i].questTile;

            newButton.GetComponent<Button>().AddEventListener(i, ItemClicked);


        }



        buttonTemplate.SetActive(false);
    }

    void ItemClicked(int itemIndex)
    {
        missionSelected = itemIndex;
        infoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questManager.quest[itemIndex].questTile;
        infoPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = questManager.quest[itemIndex].questDescription;
       /* infoPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = 
            questManager.quest[itemIndex].goal.goalType.ToString()
            + " "
            + questManager.quest[itemIndex].goal.requiredAmount.ToString();*/
    }

    public void AcceptMissionButton()
    {
        questManager.selectedQuest= missionSelected;
    }
}
