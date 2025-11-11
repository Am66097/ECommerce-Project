using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOs.BasketDTOs
{
    public record BasketDTO(string Id, ICollection<BasketItemDTO> Items);

    // record: A special type in C# used for concise data storage, ideal for DTOs
    // immutable: Values cannot be changed after creation (safe for transfer and comparison)
    // Value equality: Compared by content, not by reference (if two objects have identical content → considered equal)
    // Supports quick construction via a built-in constructor in the same line
    // Facilitates working with JSON in Web APIs
    // Less code and clearer than a regular class

}
