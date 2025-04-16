using UnityEngine;
using UniRx;
using Project.Reversi.Presenter;
using Project.Reversi.Model;
using Project.Common.Presenter;
using Project.Common.View;
using UnityEngine.Serialization;

namespace Project.Reversi.View
{
    public class ReversiView : MonoBehaviour
    {
        readonly Vector3 WhiteAngle = new Vector3(0, 180, 0);

        [SerializeField] GameObject othelloPrefab; // Up is black.
        [SerializeField] GameObject upDownButtonPrefab;
        [SerializeField] GameObject confirmButtonPrefab;
        [SerializeField] BoardView boardView;

        ReversiPresenter _presenter;
        OthelloColor _playerColor = OthelloColor.white; // temp
        bool _isSoloGame = true;


        internal void InitializeReversi(bool isSoloGame = true, bool isFirstIn = true)
        {
            _isSoloGame = isSoloGame;
            boardView.InitializeBoard();

            if (!_isSoloGame) DecidePlayerColor(isFirstIn);

            _presenter = new ReversiPresenter();
            _presenter.OthelloPresenters.ObserveAdd().Subscribe(SetOthello);
            _presenter.ButtonBuilder._buttonPresenters.ObserveAdd().Subscribe(SetButton);

            _presenter.InitializeReversi(_playerColor, ReversiModel.DefaultOthelloAmount, _isSoloGame);
        }

        void SetOthello(CollectionAddEvent<OthelloPresenter> presenter)
        {
            Vector3 position = new Vector3(presenter.Value.Position.x, presenter.Value.Position.y, 0);
            var othello = Instantiate(othelloPrefab, position, Quaternion.identity);
            if (presenter.Value.Color.Value == OthelloColor.white)
            {
                othello.transform.localEulerAngles = WhiteAngle;
            }
            othello.GetComponent<OthelloView>().Init(presenter.Value);
        }

        void SetButton(CollectionAddEvent<ButtonInfo> buttonInfo)
        {
            Vector3 buttonPos = GameScreen.GetValidPosition(buttonInfo.Value.Position, buttonInfo.Value.SparePosition);
            if (buttonPos == Vector3.zero) return;

            if (buttonInfo.Value.ButtonPresenter is CheckButtonPresenter)
            {
                GameObject confirmObj = Instantiate(confirmButtonPrefab, buttonPos, Quaternion.identity);
                CheckButton checkButton = confirmObj.GetComponentInChildren<CheckButton>();
                checkButton.Init(buttonInfo.Value.ButtonPresenter as CheckButtonPresenter);
                return;
            }
            if (buttonInfo.Value.ButtonPresenter is UpDownButtonPresenter)
            {
                GameObject upDownObj = Instantiate(upDownButtonPrefab, buttonPos, Quaternion.identity);
                UpDownButton upDownButton = upDownObj.GetComponent<UpDownButton>();
                upDownButton.Init(buttonInfo.Value.ButtonPresenter as UpDownButtonPresenter, 1, 1, ReversiModel.MaxBetAmount);
                return;
            }
        }

        void DecidePlayerColor(bool isBlack)
        {
            _playerColor = isBlack ? OthelloColor.black : OthelloColor.white;
        }


    // Test Code
        private void Start()
        {
            //InitializeReversi(OthelloColor.black, 32);
            //InitializeTest();
        }

        void InitializeTest()
        {
            OthelloColor[] colors = { OthelloColor.white, OthelloColor.black };
            int PlayerColorIndex = Random.Range(0, 2);
            _playerColor = colors[PlayerColorIndex];
            InitializeReversi();
        }
    }
}