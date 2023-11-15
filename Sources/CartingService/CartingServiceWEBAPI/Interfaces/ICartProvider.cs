using CartingServiceBusinessLogic;
using CartingServiceBusinessLogic.Infrastructure.Interfaces;

namespace CartingServiceWEBAPI.Interfaces
{
    public interface ICartProvider
    {
        public ICart Cart { get; }
    }
}
