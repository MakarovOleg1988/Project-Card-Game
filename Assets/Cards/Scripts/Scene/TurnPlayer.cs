using UnityEngine;

namespace Cards
{
    public class TurnPlayer : MonoBehaviour
    {
        public static bool _turnSide { get; set; }

        [SerializeField] GameObject _Player1;
        [SerializeField] GameObject _Player2;

        [SerializeField] MeshRenderer _spritePlayer1;
        [SerializeField] MeshRenderer _spritePlayer2;

        public void Start()
        {
            if (_turnSide == false)
            {
                _Player2.transform.localScale = new Vector3(150f, 0, 150f);
                _Player1.transform.localScale = new Vector3(155f, 0, 155f);
                
                Color color1 = _spritePlayer1.material.color;
                color1.a = 255f;
                Color color2 = _spritePlayer2.material.color;
                color2.a = 150f;
                _turnSide = !_turnSide;

            }
            else if (_turnSide == true)
            {
                _Player2.transform.localScale = new Vector3(155f, 0, 155f);
                _Player1.transform.localScale = new Vector3(150f, 0, 150f);

                Color color1 = _spritePlayer1.material.color;
                color1.a = 150f;
                Color color2 = _spritePlayer2.material.color;
                color2.a = 255f;

                _turnSide = !_turnSide;
            }
        }
    }
}
