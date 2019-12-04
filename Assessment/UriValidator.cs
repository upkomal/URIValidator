using System.Threading.Tasks;

namespace Assessment
{
    public class UriValidator
    {
        public Task<bool> IsUriValid(string requestedUri)
        {
            return Task.FromResult(false);
        }
    }
}
