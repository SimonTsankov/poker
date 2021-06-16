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

        }
        private void playerMove(Deck deck, Player player)
        {
            player.card1 = this.deck.dealCard();
            player.card2 = this.deck.dealCard();

            valueBoxPlayer1.Image = player.card1.getValueImg();
            suitBoxPlayer1.Image = player.card1.getSuitImg();
            valueBoxPlayer2.Image = player.card2.getValueImg();
            suitBoxPlayer2.Image = player.card2.getSuitImg();
        }
        private void botMove()
        {
            bot.card1 = this.deck.dealCard();
            bot.card2 = this.deck.dealCard();

            valueBoxEnemy1.Image = bot.card1.getValueImg();
            suitBoxEnemy1.Image = bot.card1.getSuitImg();
            valueBoxEnemy2.Image = bot.card2.getValueImg();
            suitBoxEnemy2.Image = bot.card2.getSuitImg();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(move==5)
            {
                
                inGame = false;
                
                clearCards();
                winnerLabel.Text = "";
                deck.fillDeck();
                deck.shuffle();
                table.newTable(deck);
               
            }
            if (inGame == false)
            {
                inGame = true;
                move = 3;
            }
            else {

                flip_card_Click();
                return;
                    }
            
            deck.shuffle();
            
            
            playerMove(deck, player);
            botMove();
            
            
            for(int i = 0; i < 3; i++) {                         
                valueBoxesTable[i].Image = table.cards[i].getValueImg();
                suitBoxesTable[i].Image = table.cards[i].getSuitImg();              
            }

            CombinationLabel.Text = " Bot combination:  " + bot.playerCombination + "\t    highest :" + bot.combinationHighCardValue +
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
            foreach(PictureBox img in valueBoxesTable)
            {
                img.Image = null; 
            }
            foreach (PictureBox img in suitBoxesTable)
            {
                img.Image = null;
            }
        }
       /* private void flip_card_Click(object sender, EventArgs e)
        {
            
            switch (move)
            {
                case 3:
                    valueBoxesTable[3].Image = table.cards[3].getValueImg();
                    suitBoxesTable[3].Image = table.cards[3].getSuitImg();
                    break;
                case 4:
                    valueBoxesTable[4].Image = table.cards[4].getValueImg();
                    suitBoxesTable[4].Image = table.cards[4].getSuitImg();
                    
                    break;
                case 5:
                    inGame = false;
                   
                    clearCards();
                    break;
            }
            move++;
            CombinationLabel.Text = " Bot combination:  " + bot.playerCombination +"\t    highest :" +bot.combinationHighCardValue+
                "\nCombination player: " + player.playerCombination+" \t       highest :" + player.combinationHighCardValue;
            
           
        }*/
        private void flip_card_Click()
        {

            switch (move)
            {
                case 3:
                    valueBoxesTable[3].Image = table.cards[3].getValueImg();
                    suitBoxesTable[3].Image = table.cards[3].getSuitImg();
                    break;
                case 4:
                    valueBoxesTable[4].Image = table.cards[4].getValueImg();
                    suitBoxesTable[4].Image = table.cards[4].getSuitImg();
                    move++;
                    CombinationLabel.Text = " Bot combination:  " + bot.playerCombination + "\t    highest :" + bot.combinationHighCardValue +
                "\nCombination player: " + player.playerCombination + " \t       highest :" + player.combinationHighCardValue;
                    switch (Combinations.isBetter(player, bot))
                    {
                         
                        case 0:
                            winnerLabel.Text = " draw";
                            break;
                        case 1:
                            winnerLabel.Text = " Player won";
                            break;
                        case 2:
                            winnerLabel.Text = "Bot won";
                            break;
                            
                    }
                    move--;
                    //flip_card.Text = "New Game!";
                    break;
                case 5:
                    
                    inGame = false;
                    //flip_card.Text = "Flip card";
                    clearCards();
                    winnerLabel.Text = "";
                    break;
            }
            move++;
            CombinationLabel.Text = " Bot combination:  " + bot.playerCombination + "\t    highest :" + bot.combinationHighCardValue +
                "\nCombination player: " + player.playerCombination + " \t       highest :" + player.combinationHighCardValue;
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
            errorLabel.Text = Combinations.isTwoPair(list,player)+""+player.combinationHighCardValue;

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
    }
}
