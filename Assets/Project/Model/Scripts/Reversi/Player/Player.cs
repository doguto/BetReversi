using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


namespace Project.Reversi.Model
{
    public static class NPC
    {
        internal static async void SetRandomPosition(List<Vector2Int> positions)
        {
            if (positions.Count == 0) return;

            var wait = SetDeray();
            await wait;

            var randomIndex = Random.Range(0, positions.Count - 1);
            ReversiModel.SetOthello(positions[randomIndex]);
        }

        static async Task SetDeray()
        {
            await Task.Delay(1000);
        }
    }


    public class ReversiPlayer
    {
        protected const int MinOthelloAmount = 0;

        internal OthelloColor PlayerColor { get; private set; }
        internal int CurrentOthelloAmount { get; private set; } = 0;

        internal ReversiPlayer(OthelloColor color, int othelloAmount)
        {
            PlayerColor = color;
            CurrentOthelloAmount = othelloAmount;
        }

        internal void UseOthello(int usedAmount)
        {
            if (CurrentOthelloAmount - usedAmount < MinOthelloAmount)
            {
                Debug.LogError("Don't use Othello over the amount you have.");
                return;
            }

            CurrentOthelloAmount -= usedAmount;
        }
    }
}