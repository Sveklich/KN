using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private bool isPlayerXTurn;
        private bool Game;

        public MainWindow()
        {
            InitializeComponent();
            Start1();
        }

        private void Start1()
        {
            Start.Content = "Старт";
            isPlayerXTurn = true;
            Game = true;
            InfoTextBlock.Text = "";
            foreach (var button in mainGrid.Children.OfType<Button>())
            {
                button.IsEnabled = false;
                button.Content = "";
            }

            Start.Content = "Заново";
            Start.IsEnabled = true;
        }
        private void Start_Click(object sender, RoutedEventArgs e)
                {
                    Start1();

                    foreach (var button in mainGrid.Children.OfType<Button>())
                    {
                        button.IsEnabled = true;
                    }
                }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Game)
            {
                Button button = (Button)sender;
                if (string.IsNullOrEmpty(button.Content.ToString()))
                {
                    button.Content = isPlayerXTurn ? "X" : "O";

                    if (WinCheck())
                    {
                        DisplayWin();
                    }
                    else if (IsBoardFull())
                    {
                        DisplayTie();
                    }
                    else
                    {
                        isPlayerXTurn = !isPlayerXTurn;
                        if (!isPlayerXTurn)
                            MakeAIMove();
                    }
                }
            }
        }

        private void MakeAIMove()
        {
            List<Button> emptyButtons = new List<Button>();
            foreach (var child in mainGrid.Children)
            {
                if (child is Button button && string.IsNullOrEmpty(button.Content.ToString()))
                {
                    emptyButtons.Add(button);
                }
            }

            if (emptyButtons.Count > 0)
            {
                Button randomButton = emptyButtons[new Random().Next(emptyButtons.Count)];
                randomButton.Content = "O";

                if (WinCheck())
                {
                    DisplayWin();
                }
                else if (IsBoardFull())
                {
                    DisplayTie();
                }
                else
                {
                    isPlayerXTurn = true;
                }
            }
        }

        private bool CheckLine(int index1, int index2, int index3)
                {
                    Button[] buttons = mainGrid.Children.OfType<Button>().ToArray();
                    return buttons[index1].Content.ToString() == buttons[index2].Content.ToString() &&
                           buttons[index2].Content.ToString() == buttons[index3].Content.ToString() &&
                           !string.IsNullOrEmpty(buttons[index1].Content.ToString());
                }

        private bool WinCheck()
        {
            if (CheckLine(0, 1, 2) || CheckLine(3, 4, 5) || CheckLine(6, 7, 8) ||
                CheckLine(0, 3, 6) || CheckLine(1, 4, 7) || CheckLine(2, 5, 8) ||
                CheckLine(0, 4, 8) || CheckLine(2, 4, 6))
            {
                return true;
            }
            return false;
        }

        private void DisplayWin()
        {
            InfoTextBlock.Text ="Победили" + (isPlayerXTurn ? "крестики" : "нолики");
            Game = false;
        }

        private void DisplayTie()
        {
            InfoTextBlock.Text = "Ничья!";
            Game = false;
        }

        private bool IsBoardFull()
        {
            foreach (var child in mainGrid.Children)
            {
                if (child is Button button && string.IsNullOrEmpty(button.Content.ToString()))
                {
                    return false;
                }
            }

            return true;
        }
    }
}