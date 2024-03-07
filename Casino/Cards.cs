using System;

public class Cards {
    public enum Suit {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public class Card {
        public Suit suit { get; set; }
        public int value { get; set; }

        public override bool Equals(object obj) {
            if (obj == null || this.GetType() != obj.GetType()) {
                return false;
            }

            Card card = (Card)obj;
            return (this.suit == card.suit) && (this.value == card.value);
        }

        public override int GetHashCode() {
            return HashCode.Combine(suit, value);
        }
    }

    public List<Card> cards { get; set; } = new List<Card>();

    public Card NextCard() {
        Random random = new Random();
        Card newCard;

        if (cards.Count == 52) {
            Console.WriteLine("Deck is empty.");
            Reset();
        }

        do {
            newCard = new Card {
                suit = (Suit)random.Next(0, 4),
                value = random.Next(1, 14)
            };
        }
        while (cards.Exists(c => c.suit == newCard.suit && c.value == newCard.value));

        cards.Add(newCard);

        return newCard;
    }

    public Card TryCard() {
        Random random = new Random();
        Card newCard;

        if (cards.Count == 52) {
            Console.WriteLine("Deck is empty.");
            Reset();
        }

        do {
            newCard = new Card {
                suit = (Suit)random.Next(0, 4),
                value = random.Next(1, 14)
            };
        }
        while (cards.Exists(c => c.suit == newCard.suit && c.value == newCard.value));

        return newCard;
    }

    public void AddCard(Card card) {
        cards.Add(card);
    }

    public void Reset() {
        cards.Clear();
    }
}