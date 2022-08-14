using System;
using System.Data;
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

namespace test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Logik core;
        public MainWindow()
        {
            InitializeComponent();
            core = new Logik();
            
        }


        public void CleanUp()
        {
            foreach (UIElement el in Lum.Children)
            {
                if (((Button)el).Background == Brushes.LightGray)
                {
                    ((Button)el).Content = "";
                }
            }
        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            var btna = sender as UIElement;

            var rowIndex = Grid.GetRow(btna);
            var columnIndex = Grid.GetColumn(btna);

            if ((string)((Button)sender).Content != "")
            {
                return;
            }
            
            

            ((Button)sender).Content = "X";
            string Name = ((Button)sender).Name;
            
            core.DoUserStep(rowIndex, columnIndex);
            var (row, column) = core.Logick();
            var children1 = new UIElement[Lum.Children.Count];
            Lum.Children.CopyTo(children1, 0);
            if (row != -1)
            {
                var btn = children1.First(x => Grid.GetRow(x) == row && Grid.GetColumn(x) == column) as Button;
                btn.Content = "0";
            }
            if (core.CheckIfUserWin())
            {
                console.Content = "you won";
                MessageBox.Show("you won");
                core.RestartGame();
                CleanUp();
            }
            if (core.CheckIfComputerWin())
            {
                console.Content = "Computer won";
                MessageBox.Show("Computer won");
                core.RestartGame();
                CleanUp();
            }
            if (core.CheckTie())
            {
                console.Content = "Tie";
                MessageBox.Show("Tie");
                core.RestartGame();
                CleanUp();
            }
        }
        public void last(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        public class Logik
        {
            public int[,] values = new int[3, 3];
            public int[,] RestartGame()
            {
                this.values = new int[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, };
                
                return values;
            }
            public void DoUserStep(int f, int s)
            {       
                values[f, s] = 1;
            }
            public bool CheckIfUserWin()
            {
                //CheckRow
                if ((values[1, 0] == 1 && values[1, 1] == 1 && values[1, 2] == 1) ||
                    (values[2, 0] == 1 && values[2, 1] == 1 && values[2, 2] == 1) ||
                    (values[0, 0] == 1 && values[0, 1] == 1 && values[0, 2] == 1))
                {
                    return true;
                }
                //CheckColumn
                else if ((values[0, 1] == 1 && values[1, 1] == 1 && values[2, 1] == 1) ||
                         (values[0, 2] == 1 && values[1, 2] == 1 && values[2, 2] == 1) ||
                         (values[0, 0] == 1 && values[1, 0] == 1 && values[2, 0] == 1))
                {
                    return true;
                }
                //CheckDiagonal
                else if ((values[0, 0] == 1 && values[1, 1] == 1 && values[2, 2] == 1) ||
                        (values[0, 2] == 1 && values[1, 1] == 1 && values[2, 0] == 1))
                {
                    return true;
                }
                return false;
            }
            public bool CheckIfComputerWin()
            {
                //CheckRow
                if ((values[1, 0] == -1 && values[1, 1] == -1 && values[1, 2] == -1) ||
                    (values[2, 0] == -1 && values[2, 1] == -1 && values[2, 2] == -1) ||
                    (values[0, 0] == -1 && values[0, 1] == -1 && values[0, 2] == -1))
                {
                    return true;
                }
                //CheckColumn
                else if ((values[0, 1] == -1 && values[1, 1] == -1 && values[2, 1] == -1) ||
                         (values[0, 2] == -1 && values[1, 2] == -1 && values[2, 2] == -1) ||
                         (values[0, 0] == -1 && values[1, 0] == -1 && values[2, 0] == -1))
                {
                    return true;
                }
                //CheckDiagonal
                else if ((values[0, 0] == -1 && values[1, 1] == -1 && values[2, 2] == -1) ||
                        (values[0, 2] == -1 && values[1, 1] == -1 && values[2, 0] == -1))
                {
                    return true;
                }
                return false;
            }

            public (int, int) ComputerHasPathToWin()
            {
                int i = 0;
                while (i <= 2)
                {
                    //check row
                    if (values[0, i] == -1 && values[1, i] == -1 && values[2, i] == 0)
                    {
                        values[2, i] = -1;
                        return (2, i);
                    }
                    else if (values[1, i] == -1 && values[2, i] == -1 && values[0, i] == 0)
                    {
                        values[0, i] = -1;
                        return (0, i);
                    }
                    else if (values[0, i] == -1 && values[2, i] == -1 && values[1, i] == 0)
                    {
                        values[1, i] = -1;
                        return (1, i);
                    }
                    i++;
                }
                i = 0;
                while (i <= 2)
                {
                    //check column
                    if (values[i, 0] == -1 && values[i, 1] == -1 && values[i, 2] == 0)
                    {
                        values[i, 2] = -1;
                        return (i, 2);
                    }
                    else if (values[i, 1] == -1 && values[i, 2] == -1 && values[i, 0] == 0)
                    {
                        values[i, 0] = -1;
                        return (i, 0);
                    }
                    else if (values[i, 0] == -1 && values[i, 2] == -1 && values[i, 1] == 0)
                    {
                        values[i, 1] = -1;
                        return (i, 1);
                    }
                    i++;
                }
                i = 0;
                while (i <= 2)
                {
                    //check diagonal
                    if (values[0, 0] == -1 && values[1, 1] == -1 && values[2, 2] == 0)
                    {
                        values[2, 2] = -1;
                        return (2, 2);
                    }
                    else if (values[1, 1] == -1 && values[2, 2] == -1 && values[0, 0] == 0)
                    {
                        values[0, 0] = -1;
                        return (0, 0);
                    }
                    else if (values[0, 0] == -1 && values[2, 2] == -1 && values[1, 1] == 0)
                    {
                        values[1, 1] = -1;
                        return (1, 1);
                    }
                    else if (values[0, 2] == -1 && values[2, 0] == -1 && values[1, 1] == 0)
                    {
                        values[1, 1] = -1;
                        return (1, 1);
                    }
                    else if (values[0, 2] == -1 && values[1, 1] == -1 && values[2, 0] == 0)
                    {
                        values[2, 0] = -1;
                        return (2, 0);
                    }
                    else if (values[2, 0] == -1 && values[1, 1] == -1 && values[0, 2] == 0)
                    {
                        values[0, 2] = -1;
                        return (0, 2);
                    }
                    i++;
                }
                return (-1, -1);
            }




            //block defence
            public (int, int) DefenceRow()
            {
                int i = 0;
                while (i <= 2)
                {
                    if (values[0, i] == 1 && values[1, i] == 1 && values[2, i] == 0)
                    {
                        values[2, i] = -1;
                        return (2, i);
                    }
                    else if (values[1, i] == 1 && values[2, i] == 1 && values[0, i] == 0)
                    {
                        values[0, i] = -1;
                        return (0, i);
                    }
                    else if (values[0, i] == 1 && values[2, i] == 1 && values[1, i] == 0)
                    {
                        values[1, i] = -1;
                        return (1, i);
                    }
                    i++;
                }
                return (-1, -1);
            }
            public (int, int) DefenceColumn()
            {
                int i = 0;
                while (i <= 2)
                {
                    if (values[i, 0] == 1 && values[i, 1] == 1 && values[i, 2] == 0)
                    {
                        values[i, 2] = -1;
                        return (i, 2);
                    }
                    else if (values[i, 1] == 1 && values[i, 2] == 1 && values[i, 0] == 0)
                    {
                        values[i, 0] = -1;
                        return (i, 0);
                    }
                    else if (values[i, 0] == 1 && values[i, 2] == 1 && values[i, 1] == 0)
                    {
                        values[i, 1] = -1;
                        return (i, 1);
                    }
                    i++;
                }
                return (-1, -1);
            }

            public (int, int) DefenceCorners()
            {
                int i = 0;

                if (values[0, 0] == 1 && values[1, 1] == 1 && values[2, 2] == 0)
                {
                    values[2, 2] = -1;
                    return (2, 2);
                }
                else if (values[1, 1] == 1 && values[2, 2] == 1 && values[0, 0] == 0)
                {
                    values[0, 0] = -1;
                    return (0, 0);
                }
                else if (values[0, 0] == 1 && values[2, 2] == 1 && values[1, 1] == 0)
                {
                    values[1, 1] = -1;
                    return (1, 1);
                }
                else if (values[0, 2] == 1 && values[2, 0] == 1 && values[1, 1] == 0)
                {
                    values[1, 1] = -1;
                    return (1, 1);
                }
                else if (values[0, 2] == 1 && values[1, 1] == 1 && values[2, 0] == 0)
                {
                    values[2, 0] = -1;
                    return (2, 0);
                }
                else if (values[2, 0] == 1 && values[1, 1] == 1 && values[0, 2] == 0)
                {
                    values[0, 2] = -1;
                    return (0, 2);
                }
                i++;

                return (-1, -1);
            }




            //FindMiddle, verification confirmed
            public (int, int) FindMiddle()
            {
                if (values[1, 1] == 0)
                {
                    values[1, 1] = -1;
                    return (1, 1);
                }
                return (-1, -1);
            }

            public (int, int) FindFreeCorners()
            {
                if (values[0, 2] == 0)
                {
                    values[0, 2] = -1;
                    return (0, 2);
                }
                else if (values[2, 0] == 0)
                {
                    values[2, 0] = -1;
                    return (2, 0);
                }
                else if (values[2, 2] == 0)
                {
                    values[2, 2] = -1;
                    return (2, 2);
                }
                else if (values[0, 0] == 0)
                {
                    values[0, 0] = -1;
                    return (0, 0);
                }
                else
                {
                    return (-1, -1);
                }
            }
            public (int, int) HasEmptyCells()
            {
                if (values[0, 1] == 0)
                {
                    values[0, 1] = -1;
                    return (0, 1);
                }
                else if (values[1, 0] == 0)
                {
                    values[1, 0] = -1;
                    return (1, 0);
                }
                else if (values[2, 1] == 0)
                {
                    values[2, 1] = -1;
                    return (2, 1);
                }
                else if (values[1, 2] == 0)
                {
                    values[1, 2] = -1;
                    return (1, 2);
                }
                else
                {
                    return (-1, -1);
                }
            }




            public (int, int) Logick()
            {
                int i;
                int j;

                (i, j) = ComputerHasPathToWin();
                if (i > -1)
                {
                    return (i, j);
                }

                (i, j) = DefenceRow();
                if (i > -1)
                {
                    return (i, j);
                }

                (i, j) = DefenceColumn();
                if (i > -1)
                {
                    return (i, j);
                }

                (i, j) = DefenceCorners();
                if (i > -1)
                {
                    return (i, j);
                }

                (i, j) = FindMiddle();
                if (i > -1)
                {
                    return (i, j);
                }

                (i, j) = FindFreeCorners();
                if (i > -1)
                {
                    return (i, j);
                }

                (i, j) = HasEmptyCells();
                if (i > -1)
                {
                    return (i, j);
                }

                return (i, j);
            }
            public bool CheckTie()
            {
                int m = 0;
                int i = 0;
                while (i <= 2)
                {
                    int j = 0;
                    while (j <= 2)
                    {
                        if (values[i, j] != 0)
                        {
                            m++;
                        }
                        
                        j++;
                    }
                    i++;
                }
                if (m == 9)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
