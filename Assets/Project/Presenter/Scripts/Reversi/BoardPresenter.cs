using System;
using UnityEngine;
using UniRx;

public class BoardPresenter
{
    internal Subject<Vector2Int> mouseInput;
    public IObserver<Vector2Int> MouseInput => mouseInput;


    public BoardPresenter()
    {
        mouseInput = new Subject<Vector2Int>();
        mouseInput.Subscribe((pos) =>
        {
            //call model setOthello() ...etc 
            ReversiModel.SetPlayerOthello(pos);
        });
    }
}
