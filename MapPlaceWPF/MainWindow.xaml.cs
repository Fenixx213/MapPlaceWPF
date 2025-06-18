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
        private const int MN = 9;
        private const int n2 = 250;
        private List<string> objectData = new List<string>
{
    ("Палатка"),
    ("Флаг"),
    ("Дом"),
    ("Озеро"),
    ("Колодец"),
    ("Дуб"),
    ("Куст"),
    ("Вышка")
};

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
            private void DrawSingleObject(int index)
            {
                if (index < 0 || index >= objectCoordinates.Count) return;

                Point point = objectCoordinates[index];
                double x = point.X + 10;  // правее
                double y = point.Y - 20;  // выше

                
           

                switch (index)
                {
                case 0: // Палатка
                    var shape = new Polygon
                    {
                        Fill = Brushes.Black,
                        Stroke = Brushes.Black,
                        Points = new PointCollection
                            {
                                new Point(0, -5),     // верх
                                new Point(-5, 5),    // низ слева
                                new Point(5, 5)      // низ справа
                            }
                    };
                    Canvas.SetLeft(shape, x);  // Center horizontally
                    Canvas.SetTop(shape, y+10);   // Center vertically
                    var shape1 = new Polygon
                    {
                        Fill = Brushes.DarkGreen,
                        Stroke = Brushes.Black,
                        Points = new PointCollection
                            {
                                new Point(0, -10),     // верх
                                new Point(-10, 10),    // низ слева
                                new Point(10, 10)      // низ справа
                            }
                    };
                    Canvas.SetLeft(shape1, x);  // Center horizontally
                    Canvas.SetTop(shape1, y + 5);   // Center vertically
                   
                    MapCanvas.Children.Add(shape1);
                    MapCanvas.Children.Add(shape);
                    break;


                case 1: // Пираты (флаг)
                        var flagpole = new Rectangle
                        {
                            Width = 3,
                            Height = 20,
                            Fill = Brushes.Black
                        };
                        Canvas.SetLeft(flagpole, x);
                        Canvas.SetTop(flagpole, y);
                        var flag = new Polygon
                        {
                            Fill = Brushes.Black,
                            Points = new PointCollection
                    {
                        new Point(x + 3, y),
                        new Point(x + 15, y + 5),
                        new Point(x + 3, y + 10)
                    }
                        };
                        MapCanvas.Children.Add(flagpole);
                        MapCanvas.Children.Add(flag);
                        return;

                    case 2: // Дом
                        var houseBase = new Rectangle
                        {
                            Width = 20,
                            Height = 20,
                            Fill = Brushes.LightGreen,
                            Stroke = Brushes.Black
                        };
                        Canvas.SetLeft(houseBase, x);
                        Canvas.SetTop(houseBase, y);
                    var housewindow = new Rectangle
                    {
                        Width = 10,
                        Height = 10,
                        Fill = Brushes.LightBlue,
                        Stroke = Brushes.Black
                    };
                    var window1= new Line
                    {
                        X1 = x +5,
                        Y1 = y + 10,
                        X2 = x + 15,
                        Y2 = y +10,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1
                    };
                    var window2 = new Line
                    {
                        X1 = x +10,
                        Y1 = y + 5,
                        X2 = x + 10,
                        Y2 = y +15,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1
                    };
                    Canvas.SetLeft(housewindow, x+5);
                    Canvas.SetTop(housewindow, y+5);
                    var roof = new Polygon
                        {
                            Fill = Brushes.Green,
                            Stroke = Brushes.Black,
                            Points = new PointCollection
                    {
                        new Point(x, y),
                        new Point(x + 10, y - 10),
                        new Point(x + 20, y)
                    }
                        };
                        MapCanvas.Children.Add(houseBase);
                    MapCanvas.Children.Add(housewindow);
                    MapCanvas.Children.Add(window1);
                    MapCanvas.Children.Add(window2);
                    MapCanvas.Children.Add(roof);
                        return;

                    case 3: // Родник (крестик-капля)
                        var spring = new Ellipse
                        {
                            Width = 20,
                            Height = 15,
                            Fill = Brushes.LightBlue,
                            Stroke = Brushes.Blue
                        };

                        Canvas.SetLeft(spring, x);
                        Canvas.SetTop(spring, y);
                        MapCanvas.Children.Add(spring);
                        return;

                    case 4: // Колодец (цилиндр с ручкой)
                            // Перекладина (горизонтальная ручка)
                        var handle = new Line
                        {
                            X1 = x - 6,
                            Y1 = y - 2,
                            X2 = x + 6,
                            Y2 = y - 2,
                            Stroke = Brushes.SaddleBrown,
                            StrokeThickness = 1
                        };
                        var lefthandle = new Line
                        {
                            X1 = x - 4,
                            Y1 = y - 2,
                            X2 = x - 4,
                            Y2 = y + 2,
                            Stroke = Brushes.SaddleBrown,
                            StrokeThickness = 1
                        };
                        var righthandle = new Line
                        {
                            X1 = x + 4,
                            Y1 = y - 2,
                            X2 = x + 4,
                            Y2 = y +2,
                            Stroke = Brushes.SaddleBrown,
                            StrokeThickness = 1
                        };
                        // Барабан ручки (сбоку)
                        var handleKnob = new Line
                        {
                            X1 = x + 6,
                            Y1 = y - 3,
                            X2 = x + 6,
                            Y2 = y + 1,
                            Stroke = Brushes.Black,
                            StrokeThickness = 1
                        };
                        // Верх колодца
                        var topOval = new Ellipse
                        {
                            Width = 10,
                            Height = 4,
                            Fill = Brushes.Gray,
                            Stroke = Brushes.Black
                        };
                        Canvas.SetLeft(topOval, x - 5);
                        Canvas.SetTop(topOval, y);

                        // Тело цилиндра
                        var body = new Rectangle
                        {
                            Width = 10,
                            Height = 10,
                            Fill = Brushes.DarkGray,
                            Stroke = Brushes.Black
                        };
                        Canvas.SetLeft(body, x - 5);
                        Canvas.SetTop(body, y + 2);

                        // Нижний овал
                        var bottomOval = new Ellipse
                        {
                            Width = 10,
                            Height = 4,
                            Fill = Brushes.Gray,
                            Stroke = Brushes.Black
                        };
                        Canvas.SetLeft(bottomOval, x - 5);
                        Canvas.SetTop(bottomOval, y + 10);


                        MapCanvas.Children.Add(handle);
                        MapCanvas.Children.Add(handleKnob);
                        MapCanvas.Children.Add(lefthandle); MapCanvas.Children.Add(righthandle);
                        MapCanvas.Children.Add(body);
                        MapCanvas.Children.Add(topOval);
                        MapCanvas.Children.Add(bottomOval);
                   
                        return;



                    case 5: // Дуб
                        var crown = new Ellipse
                        {
                            Width = 20,
                            Height = 15,
                            Fill = Brushes.Green,
                            Stroke = Brushes.Black
                        };
                        Canvas.SetLeft(crown, x);
                        Canvas.SetTop(crown, y);
                        var trunk = new Rectangle
                        {
                            Width = 4,
                            Height = 10,
                            Fill = Brushes.Brown
                        };
                        Canvas.SetLeft(trunk, x + 8);
                        Canvas.SetTop(trunk, y + 15);
                        MapCanvas.Children.Add(crown);
                        MapCanvas.Children.Add(trunk);
                        return;

                    case 6: // Куст
                        {
                            // Центральный эллипс
                            var center = new Rectangle
                            {
                                Width = 11,
                                Height = 10,
                                Fill = Brushes.LimeGreen
                        
                            };
                            Canvas.SetLeft(center, x);
                            Canvas.SetTop(center, y + 2);

                            // Левый эллипс
                            var left = new Ellipse
                            {
                                Width = 8,
                                Height = 7,
                                Fill = Brushes.LimeGreen
                           
                            };
                            Canvas.SetLeft(left, x - 5);
                            Canvas.SetTop(left, y + 5);

                            // Правый эллипс
                            var right = new Ellipse
                            {
                                Width = 8,
                                Height = 7,
                                Fill = Brushes.LimeGreen
                     
                            };
                            Canvas.SetLeft(right, x + 7);
                            Canvas.SetTop(right, y + 5);

                            // Верхний левый эллипс
                            var topLeft = new Ellipse
                            {
                                Width = 6,
                                Height = 5,
                                Fill = Brushes.LimeGreen
                       
                            };
                            Canvas.SetLeft(topLeft, x - 1);
                            Canvas.SetTop(topLeft, y);
                            var topup = new Ellipse
                            {
                                Width = 6,
                                Height = 5,
                                Fill = Brushes.LimeGreen
                          
                            };
                            Canvas.SetLeft(topup, x+ 3);
                            Canvas.SetTop(topup, y);
                            var toplup = new Ellipse
                            {
                                Width = 8,
                                Height = 6,
                                Fill = Brushes.LimeGreen

                            };
                            Canvas.SetLeft(toplup, x -4);
                            Canvas.SetTop(toplup, y+2);
                            var toprup = new Ellipse
                            {
                                Width = 8,
                                Height = 6,
                                Fill = Brushes.LimeGreen

                            };
                            Canvas.SetLeft(toprup, x +7);
                            Canvas.SetTop(toprup, y +2);
                            // Верхний правый эллипс
                            var topRight = new Ellipse
                            {
                                Width = 6,
                                Height = 5,
                                Fill = Brushes.LimeGreen
                           
                            };
                            Canvas.SetLeft(topRight, x + 8);
                            Canvas.SetTop(topRight, y);

                            // Добавляем все эллипсы на Canvas
                            MapCanvas.Children.Add(center);
                            MapCanvas.Children.Add(left);
                            MapCanvas.Children.Add(topLeft);
                            MapCanvas.Children.Add(topup);
                            MapCanvas.Children.Add(toplup);
                            MapCanvas.Children.Add(toprup);
                            MapCanvas.Children.Add(topRight);
                            MapCanvas.Children.Add(right);
                       
                   
                            return;
                        }


                case 7: // Вышка
                    var towerBase = new Rectangle
                    {
                        Width = 7,
                        Height = 5,
                        Fill = Brushes.Green,
                        Stroke = Brushes.Black
                    };
                    Canvas.SetLeft(towerBase, x - 2);
                    Canvas.SetTop(towerBase, y + 3);
                    var towerBase2 = new Polygon
                    {
                        Fill = Brushes.DarkGreen,
                        Stroke = Brushes.Black,
                        Points = new PointCollection
                {
                    new Point(x + 4, y + 3),
                    new Point(x + 4, y + 8),
                    new Point(x + 9, y + 6),
                    new Point(x + 9, y +2)
                }
                    };
                    var roofTop = new Polygon
                    {
                        Fill = Brushes.DarkGreen,
                        Stroke = Brushes.Black,
                        Points = new PointCollection
                {
                    new Point(x - 2, y+1),
                    new Point(x + 4, y+1),
                    new Point(x + 2, y - 4)
                }
                    };
                    var roofTop2 = new Polygon
                    {
                        Fill = Brushes.DarkGreen,
                        Stroke = Brushes.Black,
                        Points = new PointCollection
                {
                    new Point(x + 4, y+1),
                    new Point(x + 2, y - 4),
                    new Point(x + 9, y)
                }
                    };
                    var rightUpLeg = new Line
                    {
                        X1 = x+9,
                        Y1 = y,
                        X2 = x+9,
                        Y2 = y +12,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1
                    };
                    var rightDownLeg = new Line
                    {
                        X1 = x -2,
                        Y1 = y,
                        X2 = x -2,
                        Y2 = y + 14,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1
                    };
                    var leftDownLeg = new Line
                    {
                        X1 = x + 5,
                        Y1 = y+1,
                        X2 = x + 5,
                        Y2 = y + 14,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1
                    };
                    MapCanvas.Children.Add(towerBase);
                    MapCanvas.Children.Add(towerBase2);
                    MapCanvas.Children.Add(roofTop2);
                    MapCanvas.Children.Add(roofTop);
                    MapCanvas.Children.Add(rightUpLeg);
                    MapCanvas.Children.Add(rightDownLeg);
                    MapCanvas.Children.Add(leftDownLeg);
                    return;

                default:
                        return;
                }

                // Общее добавление для простых фигур
            
               
            }


        private void GenerateCoordinates()
        {
            objectCoordinates.Clear();
            userCoordinates.Clear();
            ListBoxCoordinates.Items.Clear();
            MapCanvas.Children.Clear();

            DrawGrid();

            for (int i = 0; i < objectData.Count; i++)
            {
                int x = random.Next(-MN, MN);
                int y = random.Next(-MN, MN);
                Point pt = new Point(xmath(x), ymath(y));
                objectCoordinates.Add(pt);

                // Используем название из objectData вместо "Объект i"
                ListBoxCoordinates.Items.Add($"{objectData[i]}: ({x}, {y})");
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
                DrawSingleObject(selectedIndex);
                ListBoxCoordinates.Items[selectedIndex] = $"{ListBoxCoordinates.Items[selectedIndex]} (Правильно)";

                // 👇 Добавь отрисовку объекта
                DrawSingleObject(selectedIndex);

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