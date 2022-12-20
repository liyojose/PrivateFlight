using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PrivateFlight.Data;

public partial class InMemoeryPrivateFlightContext : DbContext
{
    protected override void OnConfiguring
      (DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "PrivateFlight");
    }
    public virtual DbSet<Message> Messages { get; set; }
}
