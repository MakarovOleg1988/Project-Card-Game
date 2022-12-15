using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Cards
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private GameObject _frontCard;
        [SerializeField] MeshRenderer _picture;
        [SerializeField] TextMeshPro _cost;
        [SerializeField] TextMeshPro _name;
        [SerializeField] TextMeshPro _discription;
        [SerializeField] TextMeshPro _attack;
        [SerializeField] TextMeshPro _type;
        [SerializeField] TextMeshPro _health;

        Vector3 _offset;
        public string _distinationDeskTag1 = "Desk1";
        public string _distinationDeskTag2 = "Desk2";

        public CardStateType State { get; set; }   

        public bool IsFrontSize => _frontCard.activeSelf;

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

        Vector3 MouseWorldPosition()
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            mouseScreenPosition.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _offset = transform.position - MouseWorldPosition();
            transform.GetComponent<Collider>().enabled = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = MouseWorldPosition() + _offset;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var rayOrigin = Camera.main.transform.position;
            var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
            RaycastHit hitinfo;

            if (Physics.Raycast(rayOrigin, rayDirection, out hitinfo))
            {
                if (hitinfo.transform.tag == "Desk1")
                {
                    transform.position = hitinfo.transform.position;
                }
            }
            transform.GetComponent<Collider>().enabled = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            switch (State)
            {
                case CardStateType.InDesk:
                    break;
                case CardStateType.InHand:
                    transform.localScale *= 1.4f;
                    break;
                case CardStateType.InTable:
                    break;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            switch (State)
            {
                case CardStateType.InDesk:
                    break;
                case CardStateType.InHand:
                    transform.localScale /= 1.4f;
                    break;
                case CardStateType.InTable:
                    break;
            }
        }

        [ContextMenu("SwitchVisual")]
        public void SwitchVisual() => _frontCard.SetActive(!IsFrontSize);
    }
}