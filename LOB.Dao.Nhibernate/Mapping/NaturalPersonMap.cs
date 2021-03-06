﻿#region Usings

using FluentNHibernate.Mapping;
using LOB.Domain.Base;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class NaturalPersonMap : SubclassMap<NaturalPerson> {
        public NaturalPersonMap() {
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.NickName);
            Map(x => x.BirthDate);
            Map(x => x.CPF);
            Map(x => x.RG);
            Map(x => x.RGUF);
        }
    }
}