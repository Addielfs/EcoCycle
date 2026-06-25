using EcoCycle.Domain.Entities;
using EcoCycle.Domain.Interfaces;
using EcoCycle.Infrastructure.Data;

namespace EcoCycle.Infrastructure.Repositories
{
    public class CentroReciclajeRepository : BaseRepository<CentroReciclaje>, ICentroReciclajeRepository
    {
        public CentroReciclajeRepository(AppDbContext context) : base(context) { }
    }
}
