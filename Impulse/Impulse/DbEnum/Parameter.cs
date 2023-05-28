using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Impulse.DbEnum
{
    public enum Role
    {
        Admin = 1,
        SubAdmin = 2
    }

    public enum Product
    {
        Product1 = 1,
        Product2 = 2
    }

    public enum IsActiveStatus
    {
        Active = 0,
        DeActive = 1
    }

    public enum InvoiceStatus
    {
        Unpaid = 0,
        Paid = 1,
        Cancel = 2
    }
}