using Newtonsoft.Json;

namespace EpidemiologicalTrackingApi.Models
{
    /// <summary>
    /// Entity that contains the individual data
    /// </summary>
    public class Individual
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public List<string> Symptoms { get; set; } = new List<string>();
        public bool Diagnosed { get; set; }
        public DateTime? DateDiagnosed { get; set; }
    }
}
