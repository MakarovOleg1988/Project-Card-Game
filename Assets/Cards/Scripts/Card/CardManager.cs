using Cards.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class CardManager : MonoBehaviour
    {
        private Material _baseMat;
        private List<CardPropertiesData> _allCards;
        [SerializeField, Range(25,35)] private int _cardInDeck = 30;
        [SerializeField] private Card _cardPrefab;
        [SerializeField] CardPackConfiguration[] _packs;

        [Space, SerializeField] private Transform _deckPositionPlayer1;
        [SerializeField] private Transform _deckPositionPlayer2;

        [Space, SerializeField] public PlayerHand _handPlayer1;
        [SerializeField] public PlayerHand _handPlayer2;

        public Card[] _deckPlayer1;
        public Card[] _deckPlayer2;
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
            var deck = new Card[_cardInDeck];
            var offset = new Vector3(0f, 0f, 0f);

            for (int i = 0; i < _cardInDeck; i++)
            {
                deck[i] = Instantiate(_cardPrefab, parent);
                deck[i].transform.localPosition = offset;
                offset.y += 0.5f;

                var randomCard = _allCards[Random.Range(0, _allCards.Count)];

                var picture = new Material(_baseMat);
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
            _deckPlayer2 = CreateDeck(_deckPositionPlayer2);
            _deckPlayer1 = CreateDeck(_deckPositionPlayer1);
        }

        private void Update()
        {
            if (turnPlayer.TurnSide == true) CreateHandDeckPlayer1();
            if (turnPlayer.TurnSide == false) CreateHandDeckPlayer2();

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

        private void CreateHandDeckPlayer2()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Card index = null;
                int id = -1;
                for (int i = _deckPlayer2.Length - 1; i >= 0; i--)
                {
                    if (_deckPlayer2[i] != null)
                    {
                        index = _deckPlayer2[i];
                        id = _deckPlayer2[i]._inGameID;
                        index._inGameID = id;
                        index.IsFrontSide = true;
                        _deckPlayer2[i] = null;
                        break;
                    }
                }
                _handPlayer2.SetNewCard(index);
            }
        }
    }
}
