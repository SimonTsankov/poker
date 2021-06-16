using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Poker
{
    class Card
    {
        public suit cardSuit { get; set; }
        public int Value
        {
            get;
            set;
        }
        public Card(suit cardS,int cardF)
        {
            cardSuit = cardS;
            Value = cardF;
        }
        public string NamedValue
        {
            get
            {
                string name = string.Empty;
                switch (Value)
                {
                    case (14):
                        name = "Ace";
                        break;
                    case (13):
                        name = "King";
                        break;
                    case (12):
                        name = "Queen";
                        break;
                    case (11):
                        name = "Jack";
                        break;
                    default:
                        name = Value.ToString();
                        break;
                }
                return name;
            }

            
        }
        public string Name
        {
            get{ 
            return  NamedValue+" of " +cardSuit.ToString();
            }
        }
        public Image getSuitImg()
        {
            return Image.FromFile(Path.Combine(System.IO.Path.GetFullPath(@"..\..\"), "Resources") + "\\" + cardSuit + ".png");
        }
        public Image getValueImg()
        {
            Boolean isBlackSuit = false;
            if (cardSuit == suit.clubs || cardSuit == suit.spades) isBlackSuit = true;
            return Image.FromFile(Path.Combine(System.IO.Path.GetFullPath(@"..\..\"), "Resources") + "\\" + (isBlackSuit ? "black" : "") + Value + ".png");

        }
    }
    
    public enum suit
    {
        clubs,
        diamonds,
        hearts,
        spades
    }
}
