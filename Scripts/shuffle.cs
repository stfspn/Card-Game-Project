using JetBrains.Annotations;
using UnityEngine;

public class shuffle : MonoBehaviour
{
            static void shuffling()
        {

            int[] cards = new int[11];
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = i + 1;
            }

            // print array to console
            Debug.Log("Before Shuffle:" + string.Join(",", cards));

            //shuffle array
            for (int i = 0; i < cards.Length; i++)
            {
                int temp = cards[i];
                int randomIndex = Random.Range(i, cards.Length);
                cards[i] = cards[randomIndex];
                cards[randomIndex] = temp;
            }

            //print shuffled array to console
            Debug.Log("After Shuffle:" + string.Join(",", cards));
        }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
        
    // Update is called once per frame
    void Update()
    {
        
    }
}
