public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CityName { get; set; }
    public int Height { get; set; }
    public string? Allergies { get; set; }

    public Person(string first, string last, string city, int height, string? allergies)
    {
        FirstName = first;
        LastName = last;
        CityName = city;
        Height = height;
        Allergies = allergies;
    }
}
