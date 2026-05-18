using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Symbol
{
    [SerializeField] private string name;
    [SerializeField] private int id; //unique identifier for the symbol
    [SerializeField] private RectTransform symbol;
    [SerializeField, Range(0.0f, 1.0f)] private float chance; //chance for this symbol to be selected
    [SerializeField] private float spinTime; //If this symbol is selected, how long should the reel spin
    //NOTE: Spin Time is assumed to be same for the same symbol across the reels

    public float GetChance()
    {
        return chance;
    }
    public int GetID()
    {
        return id;
    }
    public float GetSpinTime()
    {
        return spinTime;
    }
    public RectTransform GetSymbol()
    {
        return symbol;
    }
}