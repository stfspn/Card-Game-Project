using System;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    //Game Buttons
    public Button dealButton;
    public Button hitButton;
    public Button standButton;
    public Button MainMenuButton;
    private int standClicks = 0;



    //access plyer and dealer hands
    public PlayerScript playerScript;
    public PlayerScript dealerScript;

    public Text PlayerCountText;
    public Text DealerCountText;
    public Text PlayerDistText;
    public Text OppDistText;
    public Text BetValueText;
    public Text MainText;

    public GameObject hideCard;
    public int BetValue = 1;
    public int PlayerDist = 7;
    public int OppDist = 7;
    bool DealerFinished = false;
    bool roundOver = false;
    void Start()
    {
        //Add Listeners to buttons
        dealButton.onClick.AddListener(Deal);
        hitButton.onClick.AddListener(Hit);
        standButton.onClick.AddListener(Stand);
        MainMenuButton.onClick.AddListener(MainMenuFunction);
        
    }
    
    private void MainMenuFunction()
    {
        SceneManager.LoadScene(0);
    }
    private void Deal()
    {
        playerScript.ResetHand();
        dealerScript.ResetHand();

        MainText.gameObject.SetActive(false);

        GameObject.Find("Deck").GetComponent<DeckScript>().CardShuffle();
        playerScript.StartHand();   
        dealerScript.StartHand();

        DealerCountText.text = $"Hand Value: ? + {GameObject.Find("2 Card").GetComponent<CardScript>().value.ToString()} / 21";
        PlayerCountText.text = "Hand Value: " + playerScript.handValue.ToString() + " / 21";

        hideCard.GetComponent<Renderer>().enabled = true;

        dealButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(true);
        DealerCountText.gameObject.SetActive(true);
        PlayerCountText.gameObject.SetActive(true);

    }

    public void UpdateValues()
    {
        BetValueText.text = BetValue.ToString();
        PlayerDistText.text = PlayerDist.ToString();
        OppDistText.text = OppDist.ToString();
    }
    private void Hit()
    {   
        //Check there is room on the table
        if(playerScript.GetCard() <= 6)
        {
            playerScript.GetCard();
            PlayerCountText.text = "Hand Value: " + playerScript.handValue.ToString() + " / 21";
        }   
        PlayerCountText.text = "Hand Value: " + playerScript.handValue.ToString() + " / 21";
        HitDealer();
    }

    private void Stand()
    {
        standClicks++;
        if (standClicks > 0)
        {
            Debug.Log("Stand Clicked");
            HitDealer();
        }
        else
        {
            Debug.Log("End Function");
        }
    }

    private void HitDealer()
    {   
        Debug.Log("Hit Dealer Running");
        if (dealerScript.handValue < 17 && dealerScript.cardIndex < 6)
        {   
            Debug.Log("Dealer Hit");
            dealerScript.GetCard();
            Debug.Log($"Dealer hand value: {dealerScript.handValue.ToString()}");
            DealerCountText.text = $"Hand Value: ? + {(dealerScript.handValue - GameObject.Find("1Card").GetComponent<CardScript>().GetCardValue()).ToString()} / 21";
        }
        else
        {
            DealerFinished = true;
        }
        if (DealerFinished == true && standClicks > 0)
        {
            Debug.Log("Dealer Finished");
            RoundOver();
        }
    }

    void RoundOver()
    {   
        Debug.Log("Round Over Function Running");
        while (DealerFinished == true)
        {   
            Debug.Log("Dealer Finished is true");
            bool playerbust = playerScript.handValue > 21;
            bool dealerbust = dealerScript.handValue > 21;
            if (playerbust && dealerbust)
            {
                //dealer is more bust than player - player wins
                if (playerScript.handValue < dealerScript.handValue)
                {
                    Debug.Log("Player Wins Round");
                    MainText.text = "Player Wins Round";
                    PlayerDist += BetValue;
                    OppDist -= BetValue;
                    BetValue++;
                    UpdateValues();

                    roundOver = true;
                }
                //player more bust than dealer - dealer wins
                else if (playerScript.handValue > dealerScript.handValue)
                {
                    Debug.Log("Dealer Wins Round");
                    MainText.text = "Dealer Wins Round";
                    PlayerDist -= BetValue;
                    OppDist += BetValue;
                    BetValue++;
                    UpdateValues();

                    roundOver = true;
                }
                // player and dealer are equally overdrawn - draw
                else
                {
                    Debug.Log("Round is a Draw");
                    MainText.text = "Round is a Draw";
                    PlayerDist += 0;
                    OppDist += 0;
                    BetValue++;
                    UpdateValues();

                    roundOver = true;
                }

            }
            
            //player busts
            else if (playerbust && !dealerbust)
            {
                Debug.Log("Dealer Wins Round");
                MainText.text = "Dealer Wins Round";
                PlayerDist -= BetValue;
                OppDist += BetValue;
                BetValue++;
                UpdateValues();

                roundOver = true;
            }

            //dealer busts
            else if (dealerbust && !playerbust)
            {
                Debug.Log("Player Wins Round");
                MainText.text = "Player Wins Round";
                PlayerDist += BetValue;
                OppDist -= BetValue;
                BetValue++;
                UpdateValues();

                roundOver = true;
            }

            //player hand has higher value than dealer hand and is not bust - player wins
            else if (playerScript.handValue > dealerScript.handValue && !playerbust)
            {
                Debug.Log("Player Wins Round");
                MainText.text = "Player Wins Round";
                PlayerDist += BetValue;
                OppDist -= BetValue;
                BetValue++;
                UpdateValues();

                roundOver = true;
            }

            //dealer hand has higher value than player hand and is not bust - dealer wins
            else if (playerScript.handValue < dealerScript.handValue && !dealerbust)
            {
                Debug.Log("Dealer Wins Round");
                MainText.text = "Dealer Wins Round";
                PlayerDist -= BetValue;
                OppDist += BetValue;
                BetValue++;
                UpdateValues();

                roundOver = true;
            }

            //player and dealer have equal hand values under 21 - draw
            else if (playerScript.handValue == dealerScript.handValue && !dealerbust && !playerbust)
            {
                Debug.Log("Round is a Draw");
                MainText.text = "Round is a Draw";
                PlayerDist += 0;
                OppDist += 0;
                BetValue++;
                UpdateValues();

                roundOver = true;
            }

            //failsafe
            else
            {
                Debug.Log("Round Over is False");
                roundOver = false;
            }

            DealerFinished = false;
        }

        if (roundOver == true)
        {
            Debug.Log("Round Over");
            dealButton.gameObject.SetActive(true);
            hitButton.gameObject.SetActive(false);
            standButton.gameObject.SetActive(false);
            MainText.gameObject.SetActive(true);
            DealerCountText.text = "Hand Value: " + dealerScript.handValue.ToString() + " / 21";
            hideCard.GetComponent<Renderer>().enabled = false;
            standClicks = 0;
        }
        
        if (OppDist <= 0)
        {
            Debug.Log("Player Wins Game");
            MainText.text = "Player Wins Game";
            dealButton.gameObject.SetActive(false);
            hitButton.gameObject.SetActive(false);
            standButton.gameObject.SetActive(false);
            MainMenuButton.gameObject.SetActive(true);
        }
        else if (PlayerDist <= 0)
        {
            Debug.Log("Dealer Wins Game");
            MainText.text = "Dealer Wins Game";
            dealButton.gameObject.SetActive(false);
            hitButton.gameObject.SetActive(false);
            standButton.gameObject.SetActive(false);
            MainMenuButton.gameObject.SetActive(true);
        }
        else
        {
            DealerFinished = false;
        }
    }   

}
