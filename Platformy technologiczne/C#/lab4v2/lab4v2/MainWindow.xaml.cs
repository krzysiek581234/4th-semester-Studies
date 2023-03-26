using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows;


namespace lab4v2
{
    public partial class MainWindow : Window
    {
        public static List<Car> myCars;
        List<Car> tempcarList;
        BindingList<Car> myCarsBindingList;
        zad3List carList;


        public MainWindow()
        {
            InitializeComponent();
            myCars = new List<Car>()
            {
                new Car("E250", new Engine(1.8, 204, "CGI"), 2009),
                new Car("E350", new Engine(3.5, 292, "CGI"), 2009),
                new Car("A6", new Engine(2.5, 187, "FSI"), 2012),
                new Car("A6", new Engine(2.8, 220, "FSI"), 2012),
                new Car("A6", new Engine(3.0, 295, "TFSI"), 2012),
                new Car("A6", new Engine(2.0, 175, "TDI"), 2011),
                new Car("A6", new Engine(3.0, 309, "TDI"), 2011),
                new Car("S6", new Engine(4.0, 414, "TFSI"), 2012),
                new Car("S8", new Engine(4.0, 513, "TFSI"), 2012)
            };

            comboBox.Items.Add("Model");
            comboBox.Items.Add("Motor");
            comboBox.Items.Add("Year");

            BindDataToGrid(myCars);

            query_expression();
            method_based();
            dalegate();

            carList = new zad3List(myCars);

        }
        private void BindDataToGrid(List<Car> Cars)
        {
            myCarsBindingList = new BindingList<Car>(Cars);
            BindingSource carBindingSource = new BindingSource();
            carBindingSource.DataSource = myCarsBindingList;
            dataGridView1.ItemsSource = carBindingSource;
        }
        public void dalegate()
        {
            Func<Car, Car, int> arg1 = Func;
            Predicate<Car> arg2 = Predicate;
            Action<Car> arg3 = Action;
            myCars.Sort(new Comparison<Car>(arg1));
            myCars.FindAll(arg2).ForEach(arg3);

        }
        static int Func(Car car, Car b)
        {
            if (car.Motor.Horsepower > b.Motor.Horsepower)
            {
                return 1;
            }
            else if (car.Motor.Horsepower < b.Motor.Horsepower)
            {
                return -1;
            }
            return 0;
        }
        public bool Predicate(Car a)
        {
            if (a.Motor.Model == "TDI") return true;
            return false;
        }
        public void Action(Car a)
        {
            System.Windows.MessageBox.Show("2. Model: " + a.Model + " Silnik: " + a.Motor + " Rok: " + a.Year);
        }
        public void HandleKeyPress(object sender, System.Windows.Input.KeyEventArgs e)
        { }

        public void Search_Button(object sender, RoutedEventArgs e)
        {
            string tekst = searchTextBox.Text;
            string wybor = comboBox.SelectedItem.ToString();

            tempcarList = carList.find(tekst, wybor);
            BindDataToGrid(tempcarList);
        }
        public void Add_Button(object sender, RoutedEventArgs e)
        {
            string M = xModel.Text;
            string EM = xEngineModel.Text;
            string H = xHorsepower.Text;
            string D = xDisplacement.Text;
            string Y = xYear.Text;


            float h = float.Parse(H);
            float d = float.Parse(D);
            int y = int.Parse(Y);
            tempcarList = carList.addEL(M, EM, h, d, y);
            BindDataToGrid(tempcarList);
        }

        static void query_expression()
        {
            var groupedCars = from c in myCars
                              where c.Model == "A6"
                              let engineType = c.Motor.Model == "TDI" ? "diesel" : "petrol"
                              let hppl = (double)c.Motor.Horsepower / c.Motor.Displacement
                              group hppl by engineType into g
                              orderby g.Average() descending
                              select new
                              {
                                  engineType = g.Key,
                                  avgHPPL = g.Average()
                              };
            string odp = "query_expression \n";
            foreach (var e in groupedCars)
            {
                odp += e.engineType + ": " + e.avgHPPL + " \n";
            }
            System.Windows.MessageBox.Show(odp);
        }

        private static void method_based()
        {
            var projectedCars = myCars
            .Where(c => c.Model == "A6")
            .Select(c => new
            {
                engineType = c.Motor.Model == "TDI" ? "diesel" : "petrol",
                hppl = (double)c.Motor.Horsepower / c.Motor.Displacement
            });
            var groupedCars = projectedCars
                .GroupBy(c => c.engineType)
                .Select(g => new
                {
                    engineType = g.Key,
                    avgHPPL = g.Average(c => c.hppl)
                })
                .OrderByDescending(c => c.avgHPPL);
            string odp = "method-based query \n";
            foreach (var e in groupedCars)
            {
                odp += e.engineType + ": " + e.avgHPPL + " \n";
            }
            System.Windows.MessageBox.Show(odp);
        }
        public void Sort_Model(object sender, RoutedEventArgs e)
        {
            tempcarList = carList.sort("Model");
            BindDataToGrid(tempcarList);
        }
        public void Sort_Year(object sender, RoutedEventArgs e)
        {
            tempcarList = carList.sort("Year");
            BindDataToGrid(tempcarList);
        }
        public void Sort_Motor(object sender, RoutedEventArgs e)
        {
            tempcarList = carList.sort("Motor");
            BindDataToGrid(tempcarList);
        }
    }
}
