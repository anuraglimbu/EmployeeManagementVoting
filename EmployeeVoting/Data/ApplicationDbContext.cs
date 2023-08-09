using EmployeeVoting.Models;
using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;

namespace EmployeeVoting.Data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Department> ev_Departments { get; set; }

		public DbSet<Role> ev_Roles { get; set; }

		public DbSet<Employee> ev_Employees { get; set; }

		public DbSet<Address> ev_Addresses { get; set; }

		public DbSet<Contact> ev_Contacts { get; set; }

		public DbSet<EmployeeHistory> ev_EmployeeHistories { get; set; }

		public DbSet<Vote> ev_Votes { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			
			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}
	}
}
