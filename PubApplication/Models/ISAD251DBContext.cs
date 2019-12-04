using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PubApplication.Models.Enum;
using PubApplication.ViewModels;
using System.Data;
using PubApplication.ViewModels.StoredProcedureViewModels;

namespace PubApplication.Models
{
    public partial class ISAD251DBContext : DbContext
    {
        public ISAD251DBContext()
        {
        }

        public ISAD251DBContext(DbContextOptions<ISAD251DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PubItems> PubItems { get; set; }
        public virtual DbSet<PubOrderItems> PubOrderItems { get; set; }
        public virtual DbSet<PubOrders> PubOrders { get; set; }
        public virtual DbSet<PubUsers> PubUsers { get; set; }

        //EXAMPLE
        //public virtual DbSet<addPubUserViewModel> AddPubUserResults { get; set; }


        public virtual DbSet<Get_PubUserViewModel> GetPubUserResults { get; set; }

        //public virtual DbSet<Get_PubUserPasswordViewModel> GetPubUserPasswordResults { get; set; }

        public int AddPubUser(PubUsers model)
        {
            SqlParameter @outputParam = new SqlParameter {ParameterName = "@outputParam", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output};

            Database.ExecuteSqlRaw("EXEC @outputParam=Add_PubUser @UserFirstName, @UserLastName, @UserAccessRank, @UserPassword", 
                    outputParam,
                    new SqlParameter("@UserFirstName", model.UserFirstName.ToString()),
                    new SqlParameter("@UserLastName", model.UserLastName.ToString()),
                    new SqlParameter("@UserAccessRank", model.UserAccessRank.ToString()),
                    new SqlParameter("@UserPassword", model.UserPassword.ToString()));
            int result = (int)@outputParam.Value; //User ID is returned. Can return the value from the stored procedure as User ID is an integer.
            return result;
        }

        public PubUsers GetPubUser(int UserId)
        {
            var results = GetPubUserResults.FromSqlRaw("EXEC Get_PubUser @UserID", new SqlParameter("@UserID", UserId)).ToList();

            System.Diagnostics.Debug.WriteLine(results.First().UserID.ToString());
            if (results.Count() > 0)
            {
                PubUsers output = new PubUsers { UserId = results.First().UserID, 
                    UserPassword = results.First().UserPassword, 
                    UserFirstName = results.First().UserFirstName, 
                    UserLastName = results.First().UserLastName, 
                    UserAccessRank = UserRank.GetRank(results.First().UserAccessRank) }; 
                //If results are returned then the user has been found. As the user is unique, their will only be one result so the password is fetched from the first (only) result.
                return output;
            }
            else
            {
                return null; //no results returned - user ID dosen't exist in DB so give nothing.
            }
        }

        //public string GetPubUserPassword(int UserId)
        //{
        //    var results = GetPubUserPasswordResults.FromSqlRaw("EXEC Get_PubUserPassword @UserID",
        //            new SqlParameter("@UserID", UserId));
        //    if (results.Count() > 0)
        //    {
        //        string output = results.First().UserPassword.ToString(); //If results are returned then the user has been found. As the user is unique, their will only be one result so the password is fetched from the first (only) result.
        //        return output;
        //    }
        //    else
        //    {
        //        return null; //no results returned - user ID dosen't exist in DB so give nothing.
        //    }  
        //}

        //public string Add_PubUser(RegisterViewModel model)
        //{
        //    var results = Add_PubUserResults.FromSqlRaw("EXEC Add_PubUser @UserFirstName, @UserLastName, @UserAccessRank, @UserPassword",
        //            new SqlParameter("@UserFirstName", model.UserFirstName.ToString()),
        //            new SqlParameter("@UserLastName", model.UserLastName.ToString()),
        //            new SqlParameter("@UserAccessRank", UserAccessRanks.Customer.ToString()),
        //            new SqlParameter("@UserPassword", model.UserPassword.ToString())).ToList();

        //    System.Diagnostics.Debug.WriteLine(results.First().UserID);

        //    return results;
        //} */

        //EXAMPLE

        /*public IEnumerable<Add_PubUserViewModel> Add_PubUser(RegisterViewModel model)
        {
            //var outputParam = new SqlParameter{ParameterName = "@Output", SqlDbType = SqlDbType.Bit, Direction = ParameterDirection.Output};

            //string sqlQuery = "Exec [ExampleStoredProc] @firstId, @outputBit OUTPUT";
            //var results = Add_PubUserResults.FromSqlRaw("EXEC Add_PubUser @UserFirstName, @UserLastName, @UserAccessRank, @UserPassword",
            //        new SqlParameter("@UserFirstName", model.UserFirstName.ToString()),
            //        new SqlParameter("@UserLastName", model.UserLastName.ToString()),
            //        new SqlParameter("@UserAccessRank", UserAccessRanks.Customer.ToString()),
            //        new SqlParameter("@UserPassword", model.UserPassword.ToString()),
            //        outputParam);

            //var output = outputParam.Value;
            //System.Diagnostics.Debug.WriteLine(output);

            //return results;

            var results = Add_PubUserResults.FromSqlRaw("EXEC Add_PubUser @UserFirstName, @UserLastName, @UserAccessRank, @UserPassword",
                    new SqlParameter("@UserFirstName", model.UserFirstName.ToString()),
                    new SqlParameter("@UserLastName", model.UserLastName.ToString()),
                    new SqlParameter("@UserAccessRank", UserAccessRanks.Customer.ToString()),
                    new SqlParameter("@UserPassword", model.UserPassword.ToString())).ToList();

            System.Diagnostics.Debug.WriteLine(results.First().UserID);

            return results;
        } */

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=socem1.uopnet.plymouth.ac.uk;Database=ISAD251_BRizza;User Id=BRizza; Password=ISAD251_22215888");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PubItems>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.Property(e => e.ItemId)
                    .HasColumnName("ItemID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ItemPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ItemType)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PubOrderItems>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ItemId });

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.PubOrderItems)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("ItemIDFK");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.PubOrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("OrderIDFK");
            });

            modelBuilder.Entity<PubOrders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PubOrders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("UserIDFK");
            });

            modelBuilder.Entity<PubUsers>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.UserFirstName).HasMaxLength(60);

                entity.Property(e => e.UserLastName).HasMaxLength(80);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
