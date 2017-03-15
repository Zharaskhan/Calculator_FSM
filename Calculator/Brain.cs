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
            AccumulateDigitsState,
            AccumulateDigitsWithDecimalState,
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
                    //TODO check division by 0
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

       


        /*
         * 
         *  switch(state)
                    {
                        case State.ZeroState:
                            break;
                        case State.AccumulateDigitsState:
                            break;
                        case State.AccumulateDigitsWithDecimalState:
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
                case State.AccumulateDigitsWithDecimalState:
                    //break;
                case State.AccumulateDigitsState:
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
                    state = State.AccumulateDigitsWithDecimalState;
                    bufferNumber = "0.";
                    TextDisplay.Text = "0.";
                    break;
                case State.AccumulateDigitsWithDecimalState:
                    break;
                case State.AccumulateDigitsState:
                    state = State.AccumulateDigitsWithDecimalState;
                    bufferNumber += ".";

                    TextDisplay.Text += ".";
                    break;
                case State.ComputeState:
                    state = State.AccumulateDigitsWithDecimalState;
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
                    state = State.ComputeState;
                   // bufferNumber = "0";

                    //TextDisplay.Text = "0";
                    break;
                case State.AccumulateDigitsWithDecimalState:
                //break;
                case State.AccumulateDigitsState:
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
                        state = State.AccumulateDigitsState;
                        bufferNumber = digit;
                        TextDisplay.Text = digit;
                    }
                    break;
                case State.AccumulateDigitsWithDecimalState:
                    bufferNumber += digit;
                    TextDisplay.Text += digit;
                    break;
                case State.AccumulateDigitsState:
                    bufferNumber += digit;
                    TextDisplay.Text += digit;
                    break;
                case State.ComputeState:
                    bufferNumber = digit;
                    if (digit == "0")
                    {
                        state = State.ZeroState;
                    } else
                    {

                        state = State.AccumulateDigitsState;
                    }

                    TextDisplay.Text += digit;
                    break;
                case State.ErrorState:
                    break;
            }

        }
    }
}
