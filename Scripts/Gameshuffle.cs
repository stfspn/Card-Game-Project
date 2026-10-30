using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private List<int> deck = new List<int>();

    void Start()
    {
        GenerateDeck();
        ShuffleDeck();
    }

    void GenerateDeck()
    {
        deck.Clear();
        for (int i = 1; i <= 11; i++) // Cards from 1 to 11
        {
            deck.Add(i);
        }
    }

    void ShuffleDeck()
    {
        for (int i = deck.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public int DrawCard()
    {
        if (deck.Count == 0) return -1; // No more cards
        int drawnCard = deck[0];
        deck.RemoveAt(0);
        return drawnCard;
    }
}
