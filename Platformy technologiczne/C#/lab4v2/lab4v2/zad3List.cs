using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace lab4v2
{
    public class zad3List : BindingList<Car>
    {
        bool BModel = false;
        bool BYear = false;
        bool BMotor = false;
        public zad3List(List<Car> mycars)
        {
            foreach(var Car in mycars)
            {
                this.Add(Car);
            }
        }
        //sortowanie
        //szukanie po int lub int 

        // szukanie zwraca liste pasujacych elementow

        public List<Car> find(string text, string combo)
        {
            List<Car> matchingCars = new List<Car>();

            foreach (Car car in this)
            {
                if (combo == "Model")
                {
                    if (car.Model == text)
                    {
                        matchingCars.Add(car);
                    }
                }
                else if (combo == "Year")
                {
                    if (car.Year == Int32.Parse(text))
                    {
                        matchingCars.Add(car);
                    }
                }
                else if (combo == "Motor")
                {
                    if (car.Motor.Model == text)
                    {
                        matchingCars.Add(car);
                    }
                }
            }
            return matchingCars;
        }
        public List<Car> addEL( String M, String EM, double H, double D, int Y)
        {
            List<Car> matchingCars = new List<Car>();
            foreach (Car car in this)
            {
                matchingCars.Add(car);
            }
            matchingCars.Add(new Car(M, new Engine(D, H, EM), Y));
            return matchingCars;
        }
        public List<Car> sort(String What)
        {
            List<Car> matchingCars = new List<Car>();
            foreach (Car car in this)
            {
                matchingCars.Add(car);
            }


            if(What == "Model")
            {
                BModel = !BModel;
                if (BModel) return matchingCars = matchingCars.OrderBy(car => car.Model).ToList();
                return matchingCars = matchingCars.OrderByDescending(car => car.Model).ToList();
                
            }
            else if(What == "Year")
            {
                BYear = !BYear;
                if(BYear) return matchingCars = matchingCars.OrderBy(car => car.Year).ToList();
                return matchingCars = matchingCars.OrderByDescending(car => car.Year).ToList();

            }
            else
            {
                BMotor = !BMotor;
                if(BMotor) return matchingCars = matchingCars.OrderBy(car => car.Motor.Model).ToList();
                return matchingCars = matchingCars.OrderByDescending(car => car.Motor.Model).ToList();

            }

        }


    }
}
