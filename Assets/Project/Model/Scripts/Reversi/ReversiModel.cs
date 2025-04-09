using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Cysharp.Threading.Tasks;
using Project.Common.Model;

namespace Project.Reversi.Model
{
    public static class ReversiModel
    {
        public static readonly OthelloColor White = OthelloColor.white;
        public static readonly OthelloColor Black = OthelloColor.black;
        public static readonly OthelloColor None = OthelloColor.None;

        public static readonly OthelloColor FirstTurn = OthelloColor.black;
        public static readonly int Length = 8;
        public static readonly int MaxBetAmount = 10;
        public static readonly int DefaultOthelloAmount = 32;
        
        public static readonly int ReversiWinnerReward = 10;
        public static readonly int BetWinBaseAmount = 20;  // 自身の獲得した面数のこの数字に対する比率を獲得したオセロの枚数にかける。

        private static readonly TurnManager<OthelloColor> Turn;
        private static readonly BoardModel Board;
        private static ReversiPlayer _player;

        private static Subject<SetOthelloMessage> _setOthelloMessage = new Subject<SetOthelloMessage>();
        private static Subject<ChangeColorMessage> _changeColorMessage = new Subject<ChangeColorMessage>();
        private static Subject<ResultMessage> _resultMessage = new Subject<ResultMessage>();

        public static IObservable<SetOthelloMessage> SetOthelloMessage => _setOthelloMessage;
        public static IObservable<ChangeColorMessage> ChangeColorMessage => _changeColorMessage;
        public static IObservable<ResultMessage> ResultMessage => _resultMessage;

        private static List<Vector2Int> _puttableGrids = new List<Vector2Int>();
        private static bool _isStarted = false;
        private static bool _isSoloGame = false;
        private static bool _canNotSet = false;


        static ReversiModel()
        {
            Board = new BoardModel();
            Turn = new TurnManager<OthelloColor>();
        }

        public static void InitializeReversi(OthelloColor color, int othelloAmount, bool isSoloGame)
        {
            _player = new ReversiPlayer(color, othelloAmount);
            _isSoloGame = isSoloGame;

            SetOthello(new Vector2Int(3, 3), Black, 1, false);
            SetOthello(new Vector2Int(3, 4), White, 1, false);
            SetOthello(new Vector2Int(4, 3), White, 1, false);
            SetOthello(new Vector2Int(4, 4), Black, 1, false);

            Board.Initialize();
            Turn.Start(FirstTurn);
            _puttableGrids = Board.GetPuttableGrid(FirstTurn);
            _isStarted = true;

            StartTurn();
        }

        public static async void SetPlayerOthello(Vector2Int position)
        {
            if (_player.PlayerColor != Turn.Current) return;        
            if (Board.HasOthello(position)) return;
            if (_isStarted && !_puttableGrids.Contains(position)) return;

            UpDownButtonModel upDownButton = new UpDownButtonModel();
            CheckButtonModel confirmationButton = new CheckButtonModel();
            var message = new SetOthelloMessage(position, Turn.Current, confirmationButton, upDownButton);
            _setOthelloMessage.OnNext(message);
            await UniTask.WaitUntil(() => confirmationButton.isChecked);  // confirmButtonの入力をUniRxを介して受け取る。
            Board.SetOthello(position, Turn.Current, upDownButton.Value);
            upDownButton.Destroy();

            List<Vector2Int> changeOthellos = new List<Vector2Int>();
            changeOthellos = Board.GetChangeOthello(position, Turn.Current);
            if (changeOthellos.Count == 0) return;

            foreach (Vector2Int pos in changeOthellos)
            {
                ChangeOthelloColor(pos);
            }
            Turn.Switch();
            StartTurn();
        }

        public static void SetOpponentOthello(Vector2Int position, int betAmount = 1)
        {
            if (Turn.Current == _player.PlayerColor) return;
            SetOthello(position, Turn.Current, betAmount);
        }

        internal static void SetOthello(Vector2Int position, int betAmount = 1, bool byPlayer = false)
        {
            SetOthello(position, Turn.Current, betAmount, byPlayer);
        }

        internal static void SetOthello(Vector2Int position, OthelloColor color, int betAmount = 1, bool byPlayer = false)
        {
            if (Board.HasOthello(position)) return;
            if (_isStarted && !_puttableGrids.Contains(position)) return;

            var message = new SetOthelloMessage(position, color, byPlayer);
            Board.SetOthello(position, color, betAmount);
            _setOthelloMessage.OnNext(message);

            List<Vector2Int> changeOthellos = Board.GetChangeOthello(position, color);
            if (changeOthellos.Count == 0) return;

            foreach (Vector2Int pos in changeOthellos)
            {
                ChangeOthelloColor(pos);
            }
            Turn.Switch();
            StartTurn();
        }

        internal static void ChangeOthelloColor(Vector2Int position)
        {
            if (!Board.HasOthello(position)) return;

            var message = new ChangeColorMessage(position);
            Board.ChangeColor(position);
            _changeColorMessage.OnNext(message);
        }

        static void StartTurn()
        {
            _puttableGrids = Board.GetPuttableGrid(Turn.Current);
            if (_puttableGrids.Count == 0)
            {
                Debug.Log("Player '" + Turn.Current + "' can't put any othello.");
                if (_canNotSet)
                {
                    EndRevesi();
                    return;
                }

                _canNotSet = true;
                Turn.Switch();
                StartTurn();
                return;
            }
            _canNotSet = false;

            if (Turn.Current == _player.PlayerColor) return;
            if (!_isSoloGame) return;

            // wait a NPC Input.
            // Debug.Log("NPC's turn");
            NPC.SetRandomPosition(_puttableGrids);
        }

        static void ShowResultDebug(OthelloColor winner)
        {

            Debug.Log("White Othello's number is : [ " + Board.GetOthelloAmount(White) + " ]");
            Debug.Log("Black Othello's number is : [ " + Board.GetOthelloAmount(Black) + " ]");
            if (winner == White)
            {
                Debug.Log("Winner is White!");
            }
            else if (winner == Black)
            {   
                Debug.Log("Winner is Black!");
            }
            else
            {
                Debug.Log("Draw!");
            }
        }

        public static async void EndRevesi()
        {
            Debug.Log("--- --- --- --- ---");
            Debug.Log("Reversi is Over!!");
            Debug.Log("--- --- --- --- ---");

            int whiteAmount = Board.GetOthelloAmount(White);
            int blackAmount = Board.GetOthelloAmount(Black);
            OthelloColor winnerColor;
            if (whiteAmount > blackAmount) 
            {
                winnerColor = White;
            }
            else if (whiteAmount < blackAmount)
            {
                winnerColor = Black;
            } 
            else
            {
                winnerColor = None;
            }
            
            ShowResultDebug(winnerColor);

            ResultMessage result = new ResultMessage(winnerColor, whiteAmount, blackAmount);
            _resultMessage.OnNext(result);

            var wait = Interval.Deray(5000);
            await wait;
            Debug.Log("App is Over.");

            #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }



    public enum OthelloColor
    {
        None = -1,
        white = 0,
        black = 1
    }
}