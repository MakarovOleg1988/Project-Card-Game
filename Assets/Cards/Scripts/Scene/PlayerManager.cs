using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    public float PlayerHealth;

    [SerializeField]private Slider _sliderPlayer;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TakePlayerDamage(int damage)
    {
        PlayerHealth -= damage;
        _sliderPlayer.value = PlayerHealth;

        if(PlayerHealth <= 0)
        {
            // Смерть игрока
        }
        Debug.Log("Player attacked");
    }
}
