using Manager;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Load the data stored to the in game UI
    /// </summary>
    public class LoadData 
    {
        public void LoadPlayerXp(float playerXp, TextMeshProUGUI playerXpText) 
            => playerXpText.SetText($"Current Xp: {playerXp.ToString()}");
        public void LoadPlayerGold(float playerGold, TextMeshProUGUI playerGoldText) 
            => playerGoldText.SetText($"Current Gold: {playerGold.ToString()}");

        public void SetButtonText(GameManager.GameState gameState, Button button)
        {
            switch (gameState)
            {
                case GameManager.GameState.GameLoose:
                    button.GetComponentInChildren<TextMeshProUGUI>().SetText("Exit");
                    break;
                case GameManager.GameState.GamePause:
                    button.GetComponentInChildren<TextMeshProUGUI>().SetText("Continue");
                    break;
                case GameManager.GameState.GameWon:
                    button.GetComponentInChildren<TextMeshProUGUI>().SetText("Next Level");
                    break;
            }
        }

        public void LoadItemImages()
        {
            //Todo
        }

    }
}
