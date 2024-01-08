namespace Project_2_Web_Api.Service.Impl;

public class UserServiceAccessorImpl : UserServiceAccessor
{
	private readonly IConfiguration _configuration;
	private readonly HttpClient _httpClient;
	private  IHttpContextAccessor _httpContextAccessor;
	public UserServiceAccessorImpl(
		IConfiguration configuration,
		HttpClient httpClient,
		IHttpContextAccessor httpContextAccessor)
	{
		_configuration = configuration;
		_httpClient = httpClient;
		_httpContextAccessor = httpContextAccessor;
	}

	public string GetCurrentUserId()
	{
		throw new NotImplementedException();
	}

	public bool IsUserAuthenticated()
	{
		throw new NotImplementedException();
	}

	public bool IsUserInRole(string role)
	{
		throw new NotImplementedException();
	}
}
