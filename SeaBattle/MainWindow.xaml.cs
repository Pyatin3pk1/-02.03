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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Threading;

namespace SeaBattle
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] array = new int[10, 10];//создание массив для кораблей игрока
        double hod = 0;//для счета кораблей
        double shoot = 100; // общее количество полей

        int a = 1;//для постройки кораблей

        public void obnul()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    array[i, j] = 0;
                }
            }
        }
        //построение сетки
        public void setka()
        {
            for (byte i = 0; i <= 10; i++)
            {
                //построение горизонтальные и вертикальные линии на поле игрока
                Line y = new Line();
                y.X1 = i * 40;
                y.X2 = i * 40;
                y.Y1 = 0;
                y.Y2 = 400;
                y.Stroke = Brushes.Gray;
                pole_i.Children.Add(y);
                Line x = new Line();
                x.X1 = 0;
                x.X2 = 400;
                x.Y1 = i * 40;
                x.Y2 = i * 40;
                x.Stroke = Brushes.Gray;
                pole_i.Children.Add(x);

                //строит горизонтальные линии на поле компьютера
                Line y_с = new Line();
                y_с.X1 = i * 40;
                y_с.X2 = i * 40;
                y_с.Y1 = 0;
                y_с.Y2 = 400;
                y_с.Stroke = Brushes.Black;
                pole_c.Children.Add(y_с);
                Line x_с = new Line();
                x_с.X1 = 0;
                x_с.X2 = 400;
                x_с.Y1 = i * 40;
                x_с.Y2 = i * 40;
                x_с.Stroke = Brushes.Black;
                pole_c.Children.Add(x_с);
            }
        }
        //игровой массив, где указанны координаты точки и значение точки(0,1,2,3,4 - в зависимости от наличия и типа корабля)
        public int game_zone(int x, int y, int a)
        {
            array[x, y] = a;
            return a;
        }
        //для рисования крестика
        public void krest_p(int x, int y)
        {

            Line k1 = new Line();
            k1.X1 = (x * 40);
            k1.X2 = (x * 40) + 40;
            k1.Y1 = (y * 40);
            k1.Y2 = (y * 40) + 40;
            k1.Stroke = Brushes.Red;
            k1.StrokeThickness = 2;
            pole_i.Children.Add(k1);

            Line k2 = new Line();
            k2.X1 = x * 40;
            k2.X2 = (x * 40) + 40;
            k2.Y1 = (y * 40) + 40;
            k2.Y2 = y * 40;
            k2.Stroke = Brushes.Red;
            k2.StrokeThickness = 2;
            pole_i.Children.Add(k2);
        }

        //рисование промаха 
        public void no_p(int x, int y)
        {

            Brush color = new SolidColorBrush(Colors.Red);
            Ellipse ell = new Ellipse();
            Thickness mrgn = new Thickness(x, y, 0, 0);
            ell.Margin = mrgn;
            ell.Width = 10;
            ell.Height = 10;
            ell.Fill = color;
            pole_i.Children.Add(ell);
        }

        //проверка на пересечение кораблей
        public int prov(int x, int y, int b, int vert)
        {
            int otv = 0;
            if (vert == 0)
            {
                switch (b)
                {
                    case 1: if (array[x, y] != 0) otv = 1; break;
                    case 2: if (array[x, y] != 0 || array[x + 1, y] != 0) otv = 1; break;
                    case 3: if (array[x, y] != 0 || array[x + 1, y] != 0 || array[x + 2, y] != 0) otv = 1; break;
                    case 4: if (array[x, y] != 0 || array[x + 1, y] != 0 || array[x + 2, y] != 0 || array[x + 3, y] != 0) otv = 1; break;
                    default: otv = 0; break;
                }
            }
            else
            {
                switch (b)
                {
                    case 1: if (array[x, y] != 0) otv = 1; break;
                    case 2: if (array[x, y] != 0 || array[x, y + 1] != 0) otv = 1; break;
                    case 3: if (array[x, y] != 0 || array[x, y + 1] != 0 || array[x, y + 2] != 0) otv = 1; break;
                    case 4: if (array[x, y] != 0 || array[x, y + 1] != 0 || array[x, y + 2] != 0 || array[x, y + 3] != 0) otv = 1; break;
                    default: otv = 0; break;
                }
            }
            return otv;
        }
        public void korabl(double x, double y)
        {
            Brush color = new SolidColorBrush(Colors.Black);
            Rectangle cube = new Rectangle();
            Thickness mrgn = new Thickness(x, y, 0, 0);
            cube.Margin = mrgn;
            cube.Width = 40;
            cube.Height = 40;
            cube.Fill = color;
            pole_c.Children.Add(cube);
        }
        public MainWindow()
        {
            InitializeComponent();
            setka();//вызов построоения сетки
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string prov = "";
            for (int n = 0; n < 10; n++)
            {
                for (int l = 0; l < 10; l++)
                {
                    prov = prov + " " + array[n, l];
                }
                prov = prov + "\n";
            }
            btn.Visibility = Visibility.Hidden;
            vert.Visibility = Visibility.Hidden;
        }
        //по нажатию клавиши определяем выбранную ячейку
        private void FieldOfShips_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int b = 0;//значение корабля
            Point pt = e.GetPosition(this);//функция получения координат щелчка мыши
            int x = Convert.ToInt32(pt.X), y = Convert.ToInt32(pt.Y);
            int x_k = 0, y_k = 0;

            x = (x - 40) / 40;
            y = (y - 40) / 40;

            x_k = -360 + x * 80;
            y_k = -360 + y * 80;
            if (a <= 4)
            {
                if (prov(x, y, 1, 0) == 0)
                {
                    b = 1;
                    game_zone(x, y, b);//добавлем элементы в массив, где хранятся корабли игрока
                    korabl(x_k, y_k);
                }
                else
                {
                    MessageBox.Show("Не правильно расположен корабль");
                    a--;
                }
            }
            if (a >= 5 && a < 8)
            {
                b = 2;
                if (vert.IsChecked == false)
                {
                    try
                    {
                        if (prov(x, y, 2, 0) == 0)
                        {
                            game_zone(x + 1, y, b);
                            game_zone(x, y, b);

                            korabl(x_k, y_k);
                            korabl(x_k + 80, y_k);
                        }
                        else
                        {
                            MessageBox.Show("пересечение с другим кораблем"); a--;
                        }
                    }
                    catch { MessageBox.Show("выход за пределы поля"); a--; }
                }
                else
                {
                    try
                    {
                        if (prov(x, y, 2, 1) == 0)
                        {
                            game_zone(x, y + 1, b);
                            game_zone(x, y, b);
                            korabl(x_k, y_k);
                            korabl(x_k, y_k + 80);
                        }
                        else
                        {
                            MessageBox.Show("пересечение с другим кораблем"); a--;
                        }
                    }
                    catch { MessageBox.Show("выход за пределы поля"); a--; }
                }
            }
            if (a >= 8 && a < 10)
            {
                b = 3;
                if (vert.IsChecked == false)
                {
                    try
                    {
                        if (prov(x, y, 3, 0) == 0)
                        {
                            game_zone(x + 2, y, b);
                            game_zone(x + 1, y, b);
                            game_zone(x, y, b);

                            korabl(x_k, y_k);
                            korabl(x_k + 80, y_k);
                            korabl(x_k + 160, y_k);
                        }
                        else
                        {
                            MessageBox.Show("пересечение с другим кораблем"); a--;
                        }
                    }
                    catch { MessageBox.Show("выход за пределы поля"); a--; }
                }
                else
                {
                    try
                    {
                        if (prov(x, y, 3, 1) == 0)
                        {
                            game_zone(x, y + 2, b);
                            game_zone(x, y + 1, b);
                            game_zone(x, y, b);

                            korabl(x_k, y_k);
                            korabl(x_k, y_k + 80);
                            korabl(x_k, y_k + 160);
                        }
                        else
                        {
                            MessageBox.Show("пересечение с другим кораблем"); a--;
                        }
                    }
                    catch { MessageBox.Show("выход за пределы поля"); a--; }
                }
            }
            if (a == 10)
            {
                b = 4;
                if (vert.IsChecked == false)
                {
                    try
                    {
                        if (prov(x, y, 4, 0) == 0)
                        {
                            game_zone(x + 3, y, b);
                            game_zone(x + 2, y, b);
                            game_zone(x + 1, y, b);
                            game_zone(x, y, b);

                            korabl(x_k, y_k);
                            korabl(x_k + 80, y_k);
                            korabl(x_k + 160, y_k);
                            korabl(x_k + 240, y_k);
                            MessageBox.Show("Все готово!\n Начать игру!");
                        }
                        else
                        {
                            MessageBox.Show("пересечение с другим кораблем"); a--;
                        }
                    }
                    catch { MessageBox.Show("выход за пределы поля"); a--; ; }
                }
                else
                {
                    try
                    {
                        if (prov(x, y, 4, 1) == 0)
                        {
                            game_zone(x, y + 3, b);
                            game_zone(x, y + 2, b);
                            game_zone(x, y + 1, b);
                            game_zone(x, y, b);

                            korabl(x_k, y_k);
                            korabl(x_k, y_k + 80);
                            korabl(x_k, y_k + 160);
                            korabl(x_k, y_k + 240);
                            MessageBox.Show("Все готово!\n Начать игру!");
                        }

                        else
                        {
                            MessageBox.Show("пересечение с другим кораблем"); a--;
                        }
                    }
                    catch { MessageBox.Show("выход за пределы поля"); a--; }
                }
            }
            if (a > 10) b = 0;
            a++;
        }
        //медот для замены модели корабля при полном уничтожении
        private void CheckDestroyedShip(int x, int y, int size)
        {
            var hits = new List<Point>();
            hits.Add(new Point(x, y));

            for (int i = 1; i <= size && hits.Count < size; i++)
            {
                if (x + i <= 9 && array[x + i, y] == 5)
                    hits.Add(new Point(x + i, y));
                else break;
            }

            for (int i = 1; i <= size && hits.Count < size; i++)
            {
                if (x - i >= 0 && array[x - i, y] == 5)
                    hits.Add(new Point(x - i, y));
                else break;
            }

            for (int i = 1; i <= size && hits.Count < size; i++)
            {
                if (y + i <= 9 && array[x, y + i] == 5)
                    hits.Add(new Point(x, y + i));
                else break;
            }

            for (int i = 1; i <= size && hits.Count < size; i++)
            {
                if (y - i >= 0 && array[x, y - i] == 5)
                    hits.Add(new Point(x, y - i));
                else break;
            }

            if (hits.Count == size)
            {
                for (int i = 0; i < size; i++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Fill = new SolidColorBrush(Colors.Red);
                    rectangle.Width = 40;
                    rectangle.Height = 40;
                    double x_k = -360 + hits[i].X * 80;
                    double y_k = -360 + hits[i].Y * 80;
                    rectangle.Margin = new Thickness(x_k, y_k, 0, 0);
                    pole_i.Children.Add(rectangle);
                    
                }
                MessageBox.Show($"Корабль состоящий из {hits.Count} палуб подбит");
            }
            if (true)///для дебага 
            {
                Console.Write("");
            }
        }
            //Поле стрельбы по караблям
        private void ShootingField_MouseUp(object sender, MouseButtonEventArgs e)
        {
           Point pt = e.GetPosition(this);
           int x = Convert.ToInt32(pt.X), y = Convert.ToInt32(pt.Y);
           int x_k = 0, y_k = 0;
           x = (x - 560) / 40;
           y = (y - 40) / 40;

           x_k = -360 + x * 80;
           y_k = -360 + y * 80;

            if (array[x, y] == 5)
            {
                MessageBox.Show("Уже здесь был!");
            }
            else if (array[x, y] == 6)
            {
                no_p(x_k, y_k);
                shoot--;
            }
            else if (array[x, y] == 6)
            {
                no_p(x_k, y_k);
            }
            else if (array[x, y] == 0)
            {
                no_p(x_k, y_k);
                array[x, y] = 6;
            }
            else
            {
                int size = array[x, y];
                array[x, y] = 5;
                krest_p(x, y);
                CheckDestroyedShip(x, y, size);
                hod++;
            }

            if (hod == 4)///для дебага (визуализация поля)
            {
                Console.Write("");
            }
            VerText.Text = String.Format("{0:F7}", (20 - hod) / shoot); // Подсчёт вероятности попаданя в корабль
            if (hod == 20)
            {
               MessageBox.Show("Поздравляем!  Вы победили!");
               this.Close();
            }
        }
        
    }
}
