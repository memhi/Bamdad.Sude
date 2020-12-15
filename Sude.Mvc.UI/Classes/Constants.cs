using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sude.Mvc.UI
{
    public static class Constants
    {
        public static class SessionNames
        {
            public const string CurrentWorkId = "CurrentWorkId";
            public const string CurrentWorkName = "CurrentWorkName";
            public const string OrderDetails = "OrderDetails";
            public const string CurrentWork = "CurrentWork";
        }

        public static class ViewBagNames
        {
            public const string Customers = "Customers";
            public const string Servings = "Servings";
            public const string OrderDetails = "OrderDetails";
            public const string CurrentWorkName = "CurrentWorkName";
            public const string CurrentWorkId = "CurrentWorkId";

            public const string WorkCount = "WorkCount";
            public const string ServingCount = "ServingCount";
            public const string OrderCount = "OrderCount";
            public const string OrderBuyCount = "OrderBuyCount";

            public const string SumOrderPrice = "SumOrderPrice";
            public const string SumOrderBuyPrice = "SumOrderBuyPrice";


        }

        public static class Messages
        {
            public const string InsertOrderDetail = "لطفا اطلاعات جزئیات سفارش را وارد کنید.";

        }

    }
}
