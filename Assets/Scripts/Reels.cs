using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reel
{
    [SerializeField] private int id; //identifier
    [SerializeField] private Symbol[] symbols; //All symbols in this Reel
    [SerializeField] private float spinSpeed; //How fast the Reel would spin
    private Symbol chosenSymbol; //Randomly selected symbol for this reel

    public Symbol[] GetSymbols()
    {
        return symbols;
    }

    public Symbol GetChosenSymbol()
    {
        return chosenSymbol;
    }

    public void SetSymbol(Symbol symbol)
    {
        chosenSymbol = symbol;
    }

    /// <summary>
    /// The Symbols in the reel are moved down a certain y value each frame, and are snapped back to the top when they're down enough
    /// </summary>
    /// <param name="deltaTime">Delta Time, time taken for one frame to render and update</param>
    public void SpinReel(float deltaTime)
    {
        foreach (Symbol symbol in symbols)
        {
            Vector3 symbol_T = symbol.GetSymbol().anchoredPosition;
            symbol_T.y -= spinSpeed * deltaTime;
            if (symbol_T.y < -54)
                symbol_T.y = 36;

            symbol.GetSymbol().anchoredPosition = symbol_T;
        }
    }

    /// <summary>
    /// Symbols are snapped to their correct slot based on their Y value
    /// </summary>
    public void Organize()
    {
        float[] slots = { 18f, 0f, -18f, -36f, -54f };

        System.Array.Sort(symbols, (a, b) =>
            b.GetSymbol().anchoredPosition.y.CompareTo(
            a.GetSymbol().anchoredPosition.y));

        for (int i = 0; i < symbols.Length; i++)
        {
            Vector3 pos = symbols[i].GetSymbol().anchoredPosition;
            pos.y = slots[i];
            symbols[i].GetSymbol().anchoredPosition = pos;
        }
    }
}