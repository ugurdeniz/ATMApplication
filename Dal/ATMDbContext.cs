namespace Dal
{
    using Entity;
    using EntityMapping;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ATMDbContext : DbContext
    {

#if DEBUG
        public ATMDbContext() : base("server=.;database=ATMApplicationDBTEST;User ID=sa;password=159357;MultipleActiveResultSets=True")
        {
            Helper.Helpers.IsTest = true; 
        }
#else
        public ATMDbContext()
        {
            throw new Exception("Release Modda Test Dal Katmaný Kullanýlamaz!");
        }
        protected ATMDbContext(string connectionString) : base(connectionString){}
#endif

        public virtual DbSet<ATMMachine> ATMMachines { get; set; }
        public virtual DbSet<Withdrawal> Withdrawals { get; set; }
        public virtual DbSet<WithdrawalDetail> WithdrawalDetails { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new WithdrawalMapping());
            modelBuilder.Configurations.Add(new WithdrawalDetailMapping());
            base.OnModelCreating(modelBuilder);
        }
    }


}