using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public class PlayerManager : MonoBehaviour
    {
        public float PlayerHealth;

        [SerializeField] private Slider _sliderPlayer;

        public void TakePlayerDamage(int damage)
        {
            PlayerHealth -= damage;
            _sliderPlayer.value = PlayerHealth;

            if (PlayerHealth <= 0)
            {
                Debug.Log("Player died");
            }
            Debug.Log("Player attacked");
        }
    }
}
