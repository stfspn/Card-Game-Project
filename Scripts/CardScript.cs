using UnityEngine;
using System.Collections.Generic;

public class CardScript : MonoBehaviour
{
    //Value of card
    public int value = 0;


    public int GetCardValue()
    {
        return value;
    }

    public  void SetCardValue(int newValue)
    {
        value = newValue;
    }

    public string GetCardName()
    {
        return GetComponent<SpriteRenderer>().sprite.name;
    }

        public  void SetSprite(Sprite newSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    
    public void ResetCard()
    {
        Sprite back = GameObject.Find("Deck").GetComponent<DeckScript>().getCardBack();
        gameObject.GetComponent<SpriteRenderer>().sprite = back;
        value = 0;
    }
}
