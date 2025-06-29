using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // Sample data
        City[] cities = {
            new City("Toronto", 100200),
            new City("Hamilton", 80923),
            new City("Ancaster", 4039),
            new City("Brantford", 500890),
        };

        Person[] persons = {
            new Person("Cedric","Coltrane","Toronto",157,null),
            new Person("Hank","Spencer","Peterborough",158,"Sulfa, Penicillin"),
            new Person("Sara","Di","29",145,null),
            new Person("Daphne","Seabright","Ancaster",146,null),
            new Person("Rick","Bennett","Ancaster",220,null),
            new Person("Amy","Leela","Hamilton",172,"Penicillin"),
            new Person("Woody","Bashir","Barrie",153,null),
            new Person("Tom", "Halliwell","Hamilton",179,"Codeine, Sulfa"),
            new Person("Rachel","Winterbourne","Hamilton",163,null),
            new Person("John","West","Oakville",138,null),
            new Person("Jon","Doggett","Hamilton",194,"Peanut Oil"),
            new Person("Angel","Edwards","Brantford",176,null),
            new Person("Brodie","Beck","Carlisle",157,null),
            new Person("Beanie","Foster","Ancaster",154,"Ragweed, Codeine"),
            new Person("Nino","Andrews","Hamilton",186,null),
            new Person("John","Farley","Hamilton",213,null),
            new Person("Nea","Kobayakawa","Toronto",147,null),
            new Person("Laura","Halliwell","Brantford",146,null),
            new Person("Lucille","Maureen","Hamilton",184,null),
            new Person("Jim","Thoma","Ottawa",173,null),
            new Person("Roderick","Payne","Halifax",58,null),
            new Person("Sam","Threep","Hamilton",199,null),
            new Person("Bertha","Crowley","Delhi",125,"Peanuts, Gluten"),
            new Person("Roland","Edge","Brantford",199,null),
            new Person("Don","Wiggum","Hamilton",189,null),
            new Person("James","Sullivan","Delhi",139,null),
            new Person("Anne","Marlowe","Pickering",165,"Peanut Oil"),
            new Person("Kelly","Hamilton","Stoney",84,null),
            new Person("Charles","Andonuts","Hamilton",62,null),
            new Person("Temple","Russert","Hamilton",166,"Sulphur"),
            new Person("Don","Edwards","Hamilton",215,null),
            new Person("Alice","Donovan","Hamilton",167,null),
            new Person("Stone","Cutting","Hamilton",110,null),
            new Person("Neil","Allan","Cambridge",203,null),
            new Person("Cross","Gordon","Ancaster",125,null),
            new Person("Phoebe","Bigelow","Thunder",183,null),
            new Person("Harry","Kuramitsu","Hamilton",210, null)
        };

        Console.WriteLine("A) Persons with height = 157 (Query):");
        var q1 = from p in persons where p.Height == 157 select p;
        foreach (var p in q1)
            Console.WriteLine($"{p.FirstName} {p.LastName}");

        Console.WriteLine("\nA) Persons with height = 157 (Method):");
        var q2 = persons.Where(p => p.Height == 157);
        foreach (var p in q2)
            Console.WriteLine($"{p.FirstName} {p.LastName}");

        Console.WriteLine("\nB) Name format: J. Doe");
        var formattedNames = from p in persons select $"{p.FirstName[0]}. {p.LastName}";
        foreach (var name in formattedNames)
            Console.WriteLine(name);

        Console.WriteLine("\nC) All distinct allergies:");
        var allAllergies = persons
            .Where(p => !string.IsNullOrEmpty(p.Allergies))
            .SelectMany(p => p.Allergies!.Split(','))
            .Select(a => a.Trim())
            .Distinct();
        foreach (var allergy in allAllergies)
            Console.WriteLine(allergy);

        Console.WriteLine("\nD) Cities starting with H:");
        var citiesH = (from c in cities where c.Name.StartsWith("H") select c).Count();
        Console.WriteLine($"Count: {citiesH}");

        Console.WriteLine("\nE) Persons in cities with population > 100000:");
        var join = from p in persons
                   join c in cities on p.CityName equals c.Name
                   where c.Population > 100000
                   select p;
        foreach (var p in join)
            Console.WriteLine($"{p.FirstName} from {p.CityName}");

        Console.WriteLine("\nF) Persons in selected cities (manual list):");
        var selectedCities = new List<string> { "Hamilton", "Ancaster", "Brantford" };
        var inCities = persons.Where(p => selectedCities.Contains(p.CityName));
        foreach (var p in inCities)
            Console.WriteLine($"{p.FirstName} from {p.CityName}");

        Console.WriteLine("\nF) Persons NOT in selected cities:");
        var notInCities = persons.Where(p => !selectedCities.Contains(p.CityName));
        foreach (var p in notInCities)
            Console.WriteLine($"{p.FirstName} from {p.CityName}");

        Console.WriteLine("\nG) Convert to XML:");
        XElement xml = new XElement("Persons",
            persons.Select(p =>
                new XElement("Person",
                    new XElement("FirstName", p.FirstName),
                    new XElement("LastName", p.LastName),
                    new XElement("City", p.CityName),
                    new XElement("Height", p.Height),
                    new XElement("Allergies", p.Allergies ?? "None")
                )
            )
        );
        Console.WriteLine(xml);
    }
}
