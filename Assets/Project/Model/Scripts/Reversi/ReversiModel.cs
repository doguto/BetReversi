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
        public const OthelloColor White = OthelloColor.white;
        public const OthelloColor Black = OthelloColor.black;
        public const OthelloColor None = OthelloColor.None;

        public const OthelloColor FirstTurn = OthelloColor.black;
        public const int Length = 8;
        public const int MaxBetAmount = 10;
        public const int DefaultOthelloAmount = 32;
        
        public const int ReversiWinnerReward = 10;
        public const int BetWinBaseAmount = 20;  // 自身の獲得した面数のこの数字に対する比率を獲得したオセロの枚数にかける。

        static readonly TurnManager<OthelloColor> Turn;
        static readonly BoardModel Board;
        static ReversiPlayer _player;

        static readonly Subject<SetOthelloMessage> setOthelloMessage = new();
        static readonly Subject<ChangeColorMessage> changeColorMessage = new();
        static readonly Subject<ResultMessage> resultMessage = new();

        public static IObservable<SetOthelloMessage> SetOthelloMessage => setOthelloMessage;
        public static IObservable<ChangeColorMessage> ChangeColorMessage => changeColorMessage;
        public static IObservable<ResultMessage> ResultMessage => resultMessage;

        static List<Vector2Int> _emptyGrids = new();
        static bool _isStarted;
        static bool _isSoloGame;
        static bool _canNotSet;


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
            _emptyGrids = Board.GetPuttableGrid(FirstTurn);
            _isStarted = true;

            StartTurn();
        }

        public static async void SetPlayerOthello(Vector2Int position)
        {
            if (_player.PlayerColor != Turn.Current) return;        
            if (Board.HasOthello(position)) return;
            if (_isStarted && !_emptyGrids.Contains(position)) return;

            var upDownButton = new UpDownButtonModel();
            var confirmationButton = new CheckButtonModel();
            var message = new SetOthelloMessage(position, Turn.Current, confirmationButton, upDownButton);
            setOthelloMessage.OnNext(message);
            await UniTask.WaitUntil(() => confirmationButton.isChecked);  // confirmButtonの入力をUniRxを介して受け取る。
            Board.SetOthello(position, Turn.Current, upDownButton.Value);
            upDownButton.Destroy();

            var changeOthellos = new List<Vector2Int>();
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
            if (_isStarted && !_emptyGrids.Contains(position)) return;

            var message = new SetOthelloMessage(position, color, byPlayer);
            Board.SetOthello(position, color, betAmount);
            setOthelloMessage.OnNext(message);

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
            changeColorMessage.OnNext(message);
        }

        static void StartTurn()
        {
            _emptyGrids = Board.GetPuttableGrid(Turn.Current);
            if (_emptyGrids.Count == 0)
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
            NPC.SetRandomPosition(_emptyGrids);
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
            resultMessage.OnNext(result);

            var wait = Interval.Delay(5000);
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