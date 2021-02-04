using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sude.Mvc.UI
{
    public static class Constants
    {
        public const int PageSize = 20;



        public static class AttachmentType
        {
            public const string AttachmentWorkLogo = "AttachmentWorkLogo";
            public const string AttachmentOrderPicture = "AttachmentOrderPicture";
            public const string AttachmentServingPicture = "AttachmentServingPicture";
            public const string AttachmentOrderBuyPicture = "AttachmentOrderBuyPicture";
            public const string AttachmentWorkPicture = "AttachmentWorkPicture";
        
        }

        public static class GroupType
        {
            public const string PaymentMode = "PaymentMode";
            public const string PaymentStatus = "PaymentStatus";
            public const string AttachmentType = "AttachmentType";
        }

        public static class PaymentMode
            {
            public const string ByCard = "ByCard";
            public const string ByCash = "ByCash";
        }
        public static class PaymenStatus
        {
            public const string NotPaid = "NotPaid";
            public const string CompletePaidByCard = "CompletePaidByCard";
            public const string CompletePaidByCash = "CompletePaidByCash";
        }

        public static class CookieNames
        {
            public const string Sude_ManagementWork_Cookie = "Sude_ManagementWork_Cookie";
            public const string Sude_ManagementWork_Cookie_UserName = "Sude_ManagementWork_Cookie_UserName";
        }


        public static class SessionNames
        {
            public const string SessionId = "SessionId";
            public const string CurrentWorkId = "CurrentWorkId";
            public const string CurrentWorkName = "CurrentWorkName";
            public const string OrderDetails = "OrderDetails";
            public const string AttachmentPictures = "AttachmentPictures";
            public const string UserWorks = "UserWorks";
            public const string CurrentWork = "CurrentWork";
            public const string IsAdmin = "IsAdmin";
            public const string CurrentUser = "CurrentUser";
            public const string CurrentTokenAccess = "CurrentTokenAccess";
            public const string CurrentAccessHttpClient = "CurrentAccessHttpClient";
            public const string CurrentTokenClient = "CurrentTokenClient";
            public const string RegisterTokenAccess = "RegisterTokenAccess";
            public const string AccessToken = "AccessToken";
            public const string CurrentLanguage= "CurrentLanguage";


        }

        public static class ViewBagNames
        {
            public const string PaymentStatus = "PaymentStatus";
            public const string OrderDateFrom = "OrderDateFrom";
            public const string OrderDateTo = "OrderDateTo";
            public const string Customers = "Customers";
            public const string Servings = "Servings";
            public const string OrderDetails = "OrderDetails";
            public const string CurrentWorkName = "CurrentWorkName";
            public const string CurrentWorkId = "CurrentWorkId";
            public const string Works = "Works";
            public const string WorkCount = "WorkCount";
            public const string ServingCount = "ServingCount";
            public const string OrderCount = "OrderCount";
            public const string OrderBuyCount = "OrderBuyCount";
            public const string OrderSearchType = "OrderSearchType";
            
            public const string SumOrderPrice = "SumOrderPrice";
            public const string SumOrderBuyPrice = "SumOrderBuyPrice";


        }

        public static class Messages
        {
            public const string InsertOrderDetail = "لطفا اطلاعات جزئیات سفارش را وارد کنید.";
            public const string OrderNotFound = "اطلاعات سفارش یافت نشد.";

        }

    }
}
