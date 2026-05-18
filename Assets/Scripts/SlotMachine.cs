using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    public static SlotMachine Machine;

    [SerializeField] private Reel[] reels;
    [SerializeField] private RawImage lever;
    [SerializeField] private Texture leverIdle;
    [SerializeField] private Texture leverActive;

    void Start()
    {
        if (Machine != null && Machine != this)
        {
            Destroy(gameObject);
            return;
        }

        Machine = this;
    }

    public Result LeverPull(float leverPullTime)
    {
        Result result = new Result();
        StartCoroutine(LeverRoutine(leverPullTime));

        //for every reel, a symbol is chosen almost instantaneously and the reel starts spinning
        for (int i = 0; i < reels.Length; i++)
        {
            float randomRoll = UnityEngine.Random.Range(0.0f, 1.0f);
            float cumulativeChance = 0.0f;
            Symbol selectedSymbol = null;

            foreach (Symbol symbol in reels[i].GetSymbols())
            {
                cumulativeChance += symbol.GetChance();

                if (randomRoll <= cumulativeChance)
                {
                    selectedSymbol = symbol;
                    break;
                }
            }

            if (selectedSymbol == null && reels[i].GetSymbols().Length > 0)
            {
                Symbol[] allSymbols = reels[i].GetSymbols();
                selectedSymbol = allSymbols[allSymbols.Length - 1];
            }

            reels[i].SetSymbol(selectedSymbol);

            float spinTime = reels[i].GetChosenSymbol().GetSpinTime();
            StartCoroutine(SpinReel(reels[i], spinTime));
            result.SetTime((spinTime > result.GetTime()) ? spinTime : result.GetTime());
        }

        //jackpot is calculated, if the previous symbol, and current symbol do not match, the game is lost
        bool jackpot = true;

        Symbol firstReelSymbol = reels[0].GetChosenSymbol();

        for (int i = 1; i < reels.Length; i++)
        {
            Symbol currentSymbol = reels[i].GetChosenSymbol();

            if (currentSymbol == null || firstReelSymbol == null || currentSymbol.GetID() != firstReelSymbol.GetID())
            {
                jackpot = false;
                break;
            }
        }

        //This frame added for frame correction
        result.AddTime(Time.deltaTime);
        result.SetJackpot(jackpot);
        return result;
    }

    private IEnumerator LeverRoutine(float wait)
    {
        lever.texture = leverActive;
        yield return new WaitForSeconds(wait);
        lever.texture = leverIdle;
    }

    private IEnumerator SpinReel(Reel reel, float spinTime)
    {
        float elapsed = 0f;

        while (elapsed < spinTime)
        {
            reel.SpinReel(Time.deltaTime);

            elapsed += Time.deltaTime;

            yield return null;
        }

        reel.Organize();
    }
}