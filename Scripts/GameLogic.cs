using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class TwentyOneGame : MonoBehaviour
{
    public Deck deck;
    public TMP_Text playerText, aiText, resultText; // UI Text
    private List<int> playerHand = new List<int>();
    private List<int> aiHand = new List<int>();

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        playerHand.Clear();
        aiHand.Clear();
        resultText.text = "";

        // Each player starts with one card
        playerHand.Add(deck.DrawCard());
        aiHand.Add(deck.DrawCard());

        UpdateUI();
    }

    public void PlayerDraw()
    {
        if (playerHand.Count < 5) // Max 5 cards
        {
            int card = deck.DrawCard();
            if (card != -1)
            {
                playerHand.Add(card);
                UpdateUI();
            }
        }
        if (GetHandValue(playerHand) > 21)
        {
            resultText.text = "Player Busts! AI Wins!";
        }
    }

    public void PlayerStand()
    {
        StartCoroutine(AITurn());
    }

    
    IEnumerator AITurn()
    {
        yield return new WaitForSeconds(1f);

        // Ai hits until 17
        while (GetHandValue(aiHand) < 17) 
        {
            aiHand.Add(deck.DrawCard());
            UpdateUI();
            yield return new WaitForSeconds(1f);
        }

        DetermineWinner();
    }

    void DetermineWinner()
    {
        int playerScore = GetHandValue(playerHand);
        int aiScore = GetHandValue(aiHand);

        if (aiScore > 21)
            resultText.text = "AI Busts! Player Wins!";
        else if (playerScore > aiScore)
            resultText.text = "Player Wins!";
        else if (playerScore < aiScore)
            resultText.text = "AI Wins!";
        else
            resultText.text = "It's a Tie!";
    }

    int GetHandValue(List<int> hand)
    {
        int total = 0;
        foreach (int card in hand)
        {
            total += card;
        }
        return total;
    }

    void UpdateUI()
    {
        playerText.text = "Player: " + GetHandString(playerHand) + " (" + GetHandValue(playerHand) + ")";
        aiText.text = "AI: " + GetHandString(aiHand) + " (" + GetHandValue(aiHand) + ")";
    }

    string GetHandString(List<int> hand)
    {
        return string.Join(" ", hand);
    }
}
