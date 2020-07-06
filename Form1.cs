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

namespace Blackjack
{

    public partial class Form1 : Form
    {

        Random random = new Random();
        int playerPosition = 2;
        int dealerPosition = 2;
        List<int> cards = new List<int>();

        public Form1()
        {

            InitializeComponent();

        }

        private void dealCardOnPosition(int position, bool dealer, bool visible)
        {
            if (cards.Count == 0)
            {
                resetGame();
            }

            int cardIndex = random.Next(0, cards.Count - 1);

            //brisehnje na karti vo igra
            int value = cards[cardIndex];
            cards.RemoveAt(cardIndex);


            // MessageBox.Show(cards.Count.ToString());

            PictureBox pb = getPictureBox(position, dealer);
            pb.Tag = value;

            /////delenje na karta
            if (visible)
            {
                pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + value);
            }
            else
            {
                pb.Image = (Image)Properties.Resources.back;
            }
        }

        private int getValueByPosition(int position, bool dealer)
        {
            PictureBox pb = getPictureBox(position, dealer);

            if (pb.Tag == null) return 0;

            return checkValue((int)pb.Tag);
        }

        public int checkValue(int numberCard)
        {
            if (numberCard >= 1 && numberCard <= 4) return 2;
            else if (numberCard >= 5 && numberCard <= 8) return 3;
            else if (numberCard >= 9 && numberCard <= 12) return 4;
            else if (numberCard >= 13 && numberCard <= 16) return 5;
            else if (numberCard >= 17 && numberCard <= 20) return 6;
            else if (numberCard >= 21 && numberCard <= 24) return 7;
            else if (numberCard >= 25 && numberCard <= 28) return 8;
            else if (numberCard >= 29 && numberCard <= 32) return 9;
            else if (numberCard >= 33 && numberCard <= 48) return 10;
            else if (numberCard >= 49 && numberCard <= 52) return 11;//aces
            return 0;
        }

        private void revealCard(int position, bool dealer)
        {
            PictureBox pb = getPictureBox(position, dealer);
            if (pb.Tag == null) return;
            pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("_" + (int)pb.Tag);
        }

        private void gameStep()
        {


            if (getSum(false) < 21)
            {
                playerPosition++;
                dealCardOnPosition(playerPosition, false, true);
                calculateSums();
                // calculateWinner();
                if (getSum(false) >= 21)
                {   
                    processDealer();
                    calculateWinner();
                  
                }

            } 
            else if (getSum(false) == 21)
            {
                calculateWinner();
            }
            else
            {
                processDealer();
            }
            /*
            if (getSum(false) > 21)
            {

            }

      */
           

        }

        private void clearPosition(int position, bool dealer)
        {
            PictureBox pb = getPictureBox(position, dealer);
            pb.Tag = null;
            pb.Image = null;
        }

        private int getSum(bool dealer)
        {
            int sum = 0;
            int max = dealer ? dealerPosition : playerPosition;
            for (int i = 1; i <= max; i++)
            {
                sum += getValueByPosition(i, dealer);

            }
            return sum;
        }

        private void calculateWinner()
        {
            if (getSum(true) <= 21 && getSum(false) > 21)
            {
                MessageBox.Show("The winner is the dealer");
            }
            else if (getSum(true) > 21 && getSum(false) < 21)
            {
                MessageBox.Show("The winner is the player");
            }
            else if (getSum(true) > 21 && getSum(false) > 21)
            {
                MessageBox.Show("No winner");
            }
            else if (getSum(false) < 21 && getSum(true) < 21)
            {
                if (getSum(false) > getSum(true))
                {
                    MessageBox.Show("The winner is the player");
                }
                if (getSum(false) < getSum(true))
                {
                    MessageBox.Show("The winner is the dealer");
                }
                if (getSum(false) == getSum(true))
                {
                    MessageBox.Show("No winner");
                }
            }
        }
       

        private void processDealer()
        {
           
            revealCard(2, true);
            calculateSums();
            
            while (getSum(true) <= 16)
            {
                dealCardOnPosition(dealerPosition, true, true);
                dealerPosition++;
                calculateSums();
            }
            
            calculateSums();
            calculateWinner();
        }

        private void calculateSums()
        {

         
            lblPlayer.Text = getSum(false).ToString();

            if((int)dealerPosition == 2 && getSum(false)<21)
            {
                lblDealer.Text = getValueByPosition(1, true).ToString();
            }
            
            else
            {
                lblDealer.Text = getSum(true).ToString();
            }
          //  lblDealer.Text = getSum(true).ToString();
        }

        private void resetGame()
        {
            cards.Clear();
            for (int i = 1; i < 52; i++)
            {
                cards.Add(i);
            }

            playerPosition = 2;
            dealerPosition = 2;
            for (int i = 1; i <= 7; i++)
            {
                clearPosition(i, true);
                clearPosition(i, false);
            }

            dealCardOnPosition(1, true, true);
            dealCardOnPosition(2, true, false);

            dealCardOnPosition(1, false, true);
            dealCardOnPosition(2, false, true);
            calculateSums();
        }

        private PictureBox getPictureBox(int position, bool dealer)
        {
            foreach (Control c in Controls)
            {
                string name = dealer ? "pbDealer" : "pictureBox";
                if (c is PictureBox && c.Name == name + position)
                {
                    return (PictureBox)c;
                }
            }
            return null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnDealCard_Click(object sender, EventArgs e)
        {
            gameStep();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            resetGame();
        }

        private void btnStand_Click(object sender, EventArgs e)
        {
            processDealer();
        }

    }
}
