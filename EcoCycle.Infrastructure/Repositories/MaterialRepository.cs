using EcoCycle.Domain.Entities;
using EcoCycle.Domain.Interfaces;
using EcoCycle.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcoCycle.Infrastructure.Repositories
{
    public class MaterialRepository : BaseRepository<Material>, IMaterialRepository
    {
        public MaterialRepository(AppDbContext context) : base(context) { }

        public async Task<Material?> GetByNombreAsync(string nombre)
        {
            return await _context.Materiales
                .FirstOrDefaultAsync(m => m.NombreMaterial == nombre);
        }
    }
}
