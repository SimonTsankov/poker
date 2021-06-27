using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker
{
    public partial class Form1 : Form
    {
        Boolean inGame = false;
        public static int move = 0;
        Table table;
        Player player;
        Player bot;
        Deck deck = new Deck();
        List<PictureBox> valueBoxesTable = new List<PictureBox>();
        List<PictureBox> suitBoxesTable = new List<PictureBox>();

        public Form1()
        {
            InitializeComponent();
            fillBoxeTables();

            deck.fillDeck();
            deck.shuffle();
            table = new Table(deck);
            player = new Player(table);
            bot = new Player(table);
            playerMoneyLabel.Text = "Player Money: " + player.money;
            botMoneyLabel.Text = "Bot money: " + bot.money;
        }
        private void playerMove(Deck deck, Player player)
        {
            player.card1 = this.deck.dealCard();
            player.card2 = this.deck.dealCard();
            //visualise card 1
            valueBoxPlayer1.Image = player.card1.getValueImg();
            suitBoxPlayer1.Image = player.card1.getSuitImg();
            //visualise card 2
            valueBoxPlayer2.Image = player.card2.getValueImg();
            suitBoxPlayer2.Image = player.card2.getSuitImg();
        }
        private void botMove()
        {
            bot.card1 = this.deck.dealCard();
            bot.card2 = this.deck.dealCard();
            /*
            //visualise card 1
            valueBoxEnemy1.Image = bot.card1.getValueImg();
            suitBoxEnemy1.Image = bot.card1.getSuitImg();
            //visualise card 2
            valueBoxEnemy2.Image = bot.card2.getValueImg();
            suitBoxEnemy2.Image = bot.card2.getSuitImg();
            */
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (inGame == false)
            {
                winnerLabel.Text = "Winner:";
                clearCards();
                inGame = true;
                move = 3;
                deck.fillDeck();
                deck.shuffle();
                playerMove(deck, player);
                botMove();
                table.newTable(deck);
                //visualizes first 3 cards
                for (int i = 0; i < 3; i++)
                {
                    valueBoxesTable[i].Image = table.cards[i].getValueImg();
                    suitBoxesTable[i].Image = table.cards[i].getSuitImg();
                }
                table.pot = 100;
                player.money -= 50;
                bot.money -= 50;
                updateLabels();


            }
            else errorLabel.Text = "Finsih this game first!";

        }
        public void updateLabels()
        {
            playerMoneyLabel.Text = "Player Money: " + player.money;

            botMoneyLabel.Text = "Bot money: " + bot.money;

            pot_label.Text = "POT: " + table.pot;

            CombinationLabel.Text =
                "\nCombination player: " + player.playerCombination + " \t       highest :" + player.combinationHighCardValue;
        }

        void clearCards()
        {
            valueBoxEnemy1.Image = null;
            suitBoxEnemy1.Image = null;
            valueBoxEnemy2.Image = null;
            suitBoxEnemy2.Image = null;

            valueBoxPlayer1.Image = null;
            suitBoxPlayer1.Image = null;
            valueBoxPlayer2.Image = null;
            suitBoxPlayer2.Image = null;
            foreach (PictureBox img in valueBoxesTable)
            {
                img.Image = null;
            }
            foreach (PictureBox img in suitBoxesTable)
            {
                img.Image = null;
            }
        }

        private void flip_card_Click()
        {
            raiseUpDown.Maximum = player.money;
            playerMoneyLabel.Text = "Player Money: " + player.money;

            botMoneyLabel.Text = "Bot money: " + bot.money;
            // MessageBox.Show(move + "");
            pot_label.Text = "POT: " + table.pot;
            switch (move)
            {

                case 3://first click, you reveal the 4th card

                    table.placeCard(deck);
                    valueBoxesTable[3].Image = table.cards[3].getValueImg();
                    suitBoxesTable[3].Image = table.cards[3].getSuitImg();
                    updateLabels();
                    break;
                case 4://you reveal the 5th card

                    table.placeCard(deck);
                    valueBoxesTable[4].Image = table.cards[4].getValueImg();
                    suitBoxesTable[4].Image = table.cards[4].getSuitImg();
                    updateLabels();
                    break;
                case 5://after last move winners is determined

                    //  reveals bot cards
                    //visualise card 1
                    valueBoxEnemy1.Image = bot.card1.getValueImg();
                    suitBoxEnemy1.Image = bot.card1.getSuitImg();
                    //visualise card 2
                    valueBoxEnemy2.Image = bot.card2.getValueImg();
                    suitBoxEnemy2.Image = bot.card2.getSuitImg();

                    switch (Combinations.isBetter(player, bot))
                    {

                        case 0:
                            winnerLabel.Text = " draw";
                            player.money += table.pot / 2;
                            bot.money += table.pot / 2;
                            break;
                        case 1:
                            winnerLabel.Text = " Player won";

                            player.money += table.pot;


                            break;
                        case 2:
                            winnerLabel.Text = "Bot won";


                            bot.money += table.pot;
                            break;

                    }
                    updateLabels();
                    //shows bot combination and overwrites old combinationLabel
                    CombinationLabel.Text = " Bot combination:  " + bot.playerCombination + "\t    highest :" + bot.combinationHighCardValue +
                        "\nCombination player: " + player.playerCombination + " \t       highest :" + player.combinationHighCardValue;
                    inGame = false;
                    break;

            }


            move++;

        }
        private void button2_Click(object sender, EventArgs e)
        {


            List<Card> list = new List<Card>();
            list.Add(new Card(suit.clubs, 2));
            list.Add(new Card(suit.clubs, 2));
            list.Add(new Card(suit.clubs, 4));
            list.Add(new Card(suit.clubs, 5));
            list.Add(new Card(suit.clubs, 6));
            list.Add(new Card(suit.clubs, 8));
            list.Add(new Card(suit.clubs, 8));
            errorLabel.Text = Combinations.isTwoPair(list, player) + "" + player.combinationHighCardValue;

        }
        private void fillBoxeTables()
        {
            valueBoxesTable.Add(valueBoxTable1);
            valueBoxesTable.Add(valueBoxTable2);
            valueBoxesTable.Add(valueBoxTable3);
            valueBoxesTable.Add(valueBoxTable4);
            valueBoxesTable.Add(valueBoxTable5);

            suitBoxesTable.Add(suitBoxTable1);
            suitBoxesTable.Add(suitBoxTable2);
            suitBoxesTable.Add(suitBoxTable3);
            suitBoxesTable.Add(suitBoxTable4);
            suitBoxesTable.Add(suitBoxTable5);
        }

        private void fold_btn_Click(object sender, EventArgs e)
        {
            if (inGame)
            {
                inGame = false;
                bot.money += table.pot;
                clearCards();
                winnerLabel.Text = "";
                deck.fillDeck();
                deck.shuffle();
                table.newTable(deck);
                table.pot = 0;
                button1.PerformClick();
            }
            else button1.PerformClick();
        }
        public Random random = new Random();
        private Boolean acceptRaise()//TO DO check if table is almost flush or almost straight so bot is more coutious
        {
           
            int perCent = random.Next(0, 100);
            if (perCent < 8) return true;
            switch (raiseUpDown.Value)
            {

                case decimal n when (n > 100 && n < 300):
                    if ((int)bot.playerCombination >= 2)
                    {
                        return true;
                    }
                    perCent = random.Next(0, 100);
                    if (perCent < 25 && (int)bot.playerCombination == 1) {
                        return true;
                    }
                    break;

                case decimal n when (n > 300 && n < 500):
                    if ((int)bot.playerCombination >= 3)
                    {
                        return true;
                    }
                    perCent = random.Next(0, 100);
                    if (perCent<15&&(int)bot.playerCombination==2&&bot.combinationHighCardValue>10) {
                        return true;
                    }
                    break;

                case decimal n when (n > 500 && n < 1000):
                    if ((int)bot.playerCombination >= 5)
                    {
                        return true;
                    }
                    perCent = random.Next(0, 100);
                    if (perCent < 60 && (int)bot.playerCombination ==4)
                    {
                        return true;
                    }
                    perCent = random.Next(0, 100);
                    if (perCent < 30 && (int)bot.playerCombination == 3)
                    {
                        return true;
                    }
                    perCent = random.Next(0, 100);
                    if (perCent < 5 && (int)bot.playerCombination == 2)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }
        private void raise_btn_Click(object sender, EventArgs e)
        {

            if (inGame)
            {
                if (acceptRaise())
                {
                    table.pot += (int)raiseUpDown.Value * 2;
                    player.money -= (int)raiseUpDown.Value;
                    bot.money -= (int)raiseUpDown.Value;
                    flip_card_Click();
                }
                else fold_btn.PerformClick();


            }
            else button1.PerformClick();
        }

        private void check_btn_Click(object sender, EventArgs e)
        {
            if (inGame)
            {
                flip_card_Click();
            }
            else button1.PerformClick();
        }
    }
}
