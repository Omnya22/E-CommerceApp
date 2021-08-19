namespace E_CommerceApp.Core.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }

        public bool IsWaited { get; set; }

        public bool IsAccepted { get; set; }

        public bool IsRejected { get; set; }

    }
}
