﻿#region Usings

using System;
using System.Collections.Generic;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable] public class ShipmentInfo : Service {

        public ShipmentStatus Status { get; set; }
        public Address Address { get; set; }
        public DateTime DeliverDate { get; set; }
        public int DaySchedule { get; set; }
        public IList<Product> Products { get; set; }

    }

    [Serializable] public enum ShipmentStatus {

        Active,
        Paused,
        Finished,
        Cancelled

    }
}