﻿#region Usings

using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain {
    [Serializable]
    public class LegalPerson : Person {

        public string CorporateName { get; set; }
        public string TradingName { get; set; }
        public int CNPJ { get; set; }
        public int Iestadual { get; set; }
        public int Imunicipal { get; set; }
        public int CNAEFiscal { get; set; }

    }
}