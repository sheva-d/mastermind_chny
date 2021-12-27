using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;

namespace MasterMind
{
    public partial class Form1 : Form
    {
        public int buttonCount = 0;
        public bool win = false;
        public int player = 0;
        public int inputedAnswearsCount = 0;

        //current row
        public int[] rgb1 = new int[3];
        public int[] rgb2 = new int[3];
        public int[] rgb3 = new int[3];
        public int[] rgb4 = new int[3];

        //correct answears
        public int[] cAns1 = new int[3];
        public int[] cAns2 = new int[3];
        public int[] cAns3 = new int[3];
        public int[] cAns4 = new int[3];

        public Form1()
        {
            InitializeComponent();
            NewGame();
        }

        //color picker that returns array color value
        public int[] ChooseColor(int i)
        {
            int[] colors = new int[3];

            switch (i)
            {
                case 1:
                  colors[0] = 219;
                  colors[1] = 99;
                  colors[2] = 22;
                  break;
                case 2:
                  colors[0] = 128;
                  colors[1] = 188;
                  colors[2] = 163;
                  break;
                case 3:
                  colors[0] = 255;
                  colors[1] = 203;
                  colors[2] = 43;
                  break;
                case 4:
                  colors[0] = 191;
                  colors[1] = 77;
                  colors[2] = 40;
                  break;
                case 5:
                  colors[0] = 236;
                  colors[1] = 129;
                  colors[2] = 117;
                  break;
                case 6:
                  colors[0] = 45;
                  colors[1] = 105;
                  colors[2] = 131;
                  break;
                case 7:
                  colors[0] = 124;
                  colors[1] = 110;
                  colors[2] = 90;
                  break;
                case 8:
                  colors[0] = 170;
                  colors[1] = 86;
                  colors[2] = 100;
                  break;
            }
            return colors;
        }

        //fill in oval and validations
        public void ColorCircle(int[] i)
        {
            int vFlag1 = 0;
            int vFlag2 = 0;
            int vFlag3 = 0;
            int vFlag4 = 0;
            int vFlag5 = 0;
            int vFlag6 = 0;
            int vFlag7 = 0;
            int vFlag8 = 0;

            int blackCount = 0;

            //loops all the ovals in form
            var ovals = Controls.OfType<Panel>()
                       .SelectMany(p => p.Controls.OfType<ShapeContainer>()
                           .SelectMany(sc => sc.Shapes.OfType<OvalShape>()));

            foreach (var oval in ovals)
            {
                if (oval.FillColor == Color.WhiteSmoke)
                {
                    oval.FillColor = Color.FromArgb(i[0], i[1], i[2]);

                    //store colors in temporary value

                    if (buttonCount % 4 == 1)
                        rgb1 = i;
                    else if (buttonCount % 4 == 2)
                        rgb2 = i;
                    else if (buttonCount % 4 == 3)
                        rgb3 = i;
                    else if (buttonCount % 4 == 0)
                        rgb4 = i;
                    break;
                }
            }

            //validations
            for (int j = 0; j < 4; j++)
            {
                foreach (var oval in ovals)
                {
                    if (oval.FillColor == Color.Gainsboro && buttonCount % 4 == 0)
                    {
                        //for black fills
                        if (rgb1[0] == cAns1[0] && vFlag1 == 0)
                        {
                            oval.FillColor = Color.Black;
                            vFlag1 = 1;
                            blackCount++;
                        }
                        else if (rgb2[0] == cAns2[0] && vFlag2 == 0)
                        {
                            oval.FillColor = Color.Black;
                            vFlag2 = 1;
                            blackCount++;
                        }
                        else if (rgb3[0] == cAns3[0] && vFlag3 == 0)
                        {
                            oval.FillColor = Color.Black;
                            vFlag3 = 1;
                            blackCount++;
                        }
                        else if (rgb4[0] == cAns4[0] && vFlag4 == 0)
                        {
                            oval.FillColor = Color.Black;
                            vFlag4 = 1;
                            blackCount++;
                        }

                        //for white fills
                        else if ((rgb1[0] == cAns2[0] || rgb1[0] == cAns3[0] || rgb1[0] == cAns4[0]) && vFlag5 == 0)
                        {
                            oval.FillColor = Color.White;
                            vFlag5 = 1;
                        }
                        else if ((rgb2[0] == cAns1[0] || rgb2[0] == cAns3[0] || rgb2[0] == cAns4[0]) && vFlag6 == 0)
                        {
                            oval.FillColor = Color.White;
                            vFlag6 = 1;
                        }
                        else if ((rgb3[0] == cAns1[0] || rgb3[0] == cAns2[0] || rgb3[0] == cAns4[0]) && vFlag7 == 0)
                        {
                            oval.FillColor = Color.White;
                            vFlag7 = 1;
                        }
                        else if ((rgb4[0] == cAns1[0] || rgb4[0] == cAns2[0] || rgb4[0] == cAns3[0]) && vFlag8 == 0)
                        {
                            oval.FillColor = Color.White;
                            vFlag8 = 1;
                        }

                        //if no similarities found, fill gray
                        else
                        {
                            oval.FillColor = Color.Gray;
                        }
                        break;
                    }
                }
            }

            //check win
            if (blackCount == 4)
            {
                win = true;

                showCorrectColorFillment();

                MessageBox.Show("Congratulations! You Won");
                NewGame();
            }

            //if lose
            if (buttonCount == 40 && win == false)
            {
                showCorrectColorFillment();

                MessageBox.Show("Game over. Please try again!");
                NewGame();
            }
        }

        public void showCorrectColorFillment()
        {
            ans1.FillColor = Color.FromArgb(cAns1[0], cAns1[1], cAns1[2]);
            ans2.FillColor = Color.FromArgb(cAns2[0], cAns2[1], cAns2[2]);
            ans3.FillColor = Color.FromArgb(cAns3[0], cAns3[1], cAns3[2]);
            ans4.FillColor = Color.FromArgb(cAns4[0], cAns4[1], cAns4[2]);
        }

        public void hideCorrectAnswer()
        {
            ans1.FillColor = Color.Black;
            ans2.FillColor = Color.Black;
            ans3.FillColor = Color.Black;
            ans4.FillColor = Color.Black;
        }

        private void color_Click(object sender, EventArgs e)
        {
            string buttonName = ((OvalShape)sender).Name;
            int buttonColor = Int32.Parse(buttonName.Substring(buttonName.Length - 1));
            if (player == 0)
            {
                List<OvalShape> correctAnswearOvals = new List<OvalShape>();
                List<int[]> correctAnswear = new List<int[]>();

                correctAnswearOvals.Add(ans1);
                correctAnswearOvals.Add(ans2);
                correctAnswearOvals.Add(ans3);
                correctAnswearOvals.Add(ans4);

                correctAnswear.Add(cAns1);
                correctAnswear.Add(cAns2);
                correctAnswear.Add(cAns3);
                correctAnswear.Add(cAns4);
                correctAnswearOvals[inputedAnswearsCount].FillColor = Color.FromArgb(ChooseColor(buttonColor)[0], 
                                                                                     ChooseColor(buttonColor)[1], 
                                                                                     ChooseColor(buttonColor)[2]);
                correctAnswear[inputedAnswearsCount][0] = ChooseColor(buttonColor)[0];
                correctAnswear[inputedAnswearsCount][1] = ChooseColor(buttonColor)[1];
                correctAnswear[inputedAnswearsCount][2] = ChooseColor(buttonColor)[2];

                inputedAnswearsCount++;
                if (inputedAnswearsCount == 4) {
                    ChangePlayer();
                }
            } 
            else
            {
              buttonCount++;
              ColorCircle(ChooseColor(buttonColor));
            }
        }

        public void ChangePlayer()
        {
            player = 1;
            hideCorrectAnswer();
            MessageBox.Show("Hello Player 2! Try to guess color code in 10 tries. Be carefull, color can repeat.");
        }

        public void NewGame()
        {
            win = false;
            buttonCount = 0;
            player = 0;
            inputedAnswearsCount = 0;

            var ovals = Controls.OfType<Panel>()
                       .SelectMany(p => p.Controls.OfType<ShapeContainer>()
                           .SelectMany(sc => sc.Shapes.OfType<OvalShape>()));

            foreach (var oval in ovals)
            {
                if (oval.FillColor == Color.White || oval.FillColor == Color.Black || oval.FillColor == Color.Gray || oval.FillColor == Color.Gainsboro)
                    oval.FillColor = Color.Gainsboro;
                else
                    oval.FillColor = Color.WhiteSmoke;
            }
            hideCorrectAnswer();
            MessageBox.Show("Hello Player 1! Choose color code for next player to guess");
        }
    }


}
