using System.IO;
using System.Threading.Tasks;

namespace InstantReview
{
    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }
}