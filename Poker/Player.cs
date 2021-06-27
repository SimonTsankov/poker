using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Poker
{
    class Player
    {
        public Card card1 { get; set; }
        public Card card2 { get; set; }
        public int money { get; set; }
        public Table table { get; set; }
        public combination playerCombination
        {
            get
            {

                List<Card> allCards = new List<Card>();
               
                allCards.Add(card1);
                allCards.Add(card2);
                
                allCards = allCards.Concat(table.cards).ToList();
                /*if (Form1.move == 5)
                    allCards = allCards.Concat(table.cards).ToList();
                if (Form1.move == 4)
                {
                    allCards.Add(table.cards[0]);
                    allCards.Add(table.cards[1]);
                    allCards.Add(table.cards[2]);
                    allCards.Add(table.cards[3]);
                }
                else
                {

                    if (Form1.move == 3)
                    {
                        allCards.Add(table.cards[0]);
                        allCards.Add(table.cards[1]);
                        allCards.Add(table.cards[2]);
                    }
                }*/
                // MessageBox.Show(Form1.move+"");

                if (Combinations.isStraight(allCards, this) && Combinations.isFlush(allCards,this)) return combination.straightFlush;
                if (Combinations.isFourOfKind(allCards, this)) return combination.fourOfKind;
                if (Combinations.isFullHouse(allCards, this)) return combination.fullHouse;
                if (Combinations.isFlush(allCards,this)) return combination.flush;
                if (Combinations.isStraight(allCards, this)) return combination.straight;
                if (Combinations.isThreeOfKind(allCards, this)) return combination.threeOfKind;
                if (Combinations.isTwoPair(allCards, this)) return combination.twoPair;
                if (Combinations.isPair(allCards, this)) return combination.pair;
                combinationHighCardValue = card1.Value > card2.Value ? card1.Value : card2.Value;
                return combination.highCard;
            }
        }
        public int getHighestCard(List<Card> cards)
        {
            int highest = 2;
            foreach (Card card in cards)
            {
                if (highest < card.Value)
                    highest = card.Value;
            }
            return highest;
        }
        public int combinationHighCardValue;
        public string Name { get; set; }
        public action currentAction { get; set; }
        public void removeMoney(int given)
        {
            money -= given;
        }
        public void giveMoney(int give)
        {
            money += give;
        }

        public Player(Table t)
        {
            table = t;
            money = 1000;
        }

    }
    public enum action
    {
        call,
        check,
        fold
    }
    public enum combination
    {
        highCard=1,
        pair=2,
        twoPair=3,
        threeOfKind=4,
        straight=5,
        flush=6,
        fullHouse=7,
        fourOfKind=8,
        straightFlush=9,
        royalFlush=10
    }
}
