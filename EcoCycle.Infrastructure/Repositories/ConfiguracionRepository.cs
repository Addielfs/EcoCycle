using EcoCycle.Domain.Entities;
using EcoCycle.Domain.Interfaces;
using EcoCycle.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcoCycle.Infrastructure.Repositories
{
    public class ConfiguracionRepository : BaseRepository<Configuracion>, IConfiguracionRepository
    {
        public ConfiguracionRepository(AppDbContext context) : base(context) { }

        public async Task<Configuracion?> GetByClaveAsync(string clave)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Clave == clave);
        }
    }
}
