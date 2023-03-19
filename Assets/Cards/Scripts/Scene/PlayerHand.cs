using System.Collections;
using UnityEngine;

namespace Cards
{
    public class PlayerHand : MonoBehaviour
    {
        public Card[] _cardInHand;

        [SerializeField] private Transform[] _positions;

        private void Start()
        {
            _cardInHand = new Card[_positions.Length];
        }

        public bool SetNewCard(Card card)
        {
            if (card == null) return true;

            var index = GetLastPosition();

            if (index == -1)
            {
                Destroy(card.gameObject);
                return false;
            }

            _cardInHand[index] = card;
            StartCoroutine(MoveInHand(card, _positions[index]));

            return true;
        }

        private int GetLastPosition()
        {
            for (int i = 0; i < _positions.Length; i++)
            {
                if (_cardInHand[i] == null) return i;
            }
            return -1;
        }
        
        private IEnumerator MoveInHand(Card card, Transform parent)
        {
            card.SwitchVisual();

            var time = 0f;
            var startPosition = card.transform.position;
            var endPosition = parent.transform.position;

            while (time < 1f)
            {
                card.transform.position = Vector3.Lerp(startPosition, endPosition, time);
                time += Time.deltaTime;
                yield return null;
            }

            card.State = CardStateType.InHand;
        }
    }
}
