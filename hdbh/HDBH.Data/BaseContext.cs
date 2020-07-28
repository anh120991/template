using HDBH.Models;
using HDBH.Models.HelperModel;
using Microsoft.EntityFrameworkCore;

namespace HDBH.Data
{
    public class BaseContext : DbContext
    {
        private readonly string _connString;
        public BaseContext(string connstring)
        {
            _connString = connstring;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_connString);
            optionsBuilder.UseOracle(_connString);
        }

        public DbSet<PhanQuyenUserModel> User { get; set; }
        public DbSet<UserPermissionModel> ListUser { get; set; }
        public DbSet<BranchModel> Branch { get; set; }
        public DbSet<ResultModel> responseModel { get; set; }
        public DbSet<BranchPermissionModel> branchPermission { get; set; }
        public DbSet<NonLifeContractListModel> nonLifeContractList{ get; set; }
        public DbSet<ContractFormModel> contractFormModel { get; set; }
        public DbSet<ContractStatusModel> contractStatusModel { get; set; }
        public DbSet<ExpectTypeModel> expectType { get; set; }
        public DbSet<ContractTypeModel> contractType { get; set; }
        public DbSet<ProductCounterPartyModel> productCounterPartyModels { get; set; }
        public DbSet<CounterPartyModel> counterPartyModels { get; set; }
        public DbSet<NonLifeInsuranceDetailModel> nonLifeInsuranceDetailModels { get; set; }
        public DbSet<ColateralModel> colateralModels { get; set; }

        public DbSet<InsuranceInauListModel> insuranceInauListModels { get; set; }

        public DbSet<ActionAttributeReturn> ActionAttributeReturn { get; set; }

        public DbSet<CounterPartyItemListModel> counterPartyItemListModels { get; set; }
        public DbSet<CounterPartyCifModel> counterPartyCifModels { get; set; }
        public DbSet<ResultInsuranceContractDetailModel> ResultInsuranceContractDetailModels { get; set; }
        public DbSet<RecipientListModel> recipientListModels { get; set; }

        public DbSet<RecipientDetailModel> recipientDetailModels { get; set; }
        public DbSet<RecipientInauListModel> recipientInauListModels { get; set; }
        public DbSet<InsuranceContractListModel> insuranceContractListModels { get; set; }
        public DbSet<PaymentScheduleModel> paymentScheduleModels { get; set; }
        public DbSet<RoleListModel> roleListModels { get; set; }
        public DbSet<ModuleListModel> moduleListModels { get; set; }
        public DbSet<UserLoginModel> userLoginModels { get; set; }
    }
}
