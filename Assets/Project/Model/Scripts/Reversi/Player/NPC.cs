using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

public static class NPC
{
    internal static async void SetRandomPosition(List<Vector2Int> positions)
    {
        if (positions.Count == 0) return;

        var wait = SetDeray();
        await wait;

        int randomIndex = Random.Range(0, positions.Count - 1);
        ReversiModel.SetOthello(positions[randomIndex]);
    }

    static async Task SetDeray()
    {
        await Task.Delay(1000);
    }
}
