using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PubApplication.Models.Enum;
using PubApplication.ViewModels;
using System.Data;
using PubApplication.ViewModels.StoredProcedureViewModels;
using System;
using System.Collections;

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
        public virtual DbSet<Get_PubItemsViewModel> GetPubItemsResults { get; set; }

        //public virtual DbSet<Get_PubUserPasswordViewModel> GetPubUserPasswordResults { get; set; }

        public PubUsers AddPubUser(PubUsers model)
        {
            SqlParameter @outputParam = new SqlParameter {ParameterName = "@outputParam", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output};

            Database.ExecuteSqlRaw("EXEC @outputParam=Add_PubUser @UserFirstName, @UserLastName, @UserAccessRank, @UserPassword", 
                    outputParam,
                    new SqlParameter("@UserFirstName", model.UserFirstName),
                    new SqlParameter("@UserLastName", model.UserLastName),
                    new SqlParameter("@UserAccessRank", model.UserAccessRank.ToString()),
                    new SqlParameter("@UserPassword", model.UserPassword));
            int result = (int)@outputParam.Value; //User ID is returned. Can return the value from the stored procedure as User ID is an integer.
            if (result > 0)
            {
                model.UserId = result;
                return model;
            }
            return null;
        }

        public PubItems AddPubItem(PubItems model)
        {
            SqlParameter @outputParam = new SqlParameter { ParameterName = "@outputParam", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

            Database.ExecuteSqlRaw("EXEC @outputParam=Add_PubItem @ItemName, @ItemType, @ItemPrice, @ItemImagePath, @ItemDescription, @ItemStock, @ItemOnSale",
                    outputParam,
                    new SqlParameter("@ItemName", model.ItemName),
                    new SqlParameter("@ItemType", model.ItemType.ToString()),
                    new SqlParameter("@ItemPrice", model.ItemPrice),
                    new SqlParameter("@ItemImagePath", (object)model.ItemImagePath ?? DBNull.Value),
                    new SqlParameter("@ItemDescription", model.ItemDescription),
                    new SqlParameter("@ItemStock", (object)model.ItemStock ?? DBNull.Value),
                    new SqlParameter("@ItemOnSale", model.ItemOnSale));
            int result = (int)@outputParam.Value; //Item ID is returned. 
            if (result > 0)
            {
                model.ItemId = result;
                return model;
            }
            return null;
        }

        public bool EditPubItem(PubItems model)
        {
            SqlParameter @outputParam = new SqlParameter { ParameterName = "@outputParam", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

            Database.ExecuteSqlRaw("EXEC @outputParam=Update_PubItem @ItemID, @ItemName, @ItemType, @ItemPrice, @ItemImagePath, @ItemDescription, @ItemStock, @ItemOnSale",
                    outputParam,
                    new SqlParameter("@ItemID", model.ItemId),
                    new SqlParameter("@ItemName", model.ItemName),
                    new SqlParameter("@ItemType", model.ItemType.ToString()),
                    new SqlParameter("@ItemPrice", model.ItemPrice),
                    new SqlParameter("@ItemImagePath", (object)model.ItemImagePath ?? DBNull.Value),
                    new SqlParameter("@ItemDescription", model.ItemDescription),
                    new SqlParameter("@ItemStock", (object)model.ItemStock ?? DBNull.Value),
                    new SqlParameter("@ItemOnSale", model.ItemOnSale));
            int result = (int)@outputParam.Value; //Item ID is returned. 
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public PubUsers GetPubUser(int UserId)
        {
            var results = GetPubUserResults.FromSqlRaw("EXEC Get_PubUser @UserID", 
                new SqlParameter("@UserID", UserId)).ToList();
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

        public PubItems GetPubItem(int ItemID) //get pub items - just the item name
        {
            var results = GetPubItemsResults.FromSqlRaw("EXEC Get_PubItem @ItemID",
                new SqlParameter("@ItemID", ItemID)).ToList();
            if (results.Count() > 0)
            {
                return ConvertItemResultsToPubItems(results.First());
            }
            else
            {
                return null; //no results returned - user ID dosen't exist in DB so give nothing.
            }
        }

        public PubItems GetRandomPubItem(ItemTypes Type) //get pub items - just the item name
        {
            var results = GetPubItemsResults.FromSqlRaw("EXEC Get_RandomPubItem @ItemType",
                new SqlParameter("@ItemType", Type.ToString())).ToList();
            if (results.Count() > 0)
            {
                return ConvertItemResultsToPubItems(results.First());
            }
            else
            {
                return null; //no results returned - user ID dosen't exist in DB so give nothing.
            }
        }

        //public List<PubItems> GetPubItems(string ItemName) //get pub items - just the item name
        //{
        //    var results = GetPubItemsResults.FromSqlRaw("EXEC Get_PubItems @ItemName",
        //        new SqlParameter("@ItemName", (object)ItemName ?? DBNull.Value)).ToList();
        //    return ConvertPubItemResultsToPubItems(results);
        //}

        //public List<PubItems> GetPubItems(string ItemName, ItemTypes TypeOfItem) //get pub items - item type and name
        //{
        //    var results = GetPubItemsResults.FromSqlRaw("EXEC Get_PubItems @ItemName, @ItemType",
        //        new SqlParameter("@ItemName", (object)ItemName ?? DBNull.Value),
        //        new SqlParameter("@ItemType", TypeOfItem.ToString())).ToList();
        //    return ConvertPubItemResultsToPubItems(results);
        //}

        public PubItemsViewModel GetPubItems(string ItemName, Boolean ItemOnSale, int PageNumber) //get pub items - if on sale and name
        {
            SqlParameter @outputParam = new SqlParameter { ParameterName = "@outputParam", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

            var results = GetPubItemsResults.FromSqlRaw("EXEC @outputParam=Get_PubItems_OnSaleFilter @ItemName, @ItemOnSale, @PageNumber, @ItemsPerPage",
                outputParam,
                new SqlParameter("@ItemName", (object)ItemName ?? DBNull.Value),
                new SqlParameter("@PageNumber", PageNumber),
                new SqlParameter("@ItemsPerPage", GlobalConstants.ItemsPerPage),
                new SqlParameter("@ItemOnSale", ItemOnSale)).ToList();
            return new PubItemsViewModel { PubItemsList = ConvertPubItemResultsToPubItems(results), RowCount = (int)@outputParam.Value };
        }

        public PubItemsViewModel GetPubItems(string ItemName, bool ItemOnSale, int PageNumber, ItemTypes TypeOfItem) //get pub items - item type, if on sale and name
        {
            SqlParameter @outputParam = new SqlParameter { ParameterName = "@outputParam", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

            var results = GetPubItemsResults.FromSqlRaw("EXEC @outputParam=Get_PubItems_OnSaleFilter @ItemName, @ItemOnSale, @PageNumber, @ItemsPerPage, @ItemType", //fetch pub items, output param returns the total number of results from the query
                outputParam,
                new SqlParameter("@ItemName", (object)ItemName ?? DBNull.Value),
                new SqlParameter("@PageNumber", PageNumber),
                new SqlParameter("@ItemsPerPage", GlobalConstants.ItemsPerPage),
                new SqlParameter("@ItemOnSale", ItemOnSale),
                new SqlParameter("@ItemType", TypeOfItem.ToString())).ToList();
            return new PubItemsViewModel { PubItemsList = ConvertPubItemResultsToPubItems(results), RowCount = (int)@outputParam.Value };
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

        private static List<PubItems> ConvertPubItemResultsToPubItems(List<Get_PubItemsViewModel> results)
        {
            if (results.Count() > 0)
            {
                List<PubItems> pubItems = new List<PubItems>();
                foreach (Get_PubItemsViewModel item in results)
                {
                    PubItems Item = ConvertItemResultsToPubItems(item);
                    pubItems.Add(Item);
                }
                return pubItems;
            }
            else
            {
                return null; //no results returned - user ID dosen't exist in DB so give nothing.
            }
        }

        private static PubItems ConvertItemResultsToPubItems(Get_PubItemsViewModel item)
        {
            return new PubItems()
            {
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                ItemDescription = item.ItemDescription,
                ItemImagePath = item.ItemImagePath,
                ItemOnSale = Convert.ToBoolean(item.ItemOnSale),
                ItemPrice = item.ItemPrice,
                ItemStock = item.ItemStock,
                ItemType = PubItemType.GetItemType(item.ItemType)
            };
        }









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
