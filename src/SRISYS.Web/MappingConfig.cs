namespace Srisys.Web
{
    using System;
    using AutoMapper;
    using Srisys.Web.Common;
    using Srisys.Web.DTO;
    using Srisys.Web.Models;

    /// <summary>
    /// <see cref="MappingConfig"/> class Mapping configuration.
    /// </summary>
    public class MappingConfig : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingConfig"/> class.
        /// </summary>
        public MappingConfig()
        {
            // Activity
            this.CreateMap<Activity, ActivitySummary>()
                .ForMember(d => d.MaterialName, s => s.MapFrom(o => o.Material.Name))
                .ForMember(d => d.Unit, s => s.MapFrom(o => o.Material.Unit))
                .ForMember(d => d.Brand, s => s.MapFrom(o => o.Material.Brand))
                .ForMember(d => d.Model, s => s.MapFrom(o => o.Material.Model))
                .ForMember(d => d.Size, s => s.MapFrom(o => o.Material.Size))
                .ForMember(d => d.Use, s => s.MapFrom(o => o.Material.Use))
                .ForMember(d => d.BorrowedBy, s => s.MapFrom(o => o.BorrowedBy.Name))
                .ForMember(d => d.ReturnedBy, s => s.MapFrom(o => o.ReturnedBy.Name))
                .ForMember(d => d.ReleasedBy, s => s.MapFrom(o => $"{o.ReleasedBy.Firstname} {o.ReleasedBy.Lastname}"))
                .ForMember(d => d.ReceivedBy, s => s.MapFrom(o => $"{o.ReceivedBy.Firstname} {o.ReceivedBy.Lastname}"))
                .ForMember(d => d.Status, s => s.MapFrom(o => Enum.GetName(typeof(ActivityStatus), o.Status)))
                .ForMember(d => d.Balance, s => s.MapFrom(o => o.QuantityBorrowed - o.TotalQuantityReturned))
                .ForMember(d => d.ActionPerformed, s => s.MapFrom(o => o.Material.Type.Code == Constants.Tool ? "borrowed" : "used"));

            this.CreateMap<ActivityRequest, Activity>();

            // Borrower
            this.CreateMap<Borrower, BorrowerSummary>();

            this.CreateMap<Borrower, BorrowerLookup>();

            this.CreateMap<SaveBorrowerRequest, Borrower>();

            // Category
            this.CreateMap<Category, CategorySummary>();

            this.CreateMap<SaveCategoryRequest, Category>();

            // Material
            this.CreateMap<Material, MaterialSummary>()
                .ForMember(d => d.TypeCode, s => s.MapFrom(o => o.Type.Code))
                .ForMember(d => d.Categoryname, s => s.MapFrom(o => o.Category.Name))
                .ForMember(d => d.SubCategoryName, s => s.MapFrom(o => o.SubCategory.Name))
                .ForMember(d => d.SupplierName, s => s.MapFrom(o => o.Supplier.Name));

            this.CreateMap<Material, MaterialLookup>();

            this.CreateMap<SaveMaterialRequest, Material>();

            // Adjustment
            this.CreateMap<Adjustment, AdjustmentSummary>();

            // Subcategory
            this.CreateMap<Subcategory, SubcategorySummary>();

            this.CreateMap<SaveSubcategoryRequest, Subcategory>();

            // Supplier
            this.CreateMap<Supplier, SupplierSummary>();

            this.CreateMap<Supplier, SupplierLookup>();

            this.CreateMap<SaveSupplierRequest, Supplier>()
                .ForMember(d => d.CreatedBy, o => o.Condition(s => s.CreatedBy != 0))
                .ForMember(d => d.CreatedDate, o => o.Condition(s => s.CreatedDate != default(DateTime)));

            // Reference
            this.CreateMap<Reference, ReferenceLookup>();

            this.CreateMap<Reference, ReferenceSummary>()
                .ForMember(d => d.ParentCode, s => s.MapFrom(o => o.ParentReference.Code))
                .ForMember(d => d.ReferenceTypeCode, s => s.MapFrom(o => o.ReferenceType.Code));

            this.CreateMap<SaveReferenceRequest, Reference>();

            // ReferenceType
            this.CreateMap<ReferenceType, ReferenceTypeSummary>();

            this.CreateMap<SaveReferenceTypeRequest, ReferenceType>();

            // User
            this.CreateMap<ApplicationUser, UserSummary>()
                .ForMember(d => d.IsAdmin, o => o.MapFrom(s => s.AccessRights.Contains("admin")))
                .ForMember(d => d.CanView, o => o.MapFrom(s => s.AccessRights.Contains("canView")))
                .ForMember(d => d.CanWrite, o => o.MapFrom(s => s.AccessRights.Contains("canWrite")))
                .ForMember(d => d.CanDelete, o => o.MapFrom(s => s.AccessRights.Contains("canDelete")));

            this.CreateMap<SaveUserRequest, ApplicationUser>()
                .ForMember(d => d.PasswordHash, o => o.Ignore());
        }
    }
}
