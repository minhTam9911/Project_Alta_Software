using Microsoft.Extensions.Options;
using Project_2_Web_Api.Configurations;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Project_2_Web_Api.Service.Impl;

public class SmsServiceImpl : SmsService
{
	private readonly TwilioSettings twilio;
	public SmsServiceImpl( IOptions<TwilioSettings> twilio)
	{
		this.twilio = twilio.Value ;
	}

	public MessageResource Send(string mobileNumber, string body)
	{
		TwilioClient.Init(twilio.AccountSID, twilio.AuthToken);
		var message = MessageResource.Create(
			body: body,
			from: new Twilio.Types.PhoneNumber(twilio.TwilioPhoneNumber),
			to: mobileNumber
			);
		return message;
	}
}
