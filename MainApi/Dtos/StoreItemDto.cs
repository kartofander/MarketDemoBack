namespace MainApi.Dtos;

public class StoreItemDto
{
    public long ItemId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public float Cost { get; set; }
}