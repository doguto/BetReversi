namespace Project.Reversi.Model
{
    public class ResultMessage
    {
        public OthelloColor WinnerColor { get; private set; }
        // public int TransferAmount { get; private set; }
        public int WhiteAmount { get; private set; }
        public int BlackAmount { get; private set; }

        internal ResultMessage(OthelloColor winnerColor, int whiteAmount, int blackAmount)
        {
            this.WinnerColor = winnerColor;
            this.WhiteAmount = whiteAmount;
            this.BlackAmount = blackAmount;
        }
    }
}