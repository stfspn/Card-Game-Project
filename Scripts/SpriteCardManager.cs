using UnityEngine;
using System.Collections.Generic;

public class SpriteCardManager : MonoBehaviour
{
    // Assign sprite renderers in Inspector
    public SpriteRenderer[] spriteRenderers;

    // Assign sprites corresponding to numbers 1-11
    public Sprite[] numberSprites;

    // Stores shuffled numbers
    private List<int> numberPool = new List<int>(); 

    void Start()
    {
        GenerateShuffledNumbers();
        AssignSprites();
    }

    void GenerateShuffledNumbers()
    {
        numberPool.Clear();

        // Generate numbers 1 to 11 twice
        for (int i = 1; i <= 11; i++)
        {
            numberPool.Add(i);
            numberPool.Add(i);
        }

        // Shuffle the list randomly
        ShuffleList(numberPool);
    }

    void AssignSprites()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            if (i < numberPool.Count)
            {
                // Get a shuffled number
                int number = numberPool[i]; 
                spriteRenderers[i].sprite = numberSprites[number - 1]; // Assign corresponding sprite
            }
        }
    }

    public void SelectAndRemoveNumber(int selectedNumber)
    {
        if (numberPool.Contains(selectedNumber))
        {
            // Remove the number
            numberPool.Remove(selectedNumber); 
            Debug.Log($"Number {selectedNumber} removed from the pool.");
        }
        else
        {
            Debug.Log($"Number {selectedNumber} is already removed or not found.");
        }
    }

    void ShuffleList(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
