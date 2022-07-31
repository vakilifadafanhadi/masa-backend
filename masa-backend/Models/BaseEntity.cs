using System.ComponentModel.DataAnnotations.Schema;

namespace masa_backend.Models
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? RemoveAt { get; set; }
        public BaseEntity()
        {
            if (Id == Guid.Empty)
                Id = Guid.NewGuid();
            CreateAt = DateTime.Now;
        }
    }
}
