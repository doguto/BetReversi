
internal class OthelloModel
{
    internal OthelloColor Color { get; private set; } = OthelloColor.None;
    internal int Amount { get; private set; } = 1;

    internal void Generate(OthelloColor color, int amount)
    {
        if (color == ReversiModel.None) return;
        Color = color;
        Amount = amount;
    }

    internal void ChangeColor() 
    {
        if (Color == ReversiModel.None) return;
        Color = (Color == ReversiModel.White)? ReversiModel.Black : ReversiModel.White;
    }
}
