namespace BookResellerStore.Common
{
    public class Constants
    {
        public enum BookStore
        {
            A,
            B
        }

        public enum OrderResult
        {
            QuantityInValid = -3,
            ExceedQuantityStock = -2,
            OrderFail = -1,
            OrderSuccessful = 1
        }

        public enum OrderExportResult
        {
            EmptyOrders = -2,
            NoOrders = -3,
            ExportFail = -1,
            ExportSuccessful = 1,
        }

        public static class RightName
        {
            public const string ViewOrder = "ViewOrder";
            public const string CreateOrder = "CreateOrder";
            public const string ExportOrder = "ExportOrder";
        }
        public static class PermissionType
        {
            public const string Right = "Right";
        }
    }
}
