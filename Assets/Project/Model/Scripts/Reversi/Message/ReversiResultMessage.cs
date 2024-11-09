using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversiResultMessage
{
    public OthelloColor WinnerColor { get; private set; }
    public int TransferAmount { get; private set; }

    internal ReversiResultMessage(OthelloColor winnerColor, int transferAmount)
    {
        this.WinnerColor = winnerColor;
        this.TransferAmount = transferAmount;
    }
}
