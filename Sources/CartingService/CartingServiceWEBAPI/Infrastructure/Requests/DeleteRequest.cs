using CartingServiceBusinessLogic.Infrastructure.Entities;

namespace CartingServiceWEBAPI.Infrastructure.Requests
{
    public class DeleteRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int cartItemId { get; set; }

    }
}
