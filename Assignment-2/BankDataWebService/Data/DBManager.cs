using BankDataWebService.Models;
using Microsoft.EntityFrameworkCore;

namespace BankDataWebService.Data
{
    public class DBManager : DbContext
    {
        private static string connectionString = "Data Source=Bank.db;Version=3;";
        private static Random rand = new Random();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = Bank.db");
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Profile> profiles = new List<Profile>();
            List<Account> accounts = new List<Account>();
            List<Transaction> transactions = new List<Transaction>();

            for(int ii = 0; ii < 1000; ii++)
            {
                Profile profile = Factory.MakeProfile();
                Account account = Factory.MakeAccount(profile.UserID);

                for(int jj = 0; jj < rand.Next(0,5); jj++)
                {
                    transactions.Add(Factory.MakeTransaction(account.AcctNo));
                }

                profiles.Add(profile);
                accounts.Add(account);
                
            }

            modelBuilder.Entity<Profile>().HasData(profiles);
            modelBuilder.Entity<Account>().HasData(accounts);
            modelBuilder.Entity<Transaction>().HasData(transactions);
        }
    }
}