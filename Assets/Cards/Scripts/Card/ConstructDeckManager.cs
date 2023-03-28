using Cards.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class ConstructDeckManager : MonoBehaviour
    {
        private Material _baseMat;
        private List<CardPropertiesData> _allCards;
        [SerializeField, Range(25,35)] private int _cardInDeck = 30;
        [SerializeField] private Card _cardPrefab;
        [SerializeField] CardPackConfiguration[] _packs;

        [Space, SerializeField] private Transform _deckPositionPlayer1;

        [Space, SerializeField] public PlayerHand _handPlayer1;

        public Card[] _deckPlayer1;
        private int _maxID = -1;

        public TurnPlayer turnPlayer;

        private void Awake()
        {
            turnPlayer = GetComponent<TurnPlayer>();
            _baseMat = new Material(Shader.Find("TextMeshPro/Sprite"));
            _baseMat.renderQueue = 2995;

            IEnumerable<CardPropertiesData> cards = new List<CardPropertiesData>();

            foreach (var pack in _packs)
            {
                cards = pack.UnionProperties(cards);
            }

            _allCards = new List<CardPropertiesData>(cards);
        }

        private Card[] CreateDeck(Transform parent)
        {
            Card[] deck = new Card[_cardInDeck];
            Vector3 offset = new Vector3(0f, 0f, 0f);

            for (int i = 0; i < _cardInDeck; i++)
            {
                deck[i] = Instantiate(_cardPrefab, parent);
                deck[i].transform.localPosition = offset;
                offset.y += 0.5f;

                CardPropertiesData randomCard = _allCards[Random.Range(0, _allCards.Count)];

                Material picture = new Material(_baseMat);
                picture.mainTexture = randomCard.Texture;

                deck[i].Configuration(picture, randomCard, CardUtility.GetDescriptionById(randomCard.Id));
                for (int a = 0; a < _allCards.Count; a++)
                {
                    deck[i]._inGameID = _maxID;
                    _maxID++;
                }
            }
            return deck;
        }

        private void Start() 
        {
            _deckPlayer1 = CreateDeck(_deckPositionPlayer1);
        }

        private void Update()
        {
           CreateHandDeckPlayer1();
        }

        private void CreateHandDeckPlayer1()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Card index = null;
                int id = -1;
                for (int i = _deckPlayer1.Length - 1; i >= 0; i--)
                {
                    if (_deckPlayer1[i] != null)
                    {
                            index = _deckPlayer1[i];
                            id = _deckPlayer1[i]._inGameID;
                            index._inGameID = id;
                            index.IsFrontSide = true;
                            _deckPlayer1[i] = null;
                            break;
                    }
                }
                _handPlayer1.SetNewCard(index);
            }
        }
    }
}
