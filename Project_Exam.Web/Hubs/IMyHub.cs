using Microsoft.AspNetCore.SignalR;

namespace Project_Exam.Web.Hubs
{
    public interface IMyHub
    {
        Task DeleteUser();
        Task ChangeRole(string userId, string roleId);
        /**********************************************/
        Task RealTime();
    }
}
