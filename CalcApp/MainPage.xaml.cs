using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using static Xamarin.Forms.Internals.GIFBitmap;
using Button = Xamarin.Forms.Button;

namespace CalcApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //Setting variable values on app start
            darkMode = false;
            isOperatorClicked = false;
            boolClicked = true;

        }

        //Declaring variables
        private decimal firstNummer;
        private string operatorName;
        private bool isOperatorClicked;
        private bool darkMode;
        private bool boolClicked;


        //Code to display numbers on screen
        private void BtnCommon_Clicked(object sender, System.EventArgs e)
        {
            var button = (Button)sender;
            //Checks if there's no number and if an operator was clicked
            if (CalcResult.Text == "0" || isOperatorClicked)
            {
                isOperatorClicked = false;
                //Displays clicked number
                CalcResult.Text = button.Text;
            }
            else
            //If there's already a number, add another number
            {
                CalcResult.Text += button.Text;
            }
        }

        //Code to display dot on screen
        private void BtnDot_Clicked(object sender, System.EventArgs e)
        {
            var button = (Button)sender;
            //Checks if there's no dot displayed to prevent multiple dots
            if (CalcResult.Text.Contains("."))
            {
                return;
            }

            else
            {
                //Displays clicked dot
                CalcResult.Text += button.Text;
            }
        }

        //Code to clear screen
        private void BtnAc_Clicked(object sender, System.EventArgs e)
        {
            //Sets display value to zero
            CalcResult.Text = "0";
            //Resets variables
            isOperatorClicked = false;
            firstNummer = 0;
            boolClicked = true;
        }

        //Code to check if an operator button was clicked
        private void BtnCommonOperation_Clicked(object sender, System.EventArgs e)
        {
            var button = (Button)sender;
            isOperatorClicked = true;
            operatorName = button.Text;
            firstNummer = Convert.ToDecimal(CalcResult.Text);
            boolClicked = true;
        }

        //Code to calculate formula and display result
        private void BtnEquals_Clicked(object sender, System.EventArgs e)
        {
            //Ensures only one calculation can occur at each equal button press
            if (boolClicked == true)
            {
                try
                {
                    //Calculate results
                    decimal secondNumber = Convert.ToDecimal(CalcResult.Text);
                    string finalResult = Calculate(firstNummer, secondNumber).ToString("0.##");
                    //Display results
                    CalcResult.Text = finalResult;

                }
                //Operator to throw exceptions when something goes wrong e.g division by zero
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message, "Ok");
                }
                finally
                {
                    boolClicked = false;
                }
            }
               
        }

        //Calculate function
        public decimal Calculate(decimal firstNumber, decimal secondNumber)
        {
            //Sets result to zero before calculating
            decimal result = 0;
            //Checks operator name to decide what type of calculation will be made
            if (operatorName == "+")
            {
                result = firstNumber + secondNumber;
            }
            else if (operatorName == "-")
            {
                result = firstNumber - secondNumber;
            }
            else if (operatorName == "*")
            {
                result = firstNumber * secondNumber;
            }
            else if (operatorName == "/")
            {
                result = firstNumber / secondNumber;
            }
            return result;
        }

        //Code to change operator type +/-
        private void BtnOp_Clicked(object sender, System.EventArgs e)
        {
            decimal i = Convert.ToDecimal(CalcResult.Text);
            if (CalcResult.Text != "0")
            {
                //Multiplies by -1 to change from - to + and vice versa
                string finalResult = Convert.ToString(i = i * -1);
                CalcResult.Text = finalResult;
            }


        }

        //Code to calculate percentage of number
        private async void BtnCent_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                string number = CalcResult.Text;
                if (number != "0")
                {
                    //Divides current number by 100
                    decimal percentValue = Convert.ToDecimal(number);
                    string result = (percentValue / 100).ToString("0.##");
                    CalcResult.Text = result;
                }
            }
            //Operator to throw exceptions when something goes wrong
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        //Code to toogle between normal and dark mode
        private void BtnDm_Clicked(object sender, System.EventArgs e)
        {
            //Changes background image based on current mode
            if(darkMode == false)
            {
                BackgroundImageSource = "enddark.png";
                darkMode = true;
            }
            else
            {
                BackgroundImageSource = "enddesign.png";
                darkMode = false;
            }
            
        }
    }
}
