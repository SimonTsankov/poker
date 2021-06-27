using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Poker
{
    class Combinations
    {
        public static int[] getRepeated(List<Card> cards)
        {
            int[] repeated = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (Card card in cards)
            {
                repeated[card.Value]++;
            }
            return repeated;
        }

        public static bool isPair(List<Card> list, Player player)
        {
            int[] repeated = getRepeated(list);
            for (int i = repeated.Length - 1; i > 0; i--)
            {
                if (repeated[i] == 2)
                {
                    player.combinationHighCardValue = i;
                    return true;
                }
            }
            return false;

        }
        public static bool isThreeOfKind(List<Card> list, Player player)
        {
            int[] repeated = getRepeated(list);
            for (int i = repeated.Length - 1; i > 0; i--)
            {
                if (repeated[i] == 3)
                {
                    player.combinationHighCardValue = i;
                    return true;
                }
            }
            return false;

        }
        public static bool isFourOfKind(List<Card> list, Player player)
        {
            int[] repeated = getRepeated(list);
            for (int i = repeated.Length - 1; i > 0; i--)
            {
                if (repeated[i] == 4)
                {
                    player.combinationHighCardValue = i;
                    return true;
                }
            }
            return false;
        }

        public static bool isFlush(List<Card> cards, Player player)
        {
            List<int> suitValues = new List<int>();

            int highest = 0;
            foreach (Card card in cards)
            {
                if (highest < card.Value)
                    highest = card.Value;
                suitValues.Add((int)card.cardSuit);
            }
            suitValues.Sort();
            //MessageBox.Show(cards.Count + "");
            if (suitValues[0] == suitValues[4])
            {
                player.combinationHighCardValue = highest;
                return true;
            }
            return false;
        }
        public static bool almostStraight(List<Card> cards)
        {
            
            List<int> cardValues = new List<int>();
            foreach (Card card in cards)
            {
                cardValues.Add(card.Value);
            }
            cardValues.Sort();
            int consequative = 0;
            int highest = 0;
            bool isStraightBool = false;
            for (int i = 0; i < cardValues.Count - 1; i++)
            {
                if (cardValues[i] == cardValues[i + 1] - 1)
                {
                    if (cardValues[i + 1] > highest) highest = cardValues[i + 1];


                    consequative++;
                    if (consequative == 3)
                        isStraightBool = true;
                }
                else consequative = 0;
            }
           
            return isStraightBool;
        }
       
        public static bool isStraight(List<Card> cards, Player player)
        {
            List<int> cardValues = new List<int>();
            foreach (Card card in cards)
            {
                cardValues.Add(card.Value);
            }
            cardValues.Sort();
            int consequative = 0;
            int highest = 0;
            bool isStraightBool = false;
            for (int i = 0; i < cardValues.Count - 1; i++)
            {
                if (cardValues[i] == cardValues[i + 1] - 1)
                {
                    if (cardValues[i + 1] > highest) highest = cardValues[i + 1];


                    consequative++;
                    if (consequative == 4)
                        isStraightBool = true;
                }
                else consequative = 0;
            }
            if (isStraightBool) player.combinationHighCardValue = highest;
            return isStraightBool;
        }

        public static bool isFullHouse(List<Card> cards, Player player)
        {
            int[] repeated = getRepeated(cards);

            if (repeated.Contains(2) && repeated.Contains(3))
            {
                int index2 = Array.IndexOf(repeated, 2);
                int index3 = Array.IndexOf(repeated, 3);
                for (int i = 0; i < repeated.Length; i++)
                {
                    if (repeated[i] == 3 || i > index3)
                        index3 = i;
                    if (repeated[i] == 2 || i > index2)
                        index2 = i;
                }
                player.combinationHighCardValue = index2 > index3 ? index2 : index3;
                return true;
            }
            return false;
        }
        public static bool isTwoPair(List<Card> cards, Player player)
        {
            int[] repeated = getRepeated(cards);

            bool flag = false;
            bool finalValue = false;
            int highest = 0;
            for (int i = 0; i < repeated.Length; i++)
            {
                if (repeated[i] == 2)
                {
                    if (highest < i)
                        highest = i;
                    if (flag == true)
                    {
                        highest = highest > i ? highest : i;
                        finalValue = true;
                    }
                    flag = true;
                }

            }
            player.combinationHighCardValue = highest;

            return finalValue;
        }
        public static int isBetter(Player player1, Player player2)//0 is draw, 1 is p1 win, 2 is p2 win
        {
            int p1HighCard = player1.card1.Value > player1.card2.Value ? player1.card1.Value : player1.card2.Value;
            int p2HighCard = player2.card1.Value > player2.card2.Value ? player2.card1.Value : player2.card2.Value;
            if ((int)player1.playerCombination == (int)player2.playerCombination) // COMBINATIONS ARE EQUAL
            {
                
                if (player1.combinationHighCardValue == player2.combinationHighCardValue) //COMBINATIONS AND HIGHEST CARD IN COMBINATIONS ARE EQUAL
                {
                    if (p1HighCard == p2HighCard)//in hand cards are equal as well
                    {
                        return 0;
                    }
                    return p1HighCard > p2HighCard ? 1 : 2;
                }
                if (player1.combinationHighCardValue > player2.combinationHighCardValue)
                    return 1;
                else return 2;
            }
            //PAIRS ARE NOT E
            if ((int)player1.playerCombination > (int)player2.playerCombination) //PAIR 1 IS BETTER THAN PAIR 2
                return 1;
            return 2;
            // PAIR 2 IS BETTER THAN PAIR 1
            //MessageBox.Show((int)player1.playerCombination + "     " + (int)player2.playerCombination);
            /*if ((int)player1.playerCombination == (int)player2.playerCombination && player1.combinationHighCardValue == player2.combinationHighCardValue) return 0;
            if ((int)player1.playerCombination > (int)player2.playerCombination)
                return 1;
            if ((int)player1.playerCombination < (int)player2.playerCombination)
                return 2;
            if (player1.combinationHighCardValue > player2.combinationHighCardValue)
                return 1;
            if (player1.combinationHighCardValue > player2.combinationHighCardValue)
                return 2;
            return 0;*/
        }
    }
}
