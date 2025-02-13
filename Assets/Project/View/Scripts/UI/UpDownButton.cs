using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class UpDownButton : MonoBehaviour
{
    [SerializeField] CountButton _upButton;
    [SerializeField] CountButton _downButton;

    int _max;
    int _min;

    public void Init(int min, int max)
    {
        _max = max;
        _min = min;
    }
    
    public int GetValue()
    {
        return _upButton.Count.Value - _downButton.Count.Value;
    }
}
