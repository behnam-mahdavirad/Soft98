using Microsoft.EntityFrameworkCore;
using Soft98.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soft98.DataAccessLayer.Context
{
    public class Soft98Context : DbContext
    {
        public Soft98Context(DbContextOptions<Soft98Context> options) : base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    } // end class Soft98Context

} // end namespace Soft98.DataAccessLayer.Context
