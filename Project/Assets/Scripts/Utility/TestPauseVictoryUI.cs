using TMPro;
using UI;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace Utility
{
    public class TestPauseVictoryUI : MonoBehaviour
    {
        private readonly LoadData _loadData = new LoadData();
        public TextMeshProUGUI tittleText;
        public TextMeshProUGUI xpText;
        public TextMeshProUGUI goldText;
        public Button button;
        public GameManager.GameState gameState;
        private int _count;
        private int _gold, _xp;
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Game();
                _loadData.Text(gameState, button,tittleText);
            }
        }

        void Game()
        {
            _gold = Random.Range(0, 500000);
            _xp = Random.Range(0, 500000);
            _loadData.Gold(_gold, goldText);
            _loadData.Xp(_xp,xpText);
            _count++;
            gameState += 1;
            if (_count == 3)
            {
                gameState = 0;
                _count = 0;
            }
        }
    }
}
