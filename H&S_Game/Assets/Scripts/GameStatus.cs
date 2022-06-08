using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStatus
{
    private static bool isMonster=false;

    public static bool IsMonster { get => isMonster; set => isMonster = value; }
}
