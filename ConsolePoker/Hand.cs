public class Hand
{
    List<Card> cards;

    public Hand()
    {
        //draw 5 cards
        cards = new List<Card>();
        //make sure to remove from deck, don't want dupes
    }

    //void Draw(Deck d)
    //{

    //}

    /// <summary>
    /// Add a card to the hand
    /// </summary>
    /// <param name="c">The Card object to add</param>
    public void AddCard(Card c)
    {
        // add card to cards
        cards.Add(c);

    }

    /// <summary>
    /// Prints a hand
    /// </summary>
    public void Display()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        foreach (Card c in cards)
        {
            Console.WriteLine("\t{0}",c);
        }
        Console.ResetColor();
    }
    /// <summary>
    /// Gets cards
    /// </summary>
    /// <returns></returns>
    public List<Card> GetCards()
    {
        List<Card> newCards = new List<Card>();
        foreach (Card c in cards)
        {
            newCards.Add(c);
        }
        return newCards;
    }

    /// <summary>
    /// Swap two cards
    /// </summary>
    /// <param name="toRemove">This card will be removed</param>
    /// <param name="newCard">This card will replace it</param>
    public void Swap(Card toRemove, Card newCard)
    {
        cards.Remove(toRemove);
        cards.Add(newCard);
    }
}





//public class Player
//{
//    Hand hand;
//    string name;

//    public Player()
//    {
//        hand = new Hand();
//        Box b1 = new Box(4);
//        Box b2 = new Box(6);
//        Box b3 = new Box(12355627);

//        Console.WriteLine(b1.GetItem());

//    }
//}

//public class Box
//{
//    int item;

//    public Box(int itemIn)
//    {
//        item = itemIn;
//    }

//    public int GetItem()
//    {
//        return item;
//    }

//}