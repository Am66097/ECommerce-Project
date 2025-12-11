namespace E_Commerce.Shared.DTOs.OrderDTOs
{
    public record OrderItemDTO(string ProductName, decimal Price, int Quantity, string PictureUrl);
   
}