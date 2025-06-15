using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MapPlaceWPF
{
    public partial class MapGame : Window
    {
        private const int CanvasSize = 500;
        private const int GridStep = 25;
        private const int MN = 10;
        private const int n2 = 250;

        private List<Point> objectCoordinates = new List<Point>();
        private List<Point> userCoordinates = new List<Point>();
        private Random random = new Random();

        public MapGame()
        {
            InitializeComponent();
            GenerateCoordinates();
            DrawGrid();
        }

        private int xmath(int num) => n2 + GridStep * num;
        private int ymath(int num) => n2 - GridStep * num;

        private void GenerateCoordinates()
        {
            objectCoordinates.Clear();
            userCoordinates.Clear();
            ListBoxCoordinates.Items.Clear();
            MapCanvas.Children.Clear();

            DrawGrid();

            for (int i = 0; i < 8; i++)
            {
                int x = random.Next(-MN, MN);
                int y = random.Next(-MN, MN);
                Point pt = new Point(xmath(x), ymath(y));
                objectCoordinates.Add(pt);
                ListBoxCoordinates.Items.Add($"Объект {i + 1}: ({x}, {y})");
            }
        }

        private void DrawGrid()
        {
            // Draw grid lines
            for (int i = 0; i <= CanvasSize; i += GridStep)
            {
                var hLine = new Line
                {
                    X1 = 0,
                    Y1 = i,
                    X2 = CanvasSize,
                    Y2 = i,
                    Stroke = Brushes.LightBlue
                };
                MapCanvas.Children.Add(hLine);

                var vLine = new Line
                {
                    X1 = i,
                    Y1 = 0,
                    X2 = i,
                    Y2 = CanvasSize,
                    Stroke = Brushes.LightBlue
                };
                MapCanvas.Children.Add(vLine);
            }

            // Draw axes
            var xAxis = new Line
            {
                X1 = 0,
                Y1 = CanvasSize / 2,
                X2 = CanvasSize,
                Y2 = CanvasSize / 2,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            MapCanvas.Children.Add(xAxis);

            var yAxis = new Line
            {
                X1 = CanvasSize / 2,
                Y1 = 0,
                X2 = CanvasSize / 2,
                Y2 = CanvasSize,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            MapCanvas.Children.Add(yAxis);
        }

        private void DrawUserPoints()
        {
            // Remove previous user points
            for (int i = MapCanvas.Children.Count - 1; i >= 0; i--)
            {
                if (MapCanvas.Children[i] is Ellipse ellipse && ellipse.Fill == Brushes.Red)
                {
                    MapCanvas.Children.RemoveAt(i);
                }
            }

            foreach (var point in userCoordinates)
            {
                Ellipse ellipse = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Fill = Brushes.Red
                };
                Canvas.SetLeft(ellipse, point.X - 5);
                Canvas.SetTop(ellipse, point.Y - 5);
                MapCanvas.Children.Add(ellipse);
            }
        }

        private void MapCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int selectedIndex = ListBoxCoordinates.SelectedIndex;
            if (selectedIndex == -1) return;

            Point clickPoint = e.GetPosition(MapCanvas);
            Point targetPoint = objectCoordinates[selectedIndex];

            if (Math.Abs(clickPoint.X - targetPoint.X) < 10 && Math.Abs(clickPoint.Y - targetPoint.Y) < 10)
            {
                MessageBox.Show("Верно!");
                userCoordinates.Add(targetPoint);
                ListBoxCoordinates.Items[selectedIndex] = $"{ListBoxCoordinates.Items[selectedIndex]} (Правильно)";
                ListBoxCoordinates.SelectedIndex = -1;
                SelectNextUnplacedObject();
                DrawUserPoints();
            }
            else
            {
                MessageBox.Show("Ошибка, неверное расположение!!!");
            }
        }

        private void SelectNextUnplacedObject()
        {
            for (int i = 0; i < ListBoxCoordinates.Items.Count; i++)
            {
                if (!ListBoxCoordinates.Items[i].ToString().Contains("(Правильно)"))
                {
                    ListBoxCoordinates.SelectedIndex = i;
                    return;
                }
            }
            MessageBox.Show("Все точки расставлены");
        }

        private void ButtonGenerate_Click(object sender, RoutedEventArgs e)
        {
            GenerateCoordinates();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}