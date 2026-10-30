# Card-Game-Project
*To keep repository focused for placement purposes only the C# files have been uploaded rather than the entire unity build*
Turn based card game written in C# using the unity engine based on a Resident Evil "21" game

## Features
* Algorithmic Dealer (AI) - operates based on automated decision making rules. Algorithm chooses to 'draw' or 'stand' based on its hand value and its set risk limit.
* Randomised deck generation - uses structured array to ensure 12 unique cards are generated into a structured deck array. Fisher-Yates shuffle is implemented to randomly rearrange cards in O(N) time.
* Asymmetric hand counters - hand values are calculated out of the optimal number of cards (21) for both player and dealer. Dealer's hidden card value is not revealed to the player however the hand value is still calculated as ? + Visible value/21. This ensures the private value is hidden until the round ends.
* Fixed point pool scoring - Each player starts with 7 points and a bet value is initiated at 1 and increases by 1 each round. Upon round won or lost points are added or redacted respectively from the other players point value based on the size of the bet value until a player has zero or fewer points.

## File overview
* GameManager.cs - Controls the core game loop, button events, and overall win/loss conditions.
* PlayerScript.cs - Tracks individual hand data, accumulates scores, and handles life-point calculations.
* DeckScript.cs - Initialises card values and executes a Fisher-Yates shuffle that synchronises both the numeric array and the sprite array through C# tuple swapping
* CardScript.cs - Data model for individual cards. It uses clean encapsulation (Get/Set methods) to track properties safely without interfering with global game state rules.
