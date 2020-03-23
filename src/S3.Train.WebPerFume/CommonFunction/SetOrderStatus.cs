using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S3.Train.WebPerFume.CommonFunction
{
    public static class SetOrderStatus
    {
        // set Order status to type string for oder by paramete pass type OrderStatus
        public static string SetStatus(OrderStatus orderStatus)
        {
            string status = "";
            switch (orderStatus)
            {
                case OrderStatus.Receive:
                    status = "Receive Order";
                    break;
                case OrderStatus.Confirm:
                    status = "Confirm Order";
                    break;
                case OrderStatus.TakeProduct:
                    status = "Take Product";
                    break;
                case OrderStatus.Delivery:
                    status = "Delivery Order";
                    break;
                case OrderStatus.Success:
                    status = "Success Order";
                    break;
                case OrderStatus.Cancel:
                    status = "Cancel";
                    break;
                default:
                    break;
            }
            return status;
        }

        // set order status for order by paramete pass type int
        public static OrderStatus SetStatusTypeInt(int status)
        {
            OrderStatus orderStatus = OrderStatus.Receive;
            switch (status)
            {
                case 0:
                    orderStatus = OrderStatus.Receive;
                    break;
                case 1:
                    orderStatus = OrderStatus.Confirm;
                    break;
                case 2:
                    orderStatus = OrderStatus.TakeProduct;
                    break;
                case 3:
                    orderStatus = OrderStatus.Delivery;
                    break;
                case 4:
                    orderStatus = OrderStatus.Success;
                    break;
                case 5:
                    orderStatus = OrderStatus.Cancel;
                    break;
                default:
                    break;
            }
            return orderStatus;
        }
    }
}