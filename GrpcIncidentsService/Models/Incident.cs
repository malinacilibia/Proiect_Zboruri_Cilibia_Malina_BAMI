using System.ComponentModel.DataAnnotations;

namespace GrpcIncidentsService.Models
{
    public class Incident
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsResolved { get; set; }
    }
}