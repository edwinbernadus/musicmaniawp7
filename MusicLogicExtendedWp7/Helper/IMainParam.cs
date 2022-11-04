using System.Threading.Tasks;

namespace MusicLogicExtendedWp7.Helper
{
    public interface IMainParam
    {
        Task<bool> GetDirectCommitDownload();
        Task<bool> GetWp8Status();
    }
}