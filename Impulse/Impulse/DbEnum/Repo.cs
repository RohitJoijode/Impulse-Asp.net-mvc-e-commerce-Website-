using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Impulse.DbEnum
{
   public enum Response
    {
        Success = 1,
        Fail = 0,
    }

    public enum UserRole
    {
        Admin = 1,
        Buyer = 2,
        Seller = 3,
        Distributor = 4,
    }

    public enum Category
    {
        LadiesJewellery = 1,
        LadiesBags = 2,
        LadiesShoes = 3,
        LadiesT_Shirt = 4,
        LadiesNighty = 5,
        TopDeals = 6,
        Recommend = 7,
    }

    public enum OrderStatus
    {
        Pending = 1, 
        OnProcess = 2, 
        Delivered = 3,
        Cancel = 4,
    }

    public enum PaymentMethodSelection
    {
        None = 1,
        CashOnDelivery = 2,
        UPIPayment = 3,
        DebitCardAndCreditCard = 4,
    }

    public enum StockStatus
    {
        InStock = 1,
        OutOfStock = 2,
    }


    public enum Color
    {
        Red    =  1,        
        White  =  2, 
        Black  = 3,
        Yellow = 4,
        Green  = 5,
        Orange = 6,
        Blue   = 7,
        Pink   = 8,
        Olive  = 9,
        OffWhite  = 10,
        NavyBlue = 11,
        Maroon   = 12,
        Mint     = 13,
        GoldenColor  = 14,
        Metallic  = 15,
        Lime   = 16,
        Ivoy   = 17,
        Grey   = 18,
        Clay   = 19,
        Brown  = 20,
        Bronze = 21,
        Beige  = 22,
        Azure  = 23,
        Amber  = 24,
        Violet = 25,
        Turquoise  = 26,
        Rust   = 27,
        Ruby   = 28,
        Purple = 29,
        Indigo = 30,
        Cyan   = 31,
        Magenta  = 32,
        Coral    = 33,
        Teal     = 34,
    }

}