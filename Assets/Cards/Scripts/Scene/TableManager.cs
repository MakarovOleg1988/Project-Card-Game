using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class TableManager : MonoBehaviour
    {
        public TurnPlayer _turnPlayer;
        public PlayerManager _player1;
        public PlayerManager _player2;
        public CardManager _cardManager;

        public List<Card> CurrentTurnCards1 = new List<Card>();
        public List<Card> CurrentTurnCards2 = new List<Card>();

        private bool _selectOpponent1 = false;
        private bool _selectOpponent2 = false;

        public Card[] SelectCard = new Card[2];
        public Card[] SelectOpponentCard = new Card[2];

        private bool isPlayerAttacking1 = false;
        private bool isPlayerAttacking2 = false;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            { 
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.collider != null && hit.collider.GetComponent<BoxCollider>() != null)
                    {
                        if (hit.collider.gameObject.name == "Player1" && _turnPlayer.isTurnedPlayer == false && SelectCard[1] != null && SelectOpponentCard[0] == null)
                        {
                            Debug.Log("Цель: игрок 1");
                            _selectOpponent1 = true;
                            isPlayerAttacking2 = true;
                        }
                        if (hit.collider.gameObject.name == "Player2" && _turnPlayer.isTurnedPlayer == true && SelectCard[0] != null && SelectOpponentCard[1] == null)
                        {
                            Debug.Log("Цель: игрок 2");
                            _selectOpponent2 = true;
                            isPlayerAttacking1 = true;
                        }
                    }
                }
            }
            if(SelectCard[1] != null && SelectOpponentCard[1] != null)
            {
                isPlayerAttacking2 = true;
            }
            if (SelectCard[0] != null && SelectOpponentCard[0] != null)
            {
                isPlayerAttacking1 = true;
            }
            if (_turnPlayer.isTurnedPlayer == false && isPlayerAttacking2 == true)
            {
                if (SelectCard[1] != null && SelectOpponentCard[1] != null)
                {
                    Debug.Log("Debug2");
                    CardAttack(SelectCard[1], SelectOpponentCard[1]);
                    isPlayerAttacking2 = false;
                }
                else if(SelectCard[1] != null && _selectOpponent1 == true)
                {
                    if (CheckTaunt(_player1) != null)
                    {
                        // Если у противника есть карта с таунтом, то атакуем ее
                        CheckTaunt(_player1).TakeDamage(SelectCard[1].Attack);
                    }
                    else
                    {
                        PlayerAttack(SelectCard[1], _player1);
                    }
                    isPlayerAttacking2 = false;
                }
            }
            else if(_turnPlayer.isTurnedPlayer == true && isPlayerAttacking1 == true)
            {
                Debug.Log("Debug1");
                if (SelectCard[0] != null && SelectOpponentCard[0] != null)
                {
                    Debug.Log("Debug2");
                    CardAttack(SelectCard[0], SelectOpponentCard[0]);
                }
                else if(SelectCard[0] != null && _selectOpponent2 == true)
                {
                    if (CheckTaunt(_player1) != null)
                    {
                        // Если у противника есть карта с таунтом, то атакуем ее
                        CheckTaunt(_player1).TakeDamage(SelectCard[0].Attack);
                    }
                    else
                    {
                        PlayerAttack(SelectCard[0], _player2);
                    }
                }
                isPlayerAttacking1 = false;
            }
        }

        public void CardAttack(Card attackingCard, Card opponentCard)
        {
            Debug.Log("Атака на карту");
            opponentCard.TakeDamage(attackingCard.Attack);
            NullingCard();
        }

        public void PlayerAttack(Card attackingCard, PlayerManager player)
        {
            Debug.Log("Атака на игрока");
            player.TakePlayerDamage(attackingCard.Attack);
            NullingCard();
        }

        public void SetCurrentTurnCard(Card card)
        {
            if(_turnPlayer.isTurnedPlayer == false)
            {
                CurrentTurnCards2.Add(card);
            }
            else
            {
                CurrentTurnCards1.Add(card);
            }
        }

        public bool CheckCurrentTurnCard(Card card)
        {
            if (_turnPlayer.isTurnedPlayer == false)
            {
                foreach (Card c in CurrentTurnCards2)
                {
                    if (c != null)
                        if (c == card && c.State == CardStateType.InTable)
                        {
                            return true;
                        }
                }
            }
            else
            {
                foreach (Card c in CurrentTurnCards1)
                {
                    if (c != null)
                        if (c == card && c.State == CardStateType.InTable)
                        {
                            return true;
                        }
                }
            }
            return false;
        }

        public void ClearCurrentTurnCard()
        {
            if (_turnPlayer.isTurnedPlayer == false)
            {
                CurrentTurnCards2.Clear();
            }
            else
            {
                CurrentTurnCards1.Clear();
            }
        }

        public void NullingCard()
        {
            if(_turnPlayer.isTurnedPlayer == false)
            {
                if (SelectCard[1] != null)
                {
                    SelectCard[1].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
                    SelectCard[1] = null;
                }
                if (SelectOpponentCard[1] != null)
                {
                    SelectOpponentCard[1].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
                    SelectOpponentCard[1] = null;
                }
            }
            else
            {
                if (SelectCard[0] != null)
                {
                    SelectCard[0].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
                    SelectCard[0] = null;
                }
                if (SelectOpponentCard[0] != null)
                {
                    SelectOpponentCard[0].GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
                    SelectOpponentCard[0] = null;
                }
            }
            _selectOpponent1 = false;
            _selectOpponent2 = false;
        }

        private Card CheckTaunt(PlayerManager player)
        {
            Card TauntCard = null;
            if(player == _player2)
            {
                foreach (Card c in _cardManager._deckPlayer2)
                {
                    if (c != null)
                        if (c.IsTaunt == true && c.State == CardStateType.InTable)
                        {
                            TauntCard = c;
                            break;
                        }
                }
            }
            else if(player == _player1)
            {
                foreach (Card c in _cardManager._deckPlayer1)
                {
                    if (c != null)
                    if (c.IsTaunt == true && c.State == CardStateType.InTable)
                    {
                       TauntCard = c;
                       break;
                    }
                }
            }
            return TauntCard;
        }
    }
}
