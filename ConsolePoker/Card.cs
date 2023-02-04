public class Card
{
    public ECardSuit Suit { get; private set; }
    public ECardValue Value { get; private set; }

    public Card(ECardSuit suit, ECardValue value)
    {
        Suit = suit;
        Value = value;
    }

    // Copy constructor
    public Card(Card c)
    {
        Value = c.Value;
        Suit = c.Suit;

    }

    public override string ToString()
    {
        return String.Format("{0} of {1}", Value, Suit);
    }
}