using System;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Cards
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] public GameObject _frontCard;
        [SerializeField] MeshRenderer _picture;
        [SerializeField] TextMeshPro _cost;
        [SerializeField] TextMeshPro _name;
        [SerializeField] TextMeshPro _discription;
        [SerializeField] TextMeshPro _attack;
        [SerializeField] TextMeshPro _type;
        [SerializeField] public TextMeshPro _health;

        [NonSerialized] public CardManager _cardManager;
        [NonSerialized] public TableManager _tableManager;

        Vector3 _offset;
        public string _distinationDeskTag1 = "Desk1";
        public string _distinationDeskTag2 = "Desk2";
        public CardStateType State { get; set; }

        public int _inGameID = -1;
        public int Health;
        public int Attack;
        public bool IsTaunt;
        public bool IsCharge;
        public bool IsFrontSide;
        public bool IsFrontSize => _frontCard.activeSelf;

        private void Start()
        {
            GameObject cm = GameObject.FindGameObjectWithTag("GameArea");
            _cardManager = cm.GetComponent<CardManager>();
            _tableManager = cm.GetComponent<TableManager>();
        }

        public void Configuration(Material picture, CardPropertiesData data, string discription)
        {
            _picture.sharedMaterial = picture;
            _cost.text = data.Cost.ToString();
            _name.text = data.Name;
            _discription.text = discription;
            _attack.text = data.Attack.ToString();
            _type.text = data.Type == CardUnitType.None ? string.Empty : data.Type.ToString();
            _health.text = data.Health.ToString();
            Health = data.Health;
            Attack = data.Attack;
            IsCharge = data.HasCharge;
            IsTaunt = data.HasTaunt;
        }

        private Vector3 MouseWorldPosition()
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            mouseScreenPosition.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_cardManager.turnPlayer.isTurnedPlayer == true && CheckPlayerIsCard() == 1 ||
                _cardManager.turnPlayer.isTurnedPlayer == false && CheckPlayerIsCard() == 2)
            {
                _offset = transform.position - MouseWorldPosition();
                transform.GetComponent<Collider>().enabled = false;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (State == CardStateType.InHand || State == CardStateType.InDesk)
            {
                if (_cardManager.turnPlayer.isTurnedPlayer == true && CheckPlayerIsCard() == 1 ||
                    _cardManager.turnPlayer.isTurnedPlayer == false && CheckPlayerIsCard() == 2)
                {
                    transform.position = MouseWorldPosition() + _offset;
                }
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.collider != null && hit.collider.GetComponent<BoxCollider>() != null)
                    {
                        if (hit.collider.gameObject == transform.gameObject.GetComponent<Collider>().gameObject)
                        {
                            if (State == CardStateType.InTable && IsFrontSide)
                            {
                                if (_tableManager.CheckCurrentTurnCard(this) == false || IsCharge == true)
                                {
                                    Debug.Log("Карта на столе");
                                    if (_cardManager.turnPlayer.isTurnedPlayer == false && CheckPlayerIsCard() == 2)
                                    {
                                        if (_tableManager.SelectCard[1] == null)
                                        {
                                            _tableManager.SelectCard[1] = this;
                                            this.GetComponent<MeshRenderer>().material.color = new Color(50, 0, 0);
                                        }
                                    }
                                    else if (_cardManager.turnPlayer.isTurnedPlayer == true && CheckPlayerIsCard() == 1)
                                    {
                                        if (_tableManager.SelectCard[0] == null)
                                        {
                                            _tableManager.SelectCard[0] = this;
                                            this.GetComponent<MeshRenderer>().material.color = new Color(50, 0, 0);
                                        }
                                    }
                                    else if (_cardManager.turnPlayer.isTurnedPlayer == false && CheckPlayerIsCard() == 1)
                                    {
                                        if (_tableManager.SelectOpponentCard[1] == null && _tableManager.SelectCard[1] != null && _tableManager.SelectCard[1] != this)
                                        {
                                            _tableManager.SelectOpponentCard[1] = this;
                                            this.GetComponent<MeshRenderer>().material.color = new Color(50, 0, 0);
                                        }
                                    }
                                    else if (_cardManager.turnPlayer.isTurnedPlayer == true && CheckPlayerIsCard() == 2)
                                    {
                                        if (_tableManager.SelectOpponentCard[0] == null && _tableManager.SelectCard[0] != null && _tableManager.SelectCard[0] != this)
                                        {
                                            _tableManager.SelectOpponentCard[0] = this;
                                            this.GetComponent<MeshRenderer>().material.color = new Color(50, 0, 0);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
                Debug.Log("OnEndDrag");
                if (!IsFrontSide)
                {
                    transform.GetComponent<Collider>().enabled = true;
                }
                else
                {
                    Debug.Log("FrontSide: " + State);
                    if (State == CardStateType.InHand || State == CardStateType.InDesk)
                    {
                        if (_cardManager.turnPlayer.isTurnedPlayer == true && CheckPlayerIsCard() == 1 ||
                            _cardManager.turnPlayer.isTurnedPlayer == false && CheckPlayerIsCard() == 2)
                        {
                            if (CheckPlayerIsCard() == 1)
                            {
                                Vector3 rayOrigin = Camera.main.transform.position;
                                Vector3 rayDirection = MouseWorldPosition() - Camera.main.transform.position;
                                RaycastHit hitinfo;
                                if (Physics.Raycast(rayOrigin, rayDirection, out hitinfo))
                                {
                                    if (hitinfo.transform.tag == "Desk1")
                                    {
                                        transform.position = hitinfo.transform.position + new Vector3(0F,10F,0F);
                                        State = CardStateType.InTable;
                                        _tableManager.SetCurrentTurnCard(this);
                                    }
                                }
                            }
                        else if (CheckPlayerIsCard() == 2)
                        {
                                Vector3 rayOrigin = Camera.main.transform.position;
                                Vector3 rayDirection = MouseWorldPosition() - Camera.main.transform.position;
                                RaycastHit hitinfo;
                                if (Physics.Raycast(rayOrigin, rayDirection, out hitinfo))
                                {
                                    if (hitinfo.transform.tag == "Desk2")
                                    {
                                        transform.position = hitinfo.transform.position + new Vector3(0F,10F,0F);
                                        State = CardStateType.InTable;
                                        _tableManager.SetCurrentTurnCard(this);
                                }
                                }
                        }
                      }
                    }
                    
                    transform.GetComponent<Collider>().enabled = true;
                }
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
        
        public void TakeDamage(int damage)
        {
            Health -= damage;
            _health.text = Health.ToString();
            if (Health <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Карта погибает");
            }
        }
        public int CheckPlayerIsCard()
        {
            int player = 0;
            if (State == CardStateType.InDesk)
            {
                if (_cardManager.turnPlayer.isTurnedPlayer == true)
                {
                    foreach (Card c in _cardManager._deckPlayer1)
                    {
                        if (c != null)
                        if (c._inGameID == this._inGameID)
                        {
                                player  = 1;
                                break;
                        }
                    }
                }
                if (_cardManager.turnPlayer.isTurnedPlayer == false)
                {
                    foreach (Card a in _cardManager._deckPlayer2)
                    {
                        if(a != null)
                        if (a._inGameID == this._inGameID)
                        {
                                player = 2;
                                break;
                        }
                    }
                }
            }
            else if(State == CardStateType.InHand || State == CardStateType.InTable)
            {
                foreach (Card c in _cardManager._handPlayer1._cardInHand)
                {
                    if (c != null)
                    if (c._inGameID == this._inGameID)
                    {
                            player = 1;
                            break;
                    }
                }
                if(player != 1)
                foreach (Card a in _cardManager._handPlayer2._cardInHand)
                {
                    if (a != null)
                    if (a._inGameID == this._inGameID)
                    {
                            player = 2;
                            break;
                    }
                }
            }
            return player;
        }
    }
}