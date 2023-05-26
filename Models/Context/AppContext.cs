using Microsoft.EntityFrameworkCore;

namespace Models.Context
{
    public class AppContext : DbContext
    {
        public AppContext() { }
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
    }
}

