using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DiceCrapGame
{
    class Die
    {
        private Random rnd = new Random();
        private int DieResult1;
        private int DieResult2;

        public Die(TextBox output, PictureBox dOutcome1, PictureBox dOutcome2)
        {
            DieResult1 = RollDie1();
            DieResult2 = RollDie2();

            switch (DieResult1)
            {
                case 1:
                    dOutcome1.Image = Properties.Resources.d1;
                    break;
                case 2:
                    dOutcome1.Image = Properties.Resources.d2;
                    break;
                case 3:
                    dOutcome1.Image = Properties.Resources.d3;
                    break;
                case 4:
                    dOutcome1.Image = Properties.Resources.d4;
                    break;
                case 5:
                    dOutcome1.Image = Properties.Resources.d5;
                    break;
                case 6:
                    dOutcome1.Image = Properties.Resources.d6;
                    break;
            }
            switch (DieResult2)
            {
                case 1:
                    dOutcome2.Image = Properties.Resources.d1;
                    break;
                case 2:
                    dOutcome2.Image = Properties.Resources.d2;
                    break;
                case 3:
                    dOutcome2.Image = Properties.Resources.d3;
                    break;
                case 4:
                    dOutcome2.Image = Properties.Resources.d4;
                    break;
                case 5:
                    dOutcome2.Image = Properties.Resources.d5;
                    break;
                case 6:
                    dOutcome2.Image = Properties.Resources.d6;
                    break;
            }

            output.Text = (DieResult1+ DieResult2).ToString();
        }

        public int getResult()
        { return DieResult1 + DieResult2; }

        private int RollDie1()
        {
            return rnd.Next(1, 7);
        }
        private int RollDie2()
        {
            return rnd.Next(1, 7);
        }

    }
}
