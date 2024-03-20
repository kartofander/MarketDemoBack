namespace Domain.Purchases;

public enum PurchaseStatus
{
    Undefined = 0,
    AwaitingPayment = 1,
    InProcess = 2,
    Delivered = 3,
    Recieved = 4,
    Canceled = 5,
}