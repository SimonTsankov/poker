using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    class Table
    {
        public List<Card> cards{ get;set; }
        public int pot { get; set; }
        public Table(Deck deck)
        {
            pot = 0;
            cards = new List<Card>();
            for(int i = 0; i < 5; i++)
            {
                cards.Add(deck.dealCard());
            }
        }
        public void placeCard(Deck deck)
        {
            cards.Add(deck.dealCard());
        }
        public void newTable(Deck deck)
        {
            pot = 0;
            cards = new List<Card>();
            for (int i = 0; i < 3; i++)
            {
                cards.Add(deck.dealCard());
            }
        }
    }
}
