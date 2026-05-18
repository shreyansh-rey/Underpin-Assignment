using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BetManager : MonoBehaviour
{
    public static BetManager Bet;

    [SerializeField] private float[] bets;
    [SerializeField] private RectTransform indicator;
    [SerializeField] private GameObject UI;
    [SerializeField] TextMeshProUGUI betUIText;

    private int highestBet = 0;
    private int currentBet = 0;

    private float bet = 0.0f;


    void Start()
    {
        if (Bet != null && Bet != this)
        {
            Destroy(gameObject);
            return;
        }

        Bet = this;

        highestBet = bets.Length;
    }

    void Update()
    {
        UI.SetActive(GameManager.Game.BetPlaceable() && !GameManager.Game.IsBetPlaced()); //UI is turned off if Bet is locked
        if (GameManager.Game.BetPlaceable() && !GameManager.Game.IsBetPlaced()) //Only run this logic if Bet is NOT locked
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
                currentBet++;
            if (Input.GetKeyDown(KeyCode.UpArrow))
                currentBet--;

            if (currentBet < 0) currentBet = highestBet - 1;
            else if (currentBet > highestBet - 1) currentBet = 0;

            if (Input.GetKeyDown(KeyCode.Return) && GameManager.Game.GetMoney() >= bets[currentBet]) //Only lock bet in if enough money is available
            {
                bet = bets[currentBet];
                GameManager.Game.BetPlaceable(false);
                GameManager.Game.IsBetPlaced(true);
            }

            UpdateIndicator();
        }
    }

    private void UpdateIndicator()
    {
        Vector3 pos = indicator.anchoredPosition;
        pos.y = currentBet * (-13f);
        indicator.anchoredPosition = pos;

        betUIText.text = "Bet: $" + bets[currentBet].ToString();
    }

    public float GetBet()
    {
        return bet;
    }

    public float GetLowestBet()
    {
        float[] temp = (float[])bets.Clone();
        Array.Sort(temp);

        return temp[0];
    }
}