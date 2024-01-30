using Microsoft.EntityFrameworkCore;
using VehicleTracking.Core.Web.API.Models;

namespace VehicleTracking.Core.Web.API.DataAccess
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        { }
        public DbSet<Vehicle> Vehicles {get; set;}









    }
}
