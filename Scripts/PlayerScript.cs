using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //keeps track of player and dealer cards

    //Get scripts
    public CardScript cardScript;
    public DeckScript deckScript;

    //value of hand
    public int handValue = 0;

    //Distance Values
    public int playerDist = 7;
    public int oppDist = 7;
    public int betValue = 1;

    //array of cards on table
    public GameObject[] hand;
    
    //index of next card to be turned over
    public int cardIndex = 0;
    public void StartHand()
    {
        GetCard();
        GetCard();
    }

    //add hands
    public int GetCard()
    {
        //Get card
        //use deal card to asign sprite and value to card on table
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());

        //show card
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        
        //add value to hand total
        handValue += cardValue;
        Debug.Log($"Card Value: {cardValue}");
        Debug.Log($"Hand Value: {handValue}");
        cardIndex++;
        return handValue;
    }


    public void AdjustDistance(int betValue)
    {
        playerDist += betValue;
        Debug.Log($"Player Distance: {playerDist}");
        oppDist -= betValue;
        Debug.Log($"Opponent Distance: {oppDist}");
        betValue += 1;
        Debug.Log($"Bet Value: {betValue}");
    }

    public int GetPlayerDistance()
    {
        return playerDist;
    }

        public int GetOppDistance()
    {
        return oppDist;
    }
        public int GetBetValue()
    {
        return betValue;
    }

    public void ResetHand()
    {
        for (int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<CardScript>().ResetCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }
        cardIndex = 0;
        handValue = 0;
    }
}
