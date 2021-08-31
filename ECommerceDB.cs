using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Model
{
    public class ECommerceDB:DbContext
    {
        public ECommerceDB(DbContextOptions<ECommerceDB> options) : base(options)
        {
            
        }
        public DbSet<ProductPhotoModel> productPhotos { get; set; }
        public DbSet<LanguageModel> Languages { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<RolePermissionModel> RolePermissions { get; set; }
        public DbSet<UserInRoleModel> UserInRoles { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<LogModel > Logs { get; set; }
        public DbSet<StoreModel> Stores { get; set; }
        public DbSet<WeightUnitModel>  WeightUnits { get; set; }
        public DbSet<WeightUnitDetailModel> WeightUnitDetails { get; set; }
        public DbSet<CountryModel> countries { get; set; }
        public DbSet<CountryDetailModel> countryDetails { get; set; }
        public DbSet<ProvinceModel> provinces { get; set; }
        public DbSet<ProvinceDetailModel> provinceDetails { get; set; }
        public DbSet<CityModel> cities { get; set; }
        public DbSet<CityDetailModel> cityDetails { get; set; }
        public DbSet<ShippingModel> shippings { get; set; }
        public DbSet<MaterialModel> materials { get; set; }
        public DbSet<MaterialDetailModel> materialDetails { get; set; }
        public DbSet<VarietyModel> varieties { get; set; }
        public DbSet<VarietyDetailModel> varietyDetails { get; set; }
        public DbSet<CategoryModel> categories { get; set; }
        public DbSet<CategoryDetailModel> categoryDetails { get; set; }
        public DbSet<CategoryPropModel> categoryProps { get; set; }
        public DbSet<CategoryPropDetailModel> categoryPropDetails { get; set; }           
        public DbSet<ProductModel> products { get; set; }
        public DbSet<ProductDetailModel> productDetails { get; set; }
        public DbSet<ProductPropertyModel> productProperties { get; set; }
        public DbSet<ProductVarietyModel> ProductVarieties { get; set; }
        public DbSet<ZoneModel> Zones { get; set; }
        public DbSet<ZoneProvinceModel> zoneProvinces { get; set; }

    }
}
