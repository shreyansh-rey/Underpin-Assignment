using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Game;

    private Result jackpot;
    private float money=1000;


    private bool betPlaced = false;
    private bool canPlaceBet = true;
    [SerializeField] private float leverPullTime; //How long will the lever pull animation last, 0.3f seems fine
    [SerializeField] private TextMeshProUGUI moneyUIText;


    void Start()
    {
        if (Game != null && Game != this)
        {
            Destroy(gameObject);
            return;
        }

        Game = this;
    }

    public bool BetPlaceable() { return canPlaceBet; }

    public bool IsBetPlaced() { return betPlaced; }

    public void IsBetPlaced(bool betPlaced) { this.betPlaced = betPlaced; }
    public void BetPlaceable(bool canPlaceBet) { this.canPlaceBet = canPlaceBet; }
    public float GetMoney() { return money; }


    public void LeverPull()
    {
        if (betPlaced)
        {
            jackpot = SlotMachine.Machine.LeverPull(leverPullTime);
            betPlaced = false; //prevent lever spam
            StartCoroutine(HandleResult());
        }
    }

    private IEnumerator HandleResult()
    {
        yield return new WaitForSeconds(jackpot.GetTime()); //The results are instantaneous, so artifically waiting for the reel spin to end

        if (jackpot.GetJackpot())
            money += BetManager.Bet.GetBet();
        else
            money -= BetManager.Bet.GetBet();

        betPlaced = false;
        canPlaceBet = (money >= BetManager.Bet.GetLowestBet()); //don't allow placing bets if money is below the lowest bet amount.

        moneyUIText.text = "Bank: $" + money.ToString();
    }
}

public class Result
{
    private bool jackpot=false;
    private float time=0.0f; //Time for the reel spin animation to end

    public bool GetJackpot() { return jackpot; }
    public float GetTime() { return time; }

    public void SetJackpot(bool jackpot) { this.jackpot = jackpot; }
    public void SetTime(float time) {  this.time = time; }
    public void AddTime(float time) { this.time += time; } 
}