using System.Threading.Tasks;

namespace MvvmCommon
{
    public interface IInitializedViewModel
    {
        Task Initialize();
    }
}