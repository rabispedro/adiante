using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence;

public class SqlServerDbContext : DbContext
{
	public DbSet<Empresa> Empresas { get; set; }
	public DbSet<Antecipacao> Antecipacoes { get; set; }
	public DbSet<NotaFiscal> NotasFiscais { get; set; }

	public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options) { }
}
