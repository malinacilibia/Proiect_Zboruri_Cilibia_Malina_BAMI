using Microsoft.EntityFrameworkCore;

namespace GrpcIncidentsService.Data
{
    public class IncidentContext : DbContext
    {
        public IncidentContext(DbContextOptions<IncidentContext> options) : base(options)
        {
        }

        public DbSet<GrpcIncidentsService.Models.Incident> Incidents { get; set; }
    }
}