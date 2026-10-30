using Unity.VisualScripting;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    
    public Sprite[] cardFaces;
    public int[] cardValues = new int[12];
    int currentIndex = 0;
    void Start()
    {
        GetCardValues();
    }

    // Update is called once per frame
    void GetCardValues()
    {
        int num = 0;
        for (int i = 0; i < cardFaces.Length; i++)
        {
            num = i;
            cardValues[i] = num++;
        }
    }


    public void CardShuffle()
    {
        for (int i = cardFaces.Length - 1; i > 0; --i)
        {
            int randomIndex = Random.Range(1, i + 1);
            (cardFaces[i], cardFaces[randomIndex]) = (cardFaces[randomIndex], cardFaces[i]);
            (cardValues[i], cardValues[randomIndex]) = (cardValues[randomIndex], cardValues[i]);

        }
        currentIndex = 1;
    }


    public int DealCard(CardScript cardScript)
    {
        cardScript.SetSprite(cardFaces[currentIndex]);
        cardScript.SetCardValue(cardValues[currentIndex]);
        currentIndex++;
        return cardScript.GetCardValue();
    }

    public Sprite getCardBack()
    {
        return cardFaces[0];
    }


}
