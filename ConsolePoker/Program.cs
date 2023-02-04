// [OK] make a deck of cards
// [OK] shuffle mechanism
//
// Player AI
// [OK] Player input
// [OK] Hand display
// [OK] Hand scoring
// [ok] MONEY 
//
// [OK] deal out hands
// [OK] let player pick what to swap
// [OK] swap selected cards
// [OK] compare hands


// [OK] Welcome prompt
    


string keepPlaying = "yes";
List<string> yesOptions = new List<string> { "yes", "y", "okay", "sure", "do it", "mmk", "Yes" };




// ----------------

Console.WriteLine("Hi there, welcome to Console Poker! Hit enter when you're ready to play!");
Console.ReadLine();


Console.WriteLine("Great, let's get started! How many chips did you bring today?");
int money = 0;
int startingMoney = 0;
while (!int.TryParse(Console.ReadLine(), out money))
{
    Console.WriteLine("I'm sorry, I don't understand what you mean.");
}

startingMoney = money;

while (yesOptions.Contains(keepPlaying.ToLower()))
{

    Console.WriteLine("Of your " + money + " chips, how many would you like to bet on this hand, between one and five? If you win, you'll get double what you wagered!");
    int wager = 0;
    while (!int.TryParse(Console.ReadLine(), out wager))
    {
        Console.WriteLine("I'm sorry, I don't understand what you mean.");
    }

    while (wager > 5)
    {
        Console.WriteLine("Sorry, that's not a valid wager. It needs to be between 1 and 5.");
        wager = Convert.ToInt32(Console.ReadLine());
    }


    Console.WriteLine("Alright, let's play!");


    // Create deck and deal hands
    Deck deck = Deck.CreateFull();

    Hand playerHand = new Hand();
    Hand aiHand = new Hand();

    for (int i = 0; i < 5; i++)
    {
        playerHand.AddCard(deck.DrawCard());
    }
    for (int i = 0; i < 5; i++)
    {
        aiHand.AddCard(deck.DrawCard());
    }
    // add ai cards

    Console.WriteLine("Here are your cards.");
    //display cards here
    playerHand.Display();
    Console.WriteLine("Would you like to swap out any cards?");

    string input = Console.ReadLine();
    bool swap = yesOptions.Contains(input.ToLower());

    // if yes, go to swap protocol
    // if no, skip and go to comparison
    if (swap)
    {
        Console.WriteLine("Which cards would you like to swap? Select multiple by typing 1 to 5, with your top card being 1.");
        //player selects card here
        //whatever they selected is toRemove
        //new card is newCard

        input = Console.ReadLine();
        foreach (char c in input)
        {
            int swapSelection = c - '0';
            Console.WriteLine("Swapping card {0}", swapSelection);
            if (swapSelection > 5 || swapSelection < 1)
            {
                Console.WriteLine("{0} isn't a valid card.", swapSelection);
                continue;
            }


            List<Card> currentHand = playerHand.GetCards();
            Card cardToReplace = currentHand[swapSelection - 1];

            playerHand.Swap(cardToReplace, deck.DrawCard());
        }
        Console.WriteLine("Here is your new hand. Good luck!");
        playerHand.Display();
    }
    Console.WriteLine("Here's what your opponent had:");
    aiHand.Display();
    Console.ReadLine();

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Let's see who wins this match!");
    Console.ResetColor();

    //show what each person has
    //human has a 2 pair while ai has a full house, for instance
    int humanScore, aiScore;
    ECardValue humanHighestValue, aiHighestValue;
    EvaluateHand(playerHand, out humanScore, out humanHighestValue);
    EvaluateHand(aiHand, out aiScore, out aiHighestValue);

    Console.WriteLine("Human: {0} {1} {2}", (EHandType)humanScore, humanScore, humanHighestValue);
    Console.WriteLine("AI: {0} {1} {2}", (EHandType)aiScore, aiScore, aiHighestValue);

    //then declare the winner
    if (humanScore > aiScore) // human win
    {
        //change money
        money += wager * humanScore;
        Console.WriteLine("Congrats, you won! Since you wagered {0} chips, you earned another {0}, and now you have a total of {1} chips.", wager * humanScore, money);

    }
    else if (humanScore < aiScore) // ai win
    {
        //change money
        money -= wager;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Oof, you just lost your wager of {0} chips. You now have {1} chips remaining.", wager, money);
    else // tie
    {
        if (humanHighestValue > aiHighestValue)
        {
            money += wager * humanScore;
            Console.WriteLine("Congrats, you won! Since you wagered {0} chips, you earned another {0}, and now you have a total of {1} chips.", wager * humanScore, money);
        }
        else if (aiHighestValue > humanHighestValue)
        {
            money -= wager;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Oof, you just lost your wager of {0} chips. You now have {1} chips remaining.", wager, money);
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("It's a perfect tie! Incredible! No chips move today!");
        }
    }

    // If we have no chips, get kicked out
    if (money <= 0)
    {
        Console.WriteLine("Hey buddy, I hate to do this but if you can't pay, you can't play...");
        keepPlaying = "I'll be good";
        continue;
        Console.ResetColor();
    }
    }

    // Otherwise ask if the player wants to play again
    Console.WriteLine("Do you want to play again?");
    keepPlaying = Console.ReadLine();
    //if yes, go back to wager
}

//if no, say goodbye and close program
Console.WriteLine("Goodbye, thanks for the {0} coins!", startingMoney - money);


void EvaluateHand(Hand h, out int multiplier, out ECardValue valueOut)
{
    multiplier = 0;
    List<Card> cards = h.GetCards();
    List<List<Card>> cardSets = new List<List<Card>>();

    // Build enums of cards for each suit
    foreach (ECardValue value in Enum.GetValues(typeof(ECardValue)))
    {
        cardSets.Add(cards.Where(card => card.Value == value).ToList());
    }

    cardSets = cardSets.OrderByDescending(l => l.Count).ThenByDescending(l => l.Count > 0 ? l[0].Value : 0).ToList();

    int largestNum = cardSets[0].Count();
    int secondLargest = cardSets.Count > 1 ? cardSets[1].Count() : 0;
    EHandType handType;

    switch (largestNum)
    {
        case 5:
            handType = EHandType.FiveOfKind;
            break;
        case 4:
            handType = EHandType.FourOfKind;
            break;
        case 3:
            if (secondLargest == 2)
                handType = EHandType.FullHouse;
            else
                handType = EHandType.ThreeOfKind;
            break;
        case 2:
            if (secondLargest == 2)
                handType = EHandType.TwoPair;
            else
                handType = EHandType.OnePair;
            break;
        default:
            handType = EHandType.None;
            break;
    }

    multiplier = (int)handType;
    valueOut = cardSets[0][0].Value;
}

void AIStuff(Hand h)
{

}

enum EHandType
{
    None = 1,
    FiveOfKind = 16,
    FourOfKind = 8,
    FullHouse = 6,
    ThreeOfKind = 4,
    TwoPair = 3,
    OnePair = 2,
}