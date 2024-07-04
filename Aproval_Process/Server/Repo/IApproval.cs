using Server.Model;

namespace Server.Repo
{
    public interface IApproval
    {
        #region Auth
        public  Task<int> loginUser(string Username, string Password);
        #endregion Auth
        #region Approval
        public Task<string> GetApprovalNumber();
        public Task<int> RequestApproval(ViewModel_RequestApproval model);
        public Task<List<ViewModel_ApprovalRequestsDetails>> ApprovalRequestDetails(int userid);
        public  Task<int> ApprovalActions(int user_id, int request_id, int type, string remarks);
        public Task<List<ViewModel_AprrovalRequestStatusDetails>> ApprovalRequestStausDetails(int request_id);
        #endregion Approval
    }
}
