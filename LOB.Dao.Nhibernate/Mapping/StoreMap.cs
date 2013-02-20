﻿#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping
{
    public class StoreMap : BaseEntityMap<Store>
    {
        public StoreMap()
        {
            Map(x => x.Name);
            HasMany(x => x.Employees)
                .Cascade.All();
            HasManyToMany(x => x.Products)
                .Table("ProductStore")
                .Cascade.All();
            HasManyToMany(x => x.Clients)
                .Cascade.All();
            HasMany(x => x.Sales);
            References(x => x.Address)
                .Cascade.All();
            References(x => x.ContactInfo)
                .Cascade.All();
        }
    }
}