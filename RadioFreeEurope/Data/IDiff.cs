using RadioFreeEurope.Models;
using System.Threading.Tasks;

namespace RadioFreeEurope.Data
{
    public interface IDiff
    {
        public Task<Response> GetDiffAsync(int id);

        public Task<Response> AddDiffAsync(Diff diff);
    }
}
