using System.Text.Json.Serialization;

namespace BlazorTool.Client.Models
{
    public class Person
    {
        [JsonPropertyName("personID")]
        public int PersonId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }

        public Person() { }

        public Person(int personId, string name)
        {
            PersonId = personId;
            Name = name;
        }

       public static Person NotAssigned => new Person(0, "Not assigned");
    }
}
