using System.ComponentModel.DataAnnotations;

namespace ServiceTemplate.Contracts
{
    public class CheckoutRequest
    {
        [Required]
        public CoinCollection Inserted { get; set; } = null!;

        // Should be decimal, but in the service we use uint for number of
        // coins and notes, and even for the value of coins and notes.
        [Required]
        public uint Price { get; set; }
    }
}