using System;
using System.Collections.Generic;
using System.Text;

namespace lab3
{
    public class Engine
    {
        public string Model { get; set; }
        public double Horsepower { get; set; }
        public double Displacement { get; set; }

        public Engine(double displacement, double horsepower, string model)
        {
            Model = model;
            Horsepower = horsepower;
            Displacement = displacement;
        }
    }


}
