using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcIncidentsService.Data;
using Microsoft.EntityFrameworkCore;

namespace GrpcIncidentsService.Services
{
    public class IncidentService : GrpcIncidentsService.IncidentService.IncidentServiceBase
    {
        private readonly IncidentContext _context;

        public IncidentService(IncidentContext context)
        {
            _context = context;
        }

        public override async Task<IncidentList> GetAll(Empty request, ServerCallContext context)
        {
            var response = new IncidentList();

            var incidentsDb = await _context.Incidents.ToListAsync();

            foreach (var inc in incidentsDb)
            {
                response.Item.Add(new GrpcIncidentsService.Incident
                {
                    Id = inc.Id,
                    Title = inc.Title,
                    Description = inc.Description,
                    IsResolved = inc.IsResolved
                });
            }

            return response;
        }

        public override async Task<GrpcIncidentsService.Incident> Get(IncidentId request, ServerCallContext context)
        {
            var inc = await _context.Incidents.FindAsync(request.Id);

            if (inc == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Incident not found"));

            return new GrpcIncidentsService.Incident
            {
                Id = inc.Id,
                Title = inc.Title,
                Description = inc.Description,
                IsResolved = inc.IsResolved
            };
        }

        public override async Task<Empty> Insert(GrpcIncidentsService.Incident request, ServerCallContext context)
        {
            var newIncident = new GrpcIncidentsService.Models.Incident
            {
                Title = request.Title,
                Description = request.Description,
                IsResolved = request.IsResolved
            };

            _context.Incidents.Add(newIncident);
            await _context.SaveChangesAsync();

            return new Empty();
        }

        public override async Task<GrpcIncidentsService.Incident> Update(GrpcIncidentsService.Incident request, ServerCallContext context)
        {
            var existing = await _context.Incidents.FindAsync(request.Id);

            if (existing == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Incident not found"));

            existing.Title = request.Title;
            existing.Description = request.Description;
            existing.IsResolved = request.IsResolved;

            await _context.SaveChangesAsync();

            return request;
        }

        public override async Task<Empty> Delete(IncidentId request, ServerCallContext context)
        {
            var existing = await _context.Incidents.FindAsync(request.Id);
            if (existing != null)
            {
                _context.Incidents.Remove(existing);
                await _context.SaveChangesAsync();
            }

            return new Empty();
        }
    }
}