using DAL.Entities;
using DAL.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ConstantRepository : IConstantRepository
{
    private readonly AppDbContext _context;

    public ConstantRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Constant> GetConstantByNameAsync(string name)
    {
        return await _context.Set<Constant>().FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task UpdateConstantAsync(Constant constant)
    {
        _context.Set<Constant>().Update(constant);
        await _context.SaveChangesAsync();
    }
}
