using Domain.Purchases;
using Domain.StoreItems;

namespace Domain.Users;

public class User
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Contacts { get; set; }
    public byte[] Password { get; set; }
    public List<StoreItem> OfferedItems { get; set; }
    public List<Purchase> Purchases { get; set; }
}

