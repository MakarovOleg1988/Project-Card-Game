using UnityEngine;
using UnityEngine.UI;
namespace Cards
{
    public class TurnPlayer : MonoBehaviour
    {
        private TableManager _tableManager;
        public bool TurnSide { get; private set; } = false;

        public bool isTurnedPlayer = false;

        [SerializeField] GameObject _Player1;
        [SerializeField] GameObject _Player2;

        [SerializeField] Slider _Player1Health;
        [SerializeField] Slider _Player2Health;

        [SerializeField] MeshRenderer _spritePlayer1;
        [SerializeField] MeshRenderer _spritePlayer2;

        public void Start()
        {
            _tableManager = GetComponent<TableManager>();
            if (TurnSide == false)
            {
                _Player2.transform.localScale = new Vector3(150f, 0, 150f);
                _Player1.transform.localScale = new Vector3(170f, 0, 170f);
                
                Color color1 = _spritePlayer1.material.color;
                color1.a = 255f;
                Color color2 = _spritePlayer2.material.color;
                color2.a = 150f;

                TurnSide = !TurnSide;
                _tableManager.ClearCurrentTurnCard();
                _tableManager.NullingCard();
                isTurnedPlayer = true;

            }
            else if (TurnSide == true)
            {
                _Player2.transform.localScale = new Vector3(170f, 0, 170f);
                _Player1.transform.localScale = new Vector3(150f, 0, 150f);

                Color color1 = _spritePlayer1.material.color;
                color1.a = 150f;
                Color color2 = _spritePlayer2.material.color;
                color2.a = 255f;

                TurnSide = !TurnSide;

                _tableManager.ClearCurrentTurnCard();
                _tableManager.NullingCard();
                isTurnedPlayer = false;
            }
        }
    }
}
