using System.Threading.Tasks;
using nona.Models;

namespace nona.Services
{
    public interface IEmailService
    {
        Task<bool> SendMail(ContactModel model);
    }
}
