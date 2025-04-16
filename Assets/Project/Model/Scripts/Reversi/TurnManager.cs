using System;
using UnityEngine;

namespace Project.Reversi.Model
{
    public class TurnManager<T> where T : Enum
    {
        readonly int TurnAmount;
        int _currentIndex;
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
            _currentIndex++;
            _currentIndex %= TurnAmount;

            if (!Enum.IsDefined(typeof(T), _currentIndex)) return;
            Current = (T)Enum.ToObject(typeof(T), _currentIndex);
        }
    }
}