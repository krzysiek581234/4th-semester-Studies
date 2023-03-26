using System;
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

            first();
            var fileName = "CarsCollection.xml";
            serialize(fileName);
            foreach (var x in myCars)
            {
                Console.WriteLine($"Year: {x.Year}, Motor Model: {x.Motor.Model}, Horsepower: {x.Motor.Horsepower}, Displacement: {x.Motor.Displacement}");
            }
            myCars = deserialize(fileName);

            Console.WriteLine();
            Console.WriteLine();
            foreach (var x in myCars)
            {
                Console.WriteLine($"Year: {x.Year}, Motor Model: {x.Motor.Model}, Horsepower: {x.Motor.Horsepower}, Displacement: {x.Motor.Displacement}");
            }
            Xpath(fileName);
            LinqSerialization();
            MyCarsToXHTMLTable();
            ModifyCarsCollectionXML();
        }
        private static void first()
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
        }
        private static void serialize(String fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Car>), new XmlRootAttribute("cars"));
            var currentDirectory = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(currentDirectory, fileName);
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, myCars);
            }
        }
        private static List<Car> deserialize(String fileName)
        {
            List<Car> list;
            XmlSerializer serializer = new XmlSerializer(typeof(List<Car>), new XmlRootAttribute("cars"));
            using (Stream reader = new FileStream(fileName, FileMode.Open))
            {
                list = (List<Car>)serializer.Deserialize(reader);
            }
            return list;
        }
        private static void Xpath(String fileName)
        {
            XElement rootNode = XElement.Load(fileName);
            var countAvarageXPath = "sum(//car/engine[@Model!=\"TDI\"]/Horsepower) div count(//car/engine[@Model!=\"TDI\"]/Horsepower)";
            Console.WriteLine($"a: {(double)rootNode.XPathEvaluate(countAvarageXPath)}");

            var notduplice = "//car/engine[@Model and not(@Model = preceding::car/engine/@Model)]";
            IEnumerable<XElement> models = rootNode.XPathSelectElements(notduplice);

            foreach(var model in models)
            {
                Console.WriteLine(model.Attribute("Model").Value);
            }
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
        private static void MyCarsToXHTMLTable()
        {
            IEnumerable<XElement> rows =
                from car in myCars
                select new XElement("tr",
                    new XAttribute("style", "border: 2px solid black"),
                    new XElement("td", new XAttribute("style", "border: 2px double black"), car.Model),
                    new XElement("td", new XAttribute("style", "border: 2px double black"), car.Motor.Model),
                    new XElement("td", new XAttribute("style", "border: 2px double black"), car.Motor.Displacement),
                    new XElement("td", new XAttribute("style", "border: 2px double black"), car.Motor.Horsepower),
                    new XElement("td", new XAttribute("style", "border: 2px double black"), car.Year)
                );

            XElement table = new XElement("table",
                new XAttribute("style", "border: 2px double black"),
                rows
            );

            XElement template = XElement.Load("template.html");
            XElement body = template.Element("{http://www.w3.org/1999/xhtml}body");
            body.Add(table);
            template.Save("templateDone.html");

        }

        private static void ModifyCarsCollectionXML()
        {
            XDocument doc = XDocument.Load("CarsCollection.xml");

            foreach (XElement car in doc.Root.Elements())
            {
                foreach (XElement field in car.Elements())
                {
                    if (field.Name == "engine")
                    {
                        foreach (XElement engineElement in field.Elements())
                        {
                            if (engineElement.Name == "Horsepower")
                            {
                                engineElement.Name = "hp";
                            }
                        }
                    }
                    else if (field.Name == "Model")
                    {
                        var yearField = car.Element("Year");
                        XAttribute attribute = new XAttribute("Year", yearField.Value);
                        field.Add(attribute);
                        yearField.Remove();
                    }
                }
            }

            doc.Save("CarsCollectionModified.xml");
        }

    }
}
