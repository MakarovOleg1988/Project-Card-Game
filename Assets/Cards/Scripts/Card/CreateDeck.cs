using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cards
{
    public class CreateDeck : MonoBehaviour
    {
        public List<string> _deckPlayer1;

        public void NextLevel()
        {
            SceneManager.LoadScene("BattleBoard");
        }
    }
}
