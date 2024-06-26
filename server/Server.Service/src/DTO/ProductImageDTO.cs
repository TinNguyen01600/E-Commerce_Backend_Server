using Server.Core.src.Entity;

namespace Server.Service.src.DTO;

public class ProductImageReadDTO : BaseEntity
{
  public string Url { get; set; }
}

public class ProductImageCreateDTO
{
  public string Url { get; set; }
}

public class ProductImageUpdateDTO
{
  public string Url { get; set; }
}