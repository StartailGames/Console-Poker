/// <summary>
/// A Deck of playing cards
/// </summary>
public class Deck
{
    public List<Card> Cards { get; private set; }
    public int Size { get { return Cards.Count; } }

    private Deck()
    {
        Cards = new List<Card>();
    }

    /// <summary>
    /// Create a new, shuffled deck of 52 unique cards.
    /// </summary>
    /// <returns>The new deck</returns>
    public static Deck CreateFull()
    {
        Deck d = new Deck();
        
        // All number / suit pairs
        foreach (ECardSuit suit in Enum.GetValues(typeof(ECardSuit)))
        {
            foreach (ECardValue value in Enum.GetValues(typeof(ECardValue)))
            {
                Card c = new Card(suit, value);
                d.Cards.Add(c);
            }
        }

        d.Shuffle();

        return d;
    }


    /// <summary>
    /// Shuffle deck using Fisher-Yates
    /// </summary>
    private void Shuffle()
    {
        Random r = new Random();
        for (int i = 0; i < Cards.Count - 1; i++)
        {
            int j = r.Next(i, Cards.Count);

            Card temp = Cards[i];
            Cards[i] = Cards[j];
            Cards[j] = temp;
        }
    }


    /// <summary>
    /// Remove and return the next card on the deck
    /// </summary>
    /// <returns>The next card</returns>
    public Card DrawCard()
    {
        Card c = Cards.First();
        Cards.RemoveAt(0);
       return c; 
    }
}