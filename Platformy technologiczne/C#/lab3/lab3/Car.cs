using System;
using System.Collections.Generic;
using System.Text;

namespace lab3
{
    public class Car
    {
        [System.Xml.Serialization.XmlIgnore]
        public string Model { get; set; }
        public Engine Motor { get; set; }
        public int Year { get; set; }

        public Car() { }
        public Car(string model, Engine motor, int year)
        {
            Model = model;
            Motor = motor;
            Year = year;
        }
        
    }
    
}
