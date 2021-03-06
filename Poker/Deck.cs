using System;
using System.Collections.Generic;


namespace Poker
{
    class Deck
    {
        public List<Card> Cards = new List<Card>();
        public void fillDeck()
        {
            Cards.RemoveRange(0,Cards.Count);
            int clubs = 0;
            
            for(int i = 0; i < 52; i++)
            {
                suit suite = (suit)(Math.Floor((decimal)i / 13));
                if(suite == suit.clubs)  clubs++; 
                int val = i % 13 + 2;
                Cards.Add(new Card(suite,val));
            }
            
        }
        public List<Card> getDeck()
        {
            return Cards;
        }
        public void shuffle()
        {
            Random rnd = new Random();
            for(var i = Cards.Count; i > 0; i--)
            {
                Swap(Cards,0, rnd.Next(0, i));
            }
            for (var i = Cards.Count; i > 0; i--)
            {
                Swap(Cards, 0, rnd.Next(0, i));
            }
        }
        public static void Swap(IList<Card> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        public Card dealCard()
        {
            if (Cards.Count < 1) { fillDeck(); }
            Card card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }
    }
}
