namespace Server.Model
{
    public class Approval_Model_Master
    {
    }
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string Details { get; set; }
    }
    public class ViewModel_RequestApproval
    {
        public int id { get; set; }
        public string approval_no { get; set; }
        public string request_date { get; set; }
        public string due_date { get; set; }
        public int category_id { get; set; }
        public int subcategory_id { get; set; }
        public string approval_title { get; set; }
        public string approval_desc { get; set; }
        public string approval_remarks { get; set; }
        public string user { get; set; }
        public int user_id { get; set; }
    }
    public class ViewModel_Registration
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
    }
    public class ViewModel_ApprovalRequestsDetails
    {
        public int id { get; set; }
        public string approval_no { get; set; }
        public string request_date { get; set; }
        public string category { get; set; }
        public string  subcategory { get; set; }
        public string approval_title { get; set; }
        public string approval_desc { get; set; }
        public string approval_remarks { get; set; }
    }
    public class ViewModel_MultiApproval
    {
        public int user_id { get; set; }
        public int request_id { get; set; }
        public int type { get; set; }
        public string commentText { get; set; }
    }
    public class ViewModel_AprrovalRequestStatusDetails
    {
        public int id { get; set; }
        public string approval_no { get; set; }
        public string request_date { get; set; }
        public string category { get; set; }
        public string subcategory { get; set; }
        public string approval_title { get; set; }
        public string approval_desc { get; set; }
        public string approval_status { get; set; }
        public string action_date { get; set; }
        public string approval_name { get; set; }
        public string remarks { get; set; }
        public string action_take { get; set; }
    }
}
