using Newtonsoft.Json;

namespace EntityApp.Dal.Models
{
    public class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
            OperationDate = DateTime.Now;
        }

        [JsonConverter(typeof(GuidConverter))]
        public Guid Id { get; set; }
        public DateTime OperationDate { get; set; }
        public decimal Amount { get; set; }
    }
}
