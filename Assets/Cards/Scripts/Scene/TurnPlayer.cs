using UnityEngine;
using UnityEngine.UI;
namespace Cards
{
    public class TurnPlayer : MonoBehaviour
    {
        private TableManager _tableManager;
        public bool TurnSide { get; private set; } = false;

        public bool isTurnedPlayer = false;

        [SerializeField] private GameObject _Player1;
        [SerializeField] private GameObject _Player2;
        private Transform _TurnLine;

        [SerializeField] Slider _Player1Health;
        [SerializeField] Slider _Player2Health;

        [SerializeField] Renderer _FramePlayer1;
        [SerializeField] Renderer _FramePlayer2;

        private void Awake()
        {
            _TurnLine = GameObject.Find("TurnLine").transform;
        }

        public void Start()
        {
            _tableManager = GetComponent<TableManager>();
            if (TurnSide == false)
            {
                _TurnLine.rotation = Quaternion.Euler(0f, 0f, 0f);

                TurnSide = !TurnSide;

                _tableManager.ClearCurrentTurnCard();
                _tableManager.NullingCard();
                isTurnedPlayer = true;

                _FramePlayer1.enabled = true;
                _FramePlayer2.enabled = false;

            }
            else if (TurnSide == true)
            {
                _TurnLine.rotation = Quaternion.Euler(0f, 0f, 180f);

                TurnSide = !TurnSide;

                _tableManager.ClearCurrentTurnCard();
                _tableManager.NullingCard();
                isTurnedPlayer = false;

                _FramePlayer1.enabled = false;
                _FramePlayer2.enabled = true;
            }
        }
    }
}
