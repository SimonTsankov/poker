using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    class Table
    {
        public List<Card> cards{ get;set; }
        public Table(Deck deck)
        {
            cards = new List<Card>();
            for(int i = 0; i < 5; i++)
            {
                cards.Add(deck.dealCard());
            }
        }
        public void newTable(Deck deck)
        {
            cards = new List<Card>();
            for (int i = 0; i < 5; i++)
            {
                cards.Add(deck.dealCard());
            }
        }
    }
}
