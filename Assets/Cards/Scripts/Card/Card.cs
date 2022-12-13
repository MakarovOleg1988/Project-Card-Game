using TMPro;
using UnityEngine;

namespace Cards
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private GameObject _frontCard;
        [SerializeField] MeshRenderer _picture;
        [SerializeField] TextMeshPro _cost;
        [SerializeField] TextMeshPro _name;
        [SerializeField] TextMeshPro _discription;
        [SerializeField] TextMeshPro _attack;
        [SerializeField] TextMeshPro _type;
        [SerializeField] TextMeshPro _health;

        public void Configuration(Material picture, CardPropertiesData data, string discription)
        {
            _picture.sharedMaterial = picture;
            _cost.text = data.Cost.ToString();
            _name.text = data.Name;
            _discription.text = discription;
            _attack.text = data.Attack.ToString();
            _type.text = data.Type == CardUnitType.None ? string.Empty : data.Type.ToString();
            _health.text = data.Health.ToString();
        }
    }
}