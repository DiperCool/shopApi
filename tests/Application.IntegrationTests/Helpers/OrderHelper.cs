using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.IntegrationTests.Helpers;

public static class OrderHelper
{
    public static List<Order> GetOrders()
    {
        return new List<Order>(){
            new Order(){
                PaymentStatus = PaymentStatus.Success,
                PaymentType = PaymentType.COD,
                TotalPrice=100,
                OrderItems = new List<OrderItem>(){
                    new OrderItem(){
                        Product = ProductHelper.GetCar(),
                        Quantity = 2 
                    }
                }
            },
            new Order(){
                PaymentStatus = PaymentStatus.Pending,
                PaymentType = PaymentType.Stripe,
                TotalPrice=100,
                OrderItems = new List<OrderItem>(){
                    new OrderItem(){
                        Product = ProductHelper.GetCar(),
                        Quantity = 3
                    },
                    new OrderItem(){
                        Product = ProductHelper.GetPhone(),
                        Quantity = 2
                    }
                }
            },
            new Order(){
                PaymentStatus = PaymentStatus.Cancel,
                PaymentType = PaymentType.Stripe,
                TotalPrice=100,
                OrderItems = new List<OrderItem>(){
                    new OrderItem(){
                        Product = ProductHelper.GetCar(),
                        Quantity = 3
                    },
                    new OrderItem(){
                        Product = ProductHelper.GetSofa(),
                        Quantity = 1
                    }
                }
            }
        };
    }
}
