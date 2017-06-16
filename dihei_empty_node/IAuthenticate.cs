using System.Threading.Tasks;

namespace dihei_empty_node
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate();
    }
}
