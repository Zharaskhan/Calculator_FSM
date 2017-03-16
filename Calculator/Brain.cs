using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{

    public class Brain
    {

        TextBox TextDisplay;
        enum State
        {
            ZeroState,
            AccumulatorState,
            AccumulatorDecimalState,
            ComputeState,
            ErrorState
        }

        State state;

        bool pendingOpStatus;
        string pendingOp;
        double result;

        string bufferNumber;

        public Brain(TextBox _Display) {
            TextDisplay = _Display;
            state = State.ZeroState;
            pendingOpStatus = true;
            pendingOp = "_";
            result = 0;
            bufferNumber = "0";
        }

        void Update()
        {
            switch(pendingOp)
            {
                case "+":
                    result += double.Parse(bufferNumber);
                    break;
                case "-":
                    result -= double.Parse(bufferNumber);
                    break;
                case "X":
                    result *= double.Parse(bufferNumber);
                    break;
                case "÷":
                    //division by 0
                    if (double.Parse(bufferNumber) == 0.0)
                    {
                        state = State.ErrorState;

                        TextDisplay.Text = "DIVISION BY 0";
                        return;
                    }


                    result /= double.Parse(bufferNumber);
                    break;
                case "_":
                    //When we started from ZeroState, so if we typed 2233, result will be 2233
                    result = double.Parse(bufferNumber);
                    break;
                default:
                    //assert
                    Debug.Assert(false);
                    break;
            }
            TextDisplay.Text = result.ToString();
            bufferNumber = "0";
        }

        public void EraserClicked(string text)
        {

            switch (state)
            {
                case State.ZeroState:
                    break;
                case State.AccumulatorDecimalState:
                    if (bufferNumber.Last() == '.')
                    {
                        state = State.AccumulatorState;
                    }
                    
                    bufferNumber = bufferNumber.Remove(bufferNumber.Length - 1);

                    TextDisplay.Text = bufferNumber;
                    break;
                case State.AccumulatorState:

                    if (bufferNumber.Length == 1)
                    {
                        state = State.ZeroState;
                        bufferNumber = "0";
                    } else
                    {

                            bufferNumber = bufferNumber.Remove(bufferNumber.Length - 1);
                    }
                    if (bufferNumber.Length == 1 && (bufferNumber[0] == '0' || bufferNumber[0] == '-'))
                    {
                        bufferNumber = "0";
                        state = State.ZeroState;
                    }

                    TextDisplay.Text = bufferNumber;
                    break;
                case State.ComputeState:
                    break;
                case State.ErrorState:
                    break;

            }

        }




        /*
         * 
         *  switch(state)
                    {
                        case State.ZeroState:
                            break;
                        case State.AccumulatorState:
                            break;
                        case State.AccumulatorDecimalState:
                            break;
                        case State.ComputeState:
                            break;
                        case State.ErrorState:
                            break;

                    }
         */
        //Addition, Substraction, Multiplication, Division
        public void ASMD(string val) {
            switch(state)
            {
                case State.ZeroState:
                    state = State.ComputeState;
                    bufferNumber = "0";
                    pendingOpStatus = true;
                    pendingOp = val;

                    TextDisplay.Text = "0" + val;
                    break;
                case State.AccumulatorDecimalState:
                    //break;
                case State.AccumulatorState:
                    state = State.ComputeState;
                    if (pendingOpStatus == true)
                    {
                        Update();
                    }


                    pendingOpStatus = true;
                    pendingOp = val;
                    bufferNumber = "0";

                    TextDisplay.Text += val;
                    break;
                case State.ComputeState:
                    pendingOpStatus = true;
                    pendingOp = val;
                    //upd
                    TextDisplay.Text += val;
                    break;
                case State.ErrorState:
                    break;    
            }
        }

        public void SeparatorClicked(string val)
        {
            switch (state)
            {
                case State.ZeroState:
                    state = State.AccumulatorDecimalState;
                    bufferNumber = "0.";
                    TextDisplay.Text = "0.";
                    break;
                case State.AccumulatorDecimalState:
                    break;
                case State.AccumulatorState:
                    state = State.AccumulatorDecimalState;
                    bufferNumber += ".";

                    TextDisplay.Text += ".";
                    break;
                case State.ComputeState:
                    state = State.AccumulatorDecimalState;
                    bufferNumber = "0.";
                    TextDisplay.Text = "0.";
                    break;
                case State.ErrorState:
                    break;
            }
        }

        public void EqualsClicked(string val)
        {
            switch (state)
            {
                case State.ZeroState:
                   // state = State.ComputeState;
                    
                    // bufferNumber = "0";

                    //TextDisplay.Text = "0";
 
                case State.AccumulatorDecimalState:
                   
                case State.AccumulatorState:
                    state = State.ComputeState;


                    if (pendingOpStatus == true)
                    {
                        Update();
                    }

                    break;
                case State.ComputeState:
                    pendingOpStatus = false;
                    break;
                case State.ErrorState:
                    break;
                    
            }

        }


        public void digitClicked(string digit) {
            switch (state)
            {
                case State.ZeroState:
                    if (digit != "0")
                    {
                        state = State.AccumulatorState;
                        bufferNumber = digit;
                        TextDisplay.Text = digit;
                    }
                    break;
                case State.AccumulatorDecimalState:
                    bufferNumber += digit;
                    
                    TextDisplay.Text += digit;
                    break;
                case State.AccumulatorState:
                    bufferNumber += digit;
                    TextDisplay.Text += digit;
                    break;
                case State.ComputeState:
                    TextDisplay.Text = "";
                    bufferNumber = digit;
                    if (digit == "0")
                    {
                        state = State.ZeroState;
                    } else
                    {

                        state = State.AccumulatorState;
                    }

                    TextDisplay.Text += digit;
                    break;
                case State.ErrorState:
                    break;
            }

        }
        public void CleanerClicked(string val)
        {

            switch (state)
            {
                case State.ZeroState:
                    break;
                case State.AccumulatorDecimalState:
                    //break;
                case State.AccumulatorState: 
                    //break;
                case State.ComputeState:

                    if (val == "C") {
                        result = 0;
                        pendingOp = "_";
                    }


                    state = State.ZeroState;
                    bufferNumber = "0";
                    TextDisplay.Text = "0";
                    break;
                case State.ErrorState:
                    if (val == "C") {
                        state = State.ZeroState;
                        bufferNumber = "0";
                        TextDisplay.Text = "0";
                        result = 0;
                        pendingOp = "_";
                    }
                    break;

            }

        }

    }
}
