using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Project_2_Web_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{ 
}
/*
	using System.Net.Http.Json;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Common.Utility;
using Wata.Commerce.Account.Dtos;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace Wata.Commerce.Account.Client.Services
{
	public class UserClient : IUserClient
	{
		#region Fields
		private readonly IConfiguration _configuration;
		private readonly HttpClient _httpClient;
		private readonly string _apiUrl;
		#endregion

		#region Constructors
		public UserClient(
			IConfiguration configuration,
			HttpClient httpClient)
		{
			_configuration = configuration;
			_httpClient = httpClient;
			//_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			_apiUrl = _configuration["Urls:AccountUrl"];
		}
		#endregion

		#region Insert
		public async Task<UserDto?> InsertAsync(UserRequestDto requestDto)
		{
			using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, _apiUrl + "/api/Account/user"))
			{
				requestMessage.Content = JsonContent.Create(requestDto);

				using (HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage))
				{
					responseMessage.EnsureSuccessStatusCode();
					if (responseMessage.StatusCode != HttpStatusCode.NoContent)
					{
						return await responseMessage.Content.ReadFromJsonAsync<UserDto>();
					}
				}
			}

			return null;
		}
		#endregion

		#region Update
		public async Task<int> UpdateAsync(UserRequestDto requestDto, string id)
		{
			using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, _apiUrl + "/api/Account/user/" + id))
			{
				requestMessage.Content = JsonContent.Create(requestDto);

				using (HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage))
				{
					responseMessage.EnsureSuccessStatusCode();
					if (responseMessage.StatusCode != HttpStatusCode.NoContent)
					{
						return await responseMessage.Content.ReadFromJsonAsync<int>();
					}
				}
			}

			return 0;
		}
		#endregion

		#region Delete
		public async Task<int> DeleteAsync(string id)
		{
			using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, _apiUrl + "/api/Account/user/" + id))
			{
				using (HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage))
				{
					responseMessage.EnsureSuccessStatusCode();
				}
*/