using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Text;
using Impulse.DBAccessLayer;
using System.Configuration;

namespace Impulse.DbEngine
{
    public class DbEngine : DbContext
    {
        public DbEngine()
        {
            this.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["ImpulseEntities"].ConnectionString;
        }
        public DbSet<SaveGeneratedOTPForRegisterUserDetail> SaveGeneratedOTPForRegisterUserDetail { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Sub_Category> Sub_Category { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<AddToCart> AddToCart { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrdersDetails> OrdersDetails { get; set; }
        public DbSet<SubCategoryKeyWords> SubCategoryKeyWords { get; set; } // Here added On (26-01-2023)
        public DbSet<Sub_CategoryColorDetails> Sub_CategoryColorDetails { get; set; } // Here added On (29-01-2023)
        public DbSet<Sub_CategoryAboutItemDB> Sub_CategoryAboutItem { get; set; } // Here added On (29-01-2023)
        public DbSet<Sub_CategoryImageListDB> Sub_CategoryImageList { get; set; } // Here added On (29-01-2023)
        public DbSet<Sub_CategoryImage> Sub_CategoryImage { get; set; } // Here added On (01-02-2023)
        public DbSet<SubCategoryReviewAndStars> SubCategoryReviewAndStars { get; set; } // Here added On (01-02-2023)
        public DbSet<Sub_CategorySizeDetails> Sub_CategorySizeDetails { get; set; } // Here added On (01-02-2023)
        public DbSet<PinCode> PinCode { get; set; }
        public DbSet<Stars> Stars { get; set; }
        public DbSet<Color> Color { get; set; }
    }
}