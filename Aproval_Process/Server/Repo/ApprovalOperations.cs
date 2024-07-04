using Server.Controllers;
using Server.Helper;
using Server.Model;
using System.Data;
using System.Data.SqlClient;


namespace Server.Repo
{
    public class ApprovalOperations :IApproval
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApprovalController> _logger;
        public ApprovalOperations(ILogger<ApprovalController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }
        #region Auth
        public async Task<int> loginUser(string Username,string Password)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("[sp_LoginUser]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", Username);
                    command.Parameters.AddWithValue("@Password", Password);
                    await connection.OpenAsync();
                    var result = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(result);
                }
            }
        }
        #endregion Auth
        #region Request Approval
        public async Task<string> GetApprovalNumber()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                var res = "";
                try
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("[sp_generateApproval_No]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        var result = await command.ExecuteScalarAsync();
                        if (result != null)
                        {
                            res = result.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in GetHostelID: {ex.Message}");
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
                return res;
            }
        }
        public async Task<int> RequestApproval(ViewModel_RequestApproval model)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("sp_SaveOrUpdateRequestApproval", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", model.id);
                    command.Parameters.AddWithValue("@approval_no", model.approval_no);
                    command.Parameters.AddWithValue("@request_date", model.request_date);
                    command.Parameters.AddWithValue("@due_date", model.due_date);
                    command.Parameters.AddWithValue("@category_id", model.category_id);
                    command.Parameters.AddWithValue("@subcategory_id", model.subcategory_id);
                    command.Parameters.AddWithValue("@approval_title", model.approval_title);
                    command.Parameters.AddWithValue("@approval_desc", model.approval_desc);
                    command.Parameters.AddWithValue("@approval_remarks", model.approval_remarks);
                    command.Parameters.AddWithValue("@user", model.user);
                    command.Parameters.AddWithValue("@user_id", model.user_id);
                    await connection.OpenAsync();
                    var result = await command.ExecuteScalarAsync();
                    // Converting result to int
                    return Convert.ToInt32(result);
                }
            }
        }
        public async Task<List<ViewModel_ApprovalRequestsDetails>> ApprovalRequestDetails(int userid)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                List<ViewModel_ApprovalRequestsDetails> model = new List<ViewModel_ApprovalRequestsDetails>();
                using (var command = new SqlCommand("usp_Get_RequestDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@user_id", userid);
                    await connection.OpenAsync();
                    DataSet ds = new DataSet();
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = command;
                    sda.Fill(ds);
                    connection.Close();
                    model = CommonMethods.ConvertToList<ViewModel_ApprovalRequestsDetails>(ds.Tables[0]);
                    return model;
                }
            }
        }
        public async Task<List<ViewModel_AprrovalRequestStatusDetails>> ApprovalRequestStausDetails(int request_id)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                List<ViewModel_AprrovalRequestStatusDetails> model = new List<ViewModel_AprrovalRequestStatusDetails>();
                using (var command = new SqlCommand("usp_GetApprovalStatus", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@request_id", request_id);
                    await connection.OpenAsync();
                    DataSet ds = new DataSet();
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = command;
                    sda.Fill(ds);
                    connection.Close();
                    model = CommonMethods.ConvertToList<ViewModel_AprrovalRequestStatusDetails>(ds.Tables[0]);
                    return model;
                }
            }
        }
        public async Task<int> ApprovalActions(int user_id, int request_id , int type, string remarks)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("usp_Perform_Approve_Sendback_Reject_op", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@request_id", request_id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@remark", remarks);
                    await connection.OpenAsync();
                    var result = await command.ExecuteScalarAsync();
                    // Converting result to int
                    return Convert.ToInt32(result);
                }
            }
        }
        #endregion Request Approval
    }
}
