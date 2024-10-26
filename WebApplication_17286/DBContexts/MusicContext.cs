using Microsoft.EntityFrameworkCore;
using WebApplication_17286.Models;

namespace WebApplication_17286.DBContexts
{
    public class MusicContext : DbContext
    {
    // Constructors
    public MusicContext(DbContextOptions<MusicContext> options) : base(options) { }
    public DbSet<Song> Songs { get; set; }
    public DbSet<Performer> Performers { get; set; }
    }
}