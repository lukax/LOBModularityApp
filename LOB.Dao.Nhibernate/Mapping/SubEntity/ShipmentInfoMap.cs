﻿#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.SubEntity {
    public class ShipmentInfoMap : BaseEntityMap<ShipmentInfo> {
        public ShipmentInfoMap() {
            References(x => x.Shipper);
            Map(x => x.Status);
            References(x => x.Address);
            Map(x => x.DeliverDate);
            Map(x => x.DaySchedule);
            HasMany(x => x.Products);
        }
    }
}