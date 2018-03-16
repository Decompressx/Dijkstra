using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace Graph_DFS_Dijkstra_Astar_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            combo.Items.Add("Качественная");
            combo.Items.Add("Цена");
            combo.Items.Add("Расстояние");
        }
        string[] point = new string[11] { "НСК(1)", "Москва(2)", "Питер(3)", "Минск(4)", "Киев(5)", "Рим(6)", "Берлин(7)", "Варшава(8)", "Афины(9)", "Прага(10)", "Щецин(11)" };
        int[,] mass, mass2, mass3;
        string[] p = new string[11];
        Stack<int> stack = new Stack<int>();
        List<int> remember = new List<int>();
        string way = "";

        void Zapolnenie()
        {
            mass = new int[11, 11]{    {0,3761,3761,0,0,0,0,0,0,0,0 },
                                    {0,0,0,4618,0,0,0,0,0,0,0},
                                    {0,0,0,4222,0,0,0,0,0,0,0},
                                    {0,0,0,0,4678,4736,0,0,4222,0,0},
                                    {0,0,0,0,0,0,4618,0,0,0,0},
                                    {0,0,0,0,0,0,3834,0,0,0,0 },
                                    {0,0,0,0,0,0,0,4890,0,0,0 },
                                    {0,0,0,0,0,0,0,0,0,0,3996 },
                                    {0,0,0,0,0,0,0,0,0,4210,0 },
                                    {0,0,0,0,0,0,0,5000,0,0,5000 },
                                    {0,0,0,0,0,0,0,0,0,0,0 },
                                };
            mass2 = new int[11, 11]{    {0,7439,7566,0,0,0,0,0,0,0,0 },
                                    {0,0,0,3523,0,0,0,0,0,0,0},
                                    {0,0,0,3344,0,0,0,0,0,0,0},
                                    {0,0,0,0,4599,11110,0,0,8695,0,0},
                                    {0,0,0,0,0,0,11752,0,0,0,0},
                                    {0,0,0,0,0,0,3781,0,0,0,0 },
                                    {0,0,0,0,0,0,0,7154,0,0,0 },
                                    {0,0,0,0,0,0,0,0,0,0,3487 },
                                    {0,0,0,0,0,0,0,0,0,8712,0 },
                                    {0,0,0,0,0,0,0,4392,0,0,10133 },
                                    {0,0,0,0,0,0,0,0,0,0,0 },
                                };
            mass3 = new int[11, 11]{    {0,3830,3366,0,0,0,0,0,0,0,0 },
                                    {0,0,0,791,0,0,0,0,0,0,0},
                                    {0,0,0,719,0,0,0,0,0,0,0},
                                    {0,0,0,0,567,2353,0,0,2589,0,0},
                                    {0,0,0,0,0,0,1348,0,0,0,0},
                                    {0,0,0,0,0,0,1502,0,0,0,0 },
                                    {0,0,0,0,0,0,0,573,0,0,0 },
                                    {0,0,0,0,0,0,0,0,0,0,572 },
                                    {0,0,0,0,0,0,0,0,0,1966,0 },
                                    {0,0,0,0,0,0,0,676,0,0,498 },
                                    {0,0,0,0,0,0,0,0,0,0,0 },
                                };
        }

        void print(int[,] m)
        {
            textBox1.Text = "";
            textBox1.Text += "\t\t";
            foreach (int i in Enumerable.Range(0, 11))
                textBox1.Text += "(" + (i + 1) + ")\t";
            textBox1.Text += "\n";
            foreach (int i in Enumerable.Range(0, 11))
            {
                textBox1.Text += point[i] + "          \t ";
                foreach (int j in Enumerable.Range(0, 11))
                {
                    textBox1.Text += m[i, j] + "\t";
                }
                textBox1.Text += "\n";
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Zapolnenie();
            if (combo.SelectedIndex == 0)
            {
                print(mass);
                Dijkstra(mass, 0, 10);
            }
            if (combo.SelectedIndex == 1)
            {
                print(mass2);
                Dijkstra(mass2, 0, 10);
            }
            if (combo.SelectedIndex == 2)
            {
                print(mass3);
                Dijkstra(mass3, 0, 10);
            }
        }

        void Dijkstra(int[,] graph, int start, int finish)
        {
            var n = graph.GetLength(0);

            var distance = new int[n];
            for (int i = 0; i < n; i++)
            {
                distance[i] = int.MaxValue;
            }

            distance[start] = 0;

            var visited = new bool[n];
            var p = new int?[n];

            while (true)
            {
                var minDistance = int.MaxValue;
                var min = 0;
                for (int i = 0; i < n; i++)
                {
                    if (!visited[i] && minDistance > distance[i])
                    {
                        minDistance = distance[i];
                        min = i;
                    }
                }
                visited[min] = true;
                if (minDistance != int.MaxValue)
                {
                    for (int i = 0; i < n; i++)
                    {
                        if (graph[min, i] > 0)
                        {
                            var shortway = distance[min];
                            var nextway = graph[min, i];
                            var way = shortway + nextway;

                            if (way < distance[i])
                            {
                                distance[i] = way;
                                p[i] = min;
                            }
                        }
                    }
                }
                else { break; }
            }

            LinkedList<int> path = new LinkedList<int>();
            int? current = finish;
            while (current != null)
            {
                path.AddFirst(current.Value + 1);
                current = p[current.Value];
            }

            List<int> path1 = path.ToList();
            string goroda = "";
            int wayLength = 0;

            for (int i = 0; i < path.Count - 1; i++)
            {
                wayLength += graph[path1[i] - 1, path1[i + 1] - 1];
            }
            for (int i = 0; i < path.Count; i++)
            {
                goroda += point[path1[i] - 1] + "->";
            }
            string finalway = "Путь по вершинам:" + string.Join("->", path) + "\n" + "Путь по городам:" + goroda;
            textBox1.Text += finalway + "   " + "Минимальная длина:" + wayLength;
        }
    }
}
