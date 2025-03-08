using System;
using UnityEngine;

public class TurnManager<T> where T : Enum
{
    readonly int TurnAmount;
    int _currentIndex = 0;
    public T Current { get; private set; }

    public TurnManager(int turnAmount = 2)
    {
        TurnAmount = turnAmount;
    }

    public void Start(T startTurn)
    {
        Current = startTurn;
        _currentIndex = Convert.ToInt32(Current);
    }

    public void Switch()
    {
        Debug.Log(_currentIndex);
        _currentIndex++;
        _currentIndex %= TurnAmount;

        Debug.Log("Trying to switch.");
        if (!Enum.IsDefined(typeof(T), _currentIndex)) return;
        Debug.Log($"Current: {Current}");
        Current = (T)Enum.ToObject(typeof(T), _currentIndex);
    }
}
