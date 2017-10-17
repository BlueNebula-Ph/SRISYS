﻿namespace Srisys.Web
{
    using AutoMapper;
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
            this.CreateMap<Activity, ActivitySummary>();

            this.CreateMap<SaveActivityRequest, Activity>();

            // Material
            this.CreateMap<Material, MaterialSummary>()
                .ForMember(d => d.TypeCode, s => s.MapFrom(o => o.Type.Code))
                .ForMember(d => d.Categoryname, s => s.MapFrom(o => o.Category.Code))
                .ForMember(d => d.SubCategoryName, s => s.MapFrom(o => o.SubCategory.Code))
                .ForMember(d => d.SupplierName, s => s.MapFrom(o => o.Supplier.Name));

            this.CreateMap<Material, MaterialLookup>();

            this.CreateMap<SaveMaterialRequest, Material>();

            // Supplier
            this.CreateMap<Supplier, SupplierSummary>();

            this.CreateMap<Supplier, SupplierLookup>();

            this.CreateMap<SaveSupplierRequest, Supplier>();

            // Reference
            this.CreateMap<Reference, ReferenceLookup>();

            this.CreateMap<Reference, ReferenceSummary>()
                .ForMember(d => d.ParentCode, s => s.MapFrom(o => o.ParentReference.Code))
                .ForMember(d => d.ReferenceTypeCode, s => s.MapFrom(o => o.ReferenceType.Code));

            this.CreateMap<SaveReferenceRequest, Reference>();

            // ReferenceType
            this.CreateMap<ReferenceType, ReferenceTypeSummary>();

            this.CreateMap<SaveReferenceTypeRequest, ReferenceType>();
        }
    }
}
