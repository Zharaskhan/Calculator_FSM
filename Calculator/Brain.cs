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
        double memory;

        string bufferNumber;

        public Brain(TextBox _Display) {
            TextDisplay = _Display;
            state = State.ZeroState;
            pendingOpStatus = true;
            pendingOp = "_";
            result = 0;
            bufferNumber = "0";
            TextDisplay.Text = "0";
            memory = 0;
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

        internal void PlusMinusClicked(string text)
        {
            switch (state)
            {
                case State.ZeroState:
                    break;
                case State.AccumulatorDecimalState:
                    bufferNumber = (double.Parse(bufferNumber) * -1.0).ToString("F6");

                    TextDisplay.Text = bufferNumber;
                    break;
                case State.AccumulatorState:

                    bufferNumber = (double.Parse(bufferNumber) * -1.0).ToString("F6");



                    TextDisplay.Text = bufferNumber;
                    break;
                case State.ComputeState:

                    result = -result;

                    TextDisplay.Text = result.ToString("F6");
                    break;
                case State.ErrorState:
                    break;

            }
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
        public void MemoryClicked(string val)
        {
            if (val == "MC") {
                memory = 0;
            } else if (val == "MS")
            {
                memory = double.Parse(bufferNumber);
            } else if (val == "M-")
            {

                memory -= double.Parse(bufferNumber);
            } else if (val == "M+")
            {

                memory += double.Parse(bufferNumber);
            } else if (val == "MR")
            {
                bufferNumber = memory.ToString("F6");
                TextDisplay.Text = bufferNumber;
            }


        }

        public void FunctionClicked(string val)
        {

            switch (state)
            {
                case State.ZeroState:
                    if (val == "1/x")
                    {
                        state = State.ErrorState;

                        TextDisplay.Text = "DIVISION BY 0";
                    }
                    break;
                case State.ComputeState:
                    
                case State.AccumulatorDecimalState:
                //break;
                case State.AccumulatorState:
                    if (state == State.ComputeState)
                    {
                        bufferNumber = result.ToString("F6");
                        pendingOp = "_";
                      //  MessageBox.Show(bufferNumber + " " + result);
                    }
                    double cur_num = double.Parse(bufferNumber);
                    if (val == "x^2")
                    {
                        state = State.ComputeState;
                        bufferNumber = (cur_num * cur_num).ToString("F6");

                    } else if (val == "√")
                    {
                        if (cur_num < 0.0)
                        {
                            state = State.ErrorState;

                            TextDisplay.Text = "Invalid input";
                            return;
                        } else
                        {
                            state = State.ComputeState;
                            bufferNumber = (Math.Sqrt(cur_num)).ToString("F6");
                        }

                    } else if (val == "1/x")
                    {
                        if (cur_num == 0.0)
                        {
                            state = State.ErrorState;

                            TextDisplay.Text = "Division by Zero";
                            return;
                        }
                        else
                        {
                            state = State.ComputeState;
                            bufferNumber = (1.0 / cur_num).ToString("F6");
                        }
                    } else if (val == "%")
                    {
                        state = State.ComputeState;
                        bufferNumber = (result / 100.0 * cur_num).ToString("F6");
                    }



                    Update();
                    break;
               
                case State.ErrorState:
                   

                    break;

            }

        }

    }
}
