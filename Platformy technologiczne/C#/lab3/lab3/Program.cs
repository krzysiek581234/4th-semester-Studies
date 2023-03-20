﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;

namespace lab3
{
    class Program
    {
        private static List<Car> myCars = new List<Car>()
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
        static void Main(string[] args)
        {

            var projectedCars = myCars
                .Where(c => c.Model == "A6")
                .Select(c => new
                {
                    engineType = c.Motor.Model == "TDI" ? "diesel" : "petrol",
                    hppl = (double)c.Motor.Horsepower / c.Motor.Displacement
                });
            var groupedCars = projectedCars.GroupBy(c => c.engineType).OrderBy(g => g.Key);

            foreach (var group in groupedCars)
            {
                double avgHppl = group.Average(c => c.hppl);
                Console.WriteLine($"{group.Key}: {string.Join(", ", group.Select(c => c.hppl))} (avg: {avgHppl})");
            }
            var fileName = "CarsCollection.xml";
            var currentDirectory = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(currentDirectory, fileName);
            serialize(filePath);
            deserialize(filePath);


        }
        private static void LinqSerialization()
        {
            IEnumerable<XElement> nodes = myCars
                .Select(n =>
                new XElement("car",
                    new XElement("model", n.Model),
                    new XElement("engine",
                        new XAttribute("model", n.Motor.Model),
                        new XElement("displacement", n.Motor.Displacement),
                        new XElement("horsePower", n.Motor.Horsepower)),
                    new XElement("year", n.Year)));
            XElement rootNode = new XElement("cars", nodes);
            rootNode.Save("CarsCollectionLinq.xml");
        }
        private static void serialize(String fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Car>), new XmlRootAttribute("cars"));
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, myCars);
            }
        }
        private static void deserialize(String fileName)
        {
            List<Car> list = new List<Car>();
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Car>), new XmlRootAttribute("cars"));
            using (Stream reader = new FileStream(fileName, FileMode.Open))
            {
                list = (List<Car>)deserializer.Deserialize(reader);
            }
        }

        private static void XPathStatements()
        {
            XElement rootNode = XElement.Load("CarsCollection.xml");
            var countAvarageXPath = "sum(//car/engine[@model!=\"TDI\"]/horsePower) div count(//car/engine[@model!=\"TDI\"]/horsePower)";
            Console.WriteLine($"Średnia: {(double)rootNode.XPathEvaluate(countAvarageXPath)}");


            var notduplice = "//car/model[not(. = preceding::car/model)]";
            var removeDuplicatesXPath = "//car[following-sibling::car/model = model]";
            IEnumerable<XElement> models = rootNode.XPathSelectElements(removeDuplicatesXPath);

            var fileName = "CarsCollectionNoRepeats.xml";
            var currentDirectory = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(currentDirectory, fileName);
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var model in models)
                {
                    writer.WriteLine(model);
                }
            }
        }
    }
}
