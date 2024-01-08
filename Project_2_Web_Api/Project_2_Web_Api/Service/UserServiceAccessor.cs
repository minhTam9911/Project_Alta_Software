namespace Project_2_Web_Api.Service;

public interface UserServiceAccessor
{
	string GetCurrentUserId();
	bool IsUserAuthenticated();
	bool IsUserInRole(string role);
}
