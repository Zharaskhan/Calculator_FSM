using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        Brain brain;
        public Form1()
        {
            InitializeComponent();
            brain = new Brain(Display);
        }

        private void digit_Click(object sender, EventArgs e)
        {
            Button curButton = sender as Button;
            brain.digitClicked(curButton.Text);
        }

        //Addition, Substraction, Multiplication, Division
        private void ASMD_Click(object sender, EventArgs e)
        {
            Button curButton = sender as Button;
            brain.ASMD(curButton.Text);
        }

        private void operatorEquals_Click(object sender, EventArgs e)
        {
            Button curButton = sender as Button;
            brain.EqualsClicked(curButton.Text);
        }

        private void separator_Click(object sender, EventArgs e)
        {

            Button curButton = sender as Button;
            brain.SeparatorClicked(curButton.Text);
        }

        private void operatorClear_Click(object sender, EventArgs e)
        {
            Button curButton = sender as Button;
            brain.CleanerClicked(curButton.Text);
        }

        private void operatorErase_Click(object sender, EventArgs e)
        {

            Button curButton = sender as Button;
            brain.EraserClicked(curButton.Text);
        }

        private void function_Click(object sender, EventArgs e)
        {

            Button curButton = sender as Button;
            brain.FunctionClicked(curButton.Text);
        }

        private void operatorPlusMinus_Click(object sender, EventArgs e)
        {
            Button curButton = sender as Button;
            brain.PlusMinusClicked(curButton.Text);
        }
    }
}
