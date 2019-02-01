namespace Domain.Models
{
    public class City : IAggregateRoot
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
