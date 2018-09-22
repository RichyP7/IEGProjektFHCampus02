﻿using System;

namespace BusinessLogic.Core.Entities
{
    public class CreditcardTransaction
    {
        public string CreditcardNumber { get; set; }
        public string CreditcardType { get; set; }
        public double Amount { get; set; }
        public string ReceiverName { get; set; }
    }
}
