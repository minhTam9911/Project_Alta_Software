using Twilio.Rest.Api.V2010.Account;

namespace Project_2_Web_Api.Service;

public interface SmsService
{

	MessageResource Send(string mobileNumber, string body);

}
