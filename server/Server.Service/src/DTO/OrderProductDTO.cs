namespace Server.Service.src.DTO;

public class OrderProductReadDTO
{
    public ProductReadDTO Product { get; set; }
    public int Quantity { get; set; }
}

public class OrderProductCreateDTO
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}