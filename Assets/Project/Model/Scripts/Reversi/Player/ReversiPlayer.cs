using UnityEngine;

internal class ReversiPlayer
{
    readonly int _minOthelloAmount = 0;

    internal OthelloColor PlayerColor { get; private set; }
    internal int CurrentOthelloAmount { get; private set; } = 0;
    
    internal ReversiPlayer(OthelloColor color, int othelloAmount)
    {
        PlayerColor = color;
        CurrentOthelloAmount = othelloAmount;
    }

    internal void UseOthello(int usedAmount)
    {
        if (CurrentOthelloAmount - usedAmount < _minOthelloAmount)
        {
            Debug.LogError("Don't use Othello over the amount you have.");  
            return;
        }

        CurrentOthelloAmount -= usedAmount;
    }
}
