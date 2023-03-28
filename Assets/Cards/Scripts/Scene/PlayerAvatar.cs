using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class PlayerAvatar : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _playerAvatar;

        [SerializeField] private Material _mageImage;
        [SerializeField] private Material _priestImage;

        private void Awake()
        {
            _playerAvatar = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            if (PlayerPrefs.GetInt("_chooseAvatar") == 1)
            {
                _playerAvatar.material = _priestImage;

            }
            else if (PlayerPrefs.GetInt("_chooseAvatar") == 2)
            {
                _playerAvatar.material = _mageImage;
            }
        }
    }
}
