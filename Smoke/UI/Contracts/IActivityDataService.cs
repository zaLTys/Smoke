using UI.Responses;
using UI.ViewModels;
using System.Threading;

namespace UI.Contracts
{
    public interface IActivityDataService
    {
        Task<ServiceResponse<List<ActivityViewModel>>> GetApiRequestActivities(long id, CancellationToken cancellationToken);
    }
}
