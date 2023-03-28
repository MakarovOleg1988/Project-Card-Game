using UnityEngine;

namespace Cards
{
    public class ChousePharacter : MonoBehaviour
    {
        private int _chooseAvatar;

        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _cardPanel;

        public void ChoosePriest()
        {
            _mainPanel.SetActive(false);
            _cardPanel.SetActive(true);
            PlayerPrefs.SetInt("_chooseAvatar", 1);
            PlayerPrefs.Save();
        }

        public void ChooseMage()
        {
            _mainPanel.SetActive(false);
            _cardPanel.SetActive(true);
            PlayerPrefs.SetInt("_chooseAvatar", 2);
            PlayerPrefs.Save();
        }

        public void BackToMainMenu()
        {
            _mainPanel.SetActive(true);
            _cardPanel.SetActive(false);
        }
    }
}
