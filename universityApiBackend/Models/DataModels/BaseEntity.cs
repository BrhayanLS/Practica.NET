using System.ComponentModel.DataAnnotations;

namespace universityApiBackend.Models.DataModels
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string UpdateBy { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; }
        public string DeleteBy {  get; set; } = string.Empty;
        public DateTime? DeleteAt { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
