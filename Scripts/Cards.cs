using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class Cards : MonoBehaviour
{
    private int[] numberArray = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11};
    private List<int> randomizedList;

    public TMP_Text displayText;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   

        randomizedList = new List<int>(numberArray);

        shuffleList(randomizedList);

    }

    void shuffleList(List<int> list)
    {
        for (int i = list.Count -1;i>0;i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);

        }

    }

    public void OnButtonPress()
    {
        int selectedCard = SelectRandomCard();
        if(selectedCard != -1)
        {
            displayText.text = "Drawn card " + selectedCard;
        }
        else
        {
            displayText.text = "No more cards to draw";
        }
    }

    public int SelectRandomCard()
    {
        if(randomizedList.Count == 0)
        {
            Debug.Log("No more cards to draw");
            return -1;
        }
        
        int index = Random.Range(0, randomizedList.Count);
        int selectedCard = randomizedList[index];
        randomizedList.RemoveAt(index);

        Debug.Log("Selected card: " + selectedCard);
        return selectedCard;
    }
}