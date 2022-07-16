using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RollDice
{
    public static int Roll()
    {
        return Random.Range(0, 6) + 1;
    }
}


public enum DiceFace
{
    ONE, TWO, THREE, FOUR, FIVE, SIX, BRUH
}
