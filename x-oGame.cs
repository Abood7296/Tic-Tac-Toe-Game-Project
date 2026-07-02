using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe_Game
{
    public partial class x_oGame : Form
    {
        public x_oGame()
        {
            InitializeComponent();
        }

        char Player1_2Turn = '1';
        bool GameOver = false;
        bool HasWinner = false;
        private void x_oGame_Paint(object sender, PaintEventArgs e)
        {
            Color Black = Color.FromArgb(220, 26, 34, 94);
            Pen Pen = new Pen(Black);
            Pen.Width = 10;

            Pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            Pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(Pen, 520, 120, 520, 620);
            e.Graphics.DrawLine(Pen, 680, 120, 680, 620);

            e.Graphics.DrawLine(Pen, 360, 280, 840, 280);
            e.Graphics.DrawLine(Pen, 360, 460, 840, 460);

        }
        void TurnPlayers(object sender, EventArgs e)
        {
            if (Player1_2Turn == '1')
            {
                ((PictureBox)sender).Image = Properties.Resources.X;
                ((PictureBox)sender).Tag = "1";
                lblPlayer.Text = "Player2";
                Player1_2Turn = '2';
            }
            else if (Player1_2Turn == '2')
            {
                ((PictureBox)sender).Image = Properties.Resources.O;
                ((PictureBox)sender).Tag = "2";
                lblPlayer.Text = "Player1";
                Player1_2Turn = '1';
            }
        }
        void WhoWinner(PictureBox pb1,PictureBox pb2,PictureBox pb3)
        {
            if (pb1.Tag == pb2.Tag && pb2.Tag == pb3.Tag)
            {
                if (pb1.Tag == "1")
                {
                    pb1.Image = Properties.Resources.WinX;
                    pb2.Image = Properties.Resources.WinX;
                    pb3.Image = Properties.Resources.WinX;
                    MessageBox.Show("Player 1 Wins!", "Who Wins ?", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblWinner.Text = "Player1";
                    HasWinner = true;
                }
                else if (pb1.Tag == "2")
                {
                    pb1.Image = Properties.Resources.WinO;
                    pb2.Image = Properties.Resources.WinO;
                    pb3.Image = Properties.Resources.WinO;
                    MessageBox.Show("Player 2 Wins!", "Who Wins ?", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblWinner.Text = "Player2";
                    HasWinner = true;
                }
            }
        }
        void Draw()
        {
            if (HasWinner)
                return;

            GameOver = true;
            foreach (Control ctrl in panel1.Controls)
            {
                if (ctrl is PictureBox pb)
                {
                    if (pb.Tag == "0")
                    {
                        GameOver = false;
                        break;
                    }
                }
            }
            if (GameOver)
            {
                lblPlayer.Text = "Game Over";
                lblWinner.Text = "Draw";
                MessageBox.Show("Draw", "Who Winner?");
            }
        }
        void CheckWinner()
        {
            //Rows
            WhoWinner(pbsqr1, pbsqr2, pbsqr3);
            WhoWinner(pbsqr4, pbsqr5, pbsqr6);
            WhoWinner(pbsqr7, pbsqr8, pbsqr9);
            //Cols
            WhoWinner(pbsqr1, pbsqr4, pbsqr7);
            WhoWinner(pbsqr2, pbsqr5, pbsqr8);
            WhoWinner(pbsqr3, pbsqr6, pbsqr9);
            //Diagonals
            WhoWinner(pbsqr1, pbsqr5, pbsqr9);
            WhoWinner(pbsqr3, pbsqr5, pbsqr7);
            Draw();
        }
        private void pbsqr_Click(object sender, EventArgs e)
        {
            if (((PictureBox)sender).Tag != null)
            {
                if (((PictureBox)sender).Tag == "1" || ((PictureBox)sender).Tag == "2")
                {
                    MessageBox.Show("Wrong Choice", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    TurnPlayers(sender, e);
                    CheckWinner();
                }
            }
            else
            {
                TurnPlayers(sender, e);
                CheckWinner();
            }
        }
        private void lblRestartGame_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in panel1.Controls)
            {
                if (ctrl is PictureBox pb)
                {
                    pb.Image = Properties.Resources.QuesMark;
                    pb.Tag = "0";
                }
            }
            lblPlayer.Text = "Player1";
            lblWinner.Text = "in Progress";
            Player1_2Turn = '1';
            HasWinner = false;
        }
    }
}