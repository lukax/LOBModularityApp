﻿#region Usings

using System.Collections.Generic;
using System.ComponentModel.Composition;
using LOB.Business.Contract.Logic;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Logic.Base;
using LOB.Dao.Contract;
using LOB.Domain;
using LOB.Domain.Base;

#endregion

namespace LOB.Business.Logic {
    [Export(typeof(ICustomerFacade)), Export(typeof(IBaseEntityFacade<Customer>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class CustomerFacade : BaseEntityFacade, ICustomerFacade {
        private readonly ILegalPersonFacade _legalPersonFacade;
        private readonly INaturalPersonFacade _naturalPersonFacade;

        [ImportingConstructor]
        public CustomerFacade(INaturalPersonFacade naturalPersonFacade, ILegalPersonFacade legalPersonFacade, IRepository repository)
                : base(repository) {
            _naturalPersonFacade = naturalPersonFacade;
            _legalPersonFacade = legalPersonFacade;
        }

        public Customer Generate() { return GenerateEntity(PersonType.Natural); }

        public Customer GenerateEntity(PersonType personType) {
            var result = new Customer();
            if(personType == PersonType.Natural) {
                result.Orders = new List<Order>();
                result.Status = default(CustomerStatus);
                result.AssociatedCompanies = new List<Company>();
                result.Person = _naturalPersonFacade.Generate();
                result.PersonType = default(PersonType);
            }
            if(personType == PersonType.Legal) {
                result.Orders = new List<Order>();
                result.Status = default(CustomerStatus);
                result.AssociatedCompanies = new List<Company>();
                result.Person = _legalPersonFacade.Generate();
                result.PersonType = default(PersonType);
            }
            return result;
        }
    }
}