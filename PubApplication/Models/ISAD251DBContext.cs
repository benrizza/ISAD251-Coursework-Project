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
        public virtual DbSet<PubSessions> PubSessions { get; set; }
        public virtual DbSet<PubOrderBasketItems> PubOrderBasketItems { get; set; }

        //EXAMPLE
        //public virtual DbSet<addPubUserViewModel> AddPubUserResults { get; set; }


        public virtual DbSet<Get_PubUserViewModel> GetPubUserResults { get; set; }
        public virtual DbSet<Get_PubItemsViewModel> GetPubItemsResults { get; set; }
        public virtual DbSet<Get_PubUserDetailsViewModel> GetPubUserDetailsResults { get; set; }
        public virtual DbSet<Get_PubOrderBasketItemsViewModel> GetPubOrderBasketItemsResults { get; set; }
        public virtual DbSet<Get_PubOrderItemsViewModel> GetPubOrderItemsResults { get; set; }
        public virtual DbSet<Get_PubOrderViewModel> GetPubOrderResults { get; set; }

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
                    new SqlParameter("@ItemStock", (object)model.ItemStock ?? 0),
                    new SqlParameter("@ItemOnSale", model.ItemOnSale));
            int result = (int)@outputParam.Value; //Item ID is returned. 
            if (result > 0)
            {
                model.ItemId = result;
                return model;
            }
            return null;
        }

        public int AddPubOrder(int UserID) //take in user ID return the order ID
        {
            SqlParameter @outputParam = new SqlParameter { ParameterName = "@outputParam", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

            Database.ExecuteSqlRaw("EXEC @outputParam=Add_PubItem @UserID",
                    outputParam,
                    new SqlParameter("@UserID", UserID));
            return (int)@outputParam.Value; //order id is returned. 
        }

        public bool AddPubOrderBasketItem(int OrderBasketID, int ItemID, int ItemQuantity) //take in user ID return the order ID
        {
            SqlParameter @outputParam = new SqlParameter { ParameterName = "@outputParam", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

            Database.ExecuteSqlRaw("EXEC @outputParam=Add_PubOrderBasketItem @OrderBasketID, @ItemID, @ItemQuantity",
                    outputParam,
                    new SqlParameter("@OrderBasketID", OrderBasketID), 
                    new SqlParameter("@ItemID", ItemID),
                    new SqlParameter("@ItemQuantity", ItemQuantity));
            int result = (int)@outputParam.Value; //1 or 0 is returned - 1 being success, 0 being failure
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public string AddPubSession(int? UserID, int? OrderBasketID) //take in user ID return the order ID
        {
            string SessionID = String.Format("Session_{0}",Guid.NewGuid().ToString());
            SqlParameter @outputParam = new SqlParameter { ParameterName = "@outputParam", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

            Database.ExecuteSqlRaw("EXEC @outputParam=Add_PubSession @SessionID, @UserID, @OrderBasketID",
                    outputParam,
                    new SqlParameter("@OrderBasketID", OrderBasketID ?? 0),
                    new SqlParameter("@UserID", UserID ?? 0),
                    new SqlParameter("@SessionID", SessionID));
            if ((int)@outputParam.Value == 1) //1 or 0 is returned - 1 being success, 0 being failure
            {
                return SessionID;
            }
            else
            {
                return null;
            }
        }

        public int CreatePubSessionOrderBasket(string SessionID, int? OrderBasketID)
        {
            SqlParameter @outputParam = new SqlParameter { ParameterName = "@outputParam", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

            Database.ExecuteSqlRaw("EXEC @outputParam=Create_PubSessionOrderBasket @SessionID, @OrderBasketID",
                    outputParam,
                    new SqlParameter("@SessionID", SessionID),
                    new SqlParameter("@OrderBasketID", OrderBasketID ?? 0));
            return (int)@outputParam.Value; //order basket id is returned
        }

        public int CreateOrder(int OrderBasketID, int UserID, string SessionID)
        {
            SqlParameter @outputParam = new SqlParameter { ParameterName = "@outputParam", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

            Database.ExecuteSqlRaw("EXEC @outputParam=Create_PubOrder @UserID, @OrderBasketID, @SessionID",
                    outputParam,
                    new SqlParameter("@UserID", UserID),
                    new SqlParameter("@OrderBasketID", OrderBasketID),
                    new SqlParameter("@SessionID", SessionID));
            return (int)@outputParam.Value; //order id is returned
        }

        public bool EditPubOrderBasketItem(int OrderBasketID, int ItemID, int ItemQuantity)
        {
            SqlParameter @outputParam = new SqlParameter { ParameterName = "@outputParam", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

            Database.ExecuteSqlRaw("EXEC @outputParam=Update_PubOrderBasketItem @OrderBasketID,@ItemID,@ItemQuantity",
                    outputParam,
                    new SqlParameter("@OrderBasketID", OrderBasketID),
                    new SqlParameter("@ItemID", ItemID),
                    new SqlParameter("@ItemQuantity", ItemQuantity));
            int result = (int)@outputParam.Value;
            if (result == 1)
            {
                return true;
            }
            return false;
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
            int result = (int)@outputParam.Value; 
            if (result == 1)
            {
                return true;
            }
            return false;
        }


        public bool UpdatePubSession(string SessionID, int UserID, int? OrderBasketID)
        {
            SqlParameter @outputParam = new SqlParameter { ParameterName = "@outputParam", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

            Database.ExecuteSqlRaw("EXEC @outputParam=Update_PubSession @SessionID, @OrderBasketID, @UserID",
                    outputParam,
                    new SqlParameter("@SessionID", SessionID),
                    new SqlParameter("@OrderBasketID", OrderBasketID ?? 0),
                    new SqlParameter("@UserID", UserID));
            int result = (int)@outputParam.Value;
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public bool UpdatePubUserOrderBasket(int UserID, int OrderBasketID)
        {
            SqlParameter @outputParam = new SqlParameter { ParameterName = "@outputParam", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

            Database.ExecuteSqlRaw("EXEC @outputParam=Update_PubUserOrderBasket @UserID, @OrderBasketID",
                    outputParam,
                    new SqlParameter("@OrderBasketID", OrderBasketID),
                    new SqlParameter("@UserID", UserID));
            int result = (int)@outputParam.Value;
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
                    UserAccessRank = UserRank.GetRank(results.First().UserAccessRank),
                    UserOrderBasketID = results.First().UserOrderBasketID ?? 0
                }; 
                //If results are returned then the user has been found. As the user is unique, their will only be one result so the password is fetched from the first (only) result.
                return output;
            }
            else
            {
                return null; //no results returned - user ID dosen't exist in DB so give nothing.
            }
        }

        public Get_PubOrderViewModel GetPubOrder(int OrderID)
        {
            var results = GetPubOrderResults.FromSqlRaw("EXEC Get_PubOrder @OrderID",
                new SqlParameter("@OrderID", OrderID)).ToList();
            if (results.Count() > 0) {
                return results.First();
            }
            else
            {
                return null; //no results returned - order dosen't exist in DB so give nothing.
            }
        }

        public List<Get_PubOrderItemsViewModel> GetPubOrderItems(int OrderID)
        {
            var results = GetPubOrderItemsResults.FromSqlRaw("EXEC Get_PubOrderItems @OrderID",
            new SqlParameter("@OrderID", OrderID)).ToList();
            if (results.Count() > 0) {
                return results;
            }
            else
            {
                return null; //no results returned - order dosen't exist in DB so give nothing.
            }
        }

        public PubUserDetailsViewModel GetPubUserDetails(int UserId)
        {
            var results = GetPubUserDetailsResults.FromSqlRaw("EXEC Get_PubUserDetails @UserID",
                new SqlParameter("@UserID", UserId)).ToList();
            if (results.Count() > 0)
            {
                PubUserDetailsViewModel output = new PubUserDetailsViewModel
                {
                    UserId = results.First().UserId,
                    UserFirstName = results.First().UserFirstName,
                    UserLastName = results.First().UserLastName,
                    UserAccessRank = UserRank.GetRank(results.First().UserAccessRank),
                    UserOrderBasketID = results.First().UserOrderBasketID ?? 0
                };
                //If results are returned then the user has been found. As the user is unique, their will only be one result so the password is fetched from the first (only) result.
                return output;
            }
            else
            {
                return null; //no results returned - user ID dosen't exist in DB so give nothing.
            }
        }

        public List<OrderBasketViewModel> GetPubOrderBasketItems(int OrderBasketID)
        {
            var results = GetPubOrderBasketItemsResults.FromSqlRaw("EXEC Get_PubOrderBasketItems @OrderBasketID",
                new SqlParameter("@OrderBasketID", OrderBasketID)).ToList();
            if (results.Count() > 0)
            {
                List<OrderBasketViewModel> OrderBasket = new List<OrderBasketViewModel>();
                foreach (Get_PubOrderBasketItemsViewModel item in results)
                {
                    OrderBasket.Add(new OrderBasketViewModel()
                    {
                        ItemQuantity = item.ItemQuantity,
                        PubItem = new PubItems()
                        {
                            ItemDescription = item.ItemDescription,
                            ItemId = item.ItemId,
                            ItemImagePath = item.ItemImagePath,
                            ItemName = item.ItemName,
                            ItemOnSale = item.ItemOnSale,
                            ItemPrice = item.ItemPrice,
                            ItemStock = item.ItemStock,
                            ItemType = PubItemType.GetItemType(item.ItemType)
                        }
                    });
                }
                //If results are returned then the user has been found. As the user is unique, their will only be one result so the password is fetched from the first (only) result.
                return OrderBasket;
            }
            else
            {
                return null; //no results returned - user ID dosen't exist in DB so give nothing.
            }
        }

        public PubSessions GetPubSession(string SessionID)
        {
            var results = PubSessions.FromSqlRaw("EXEC Get_PubSession @SessionID",
                new SqlParameter("@SessionID", SessionID)).ToList();
            if (results.Count() > 0)
            {
                PubSessions session = results.First();
                return session;
            }
            else
            {
                return null; 
            }
        }

        public PubOrderBasketItems GetPubOrderBasketItem(int OrderBasketID, int ItemID)
        {
            var results = PubOrderBasketItems.FromSqlRaw("EXEC Get_PubOrderBasketItem @OrderBasketID,@ItemID",
                new SqlParameter("@OrderBasketID", OrderBasketID),
                new SqlParameter("@ItemID", ItemID)).ToList();
            if (results.Count() > 0)
            {
                PubOrderBasketItems item = results.First();
                return item;
            }
            else
            {
                return null; 
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
                return null; 
            }
        }

        public PubItems GetRandomPubItem(ItemTypes Type, bool ItemOnSale) //get pub items - just the item name
        {
            var results = GetPubItemsResults.FromSqlRaw("EXEC Get_RandomPubItem @ItemOnSale,@ItemType",
                new SqlParameter("@ItemOnSale", ItemOnSale), 
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

            modelBuilder.Entity<Get_PubOrderViewModel>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.UserId });

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.OrderDate).HasColumnName("OrderDate");
            });

            modelBuilder.Entity<Get_PubOrderViewModel>().HasKey(c => new { c.OrderId, c.UserId });

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

            modelBuilder.Entity<PubOrderBasketItems>(entity =>
            {
                entity.HasKey(e => new { e.OrderBasketId, e.ItemId });

                entity.Property(e => e.OrderBasketId).HasColumnName("OrderBasketID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemQuantity).HasColumnName("ItemQuantity");
            });

            modelBuilder.Entity<PubSessions>(entity =>
            {
                entity.HasKey(e => e.SessionId);

                entity.Property(e => e.SessionId)
                    .HasColumnName("SessionID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderBasketId).HasColumnName("OrderBasketID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

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
