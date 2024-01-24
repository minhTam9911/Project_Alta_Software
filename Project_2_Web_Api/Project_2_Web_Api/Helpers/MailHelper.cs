﻿using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using Twilio.TwiML.Messaging;

namespace Project_2_Web_Api.Helplers;
public class MailHelper
{
    private IConfiguration configuration;

    public MailHelper(IConfiguration _configuration)
    {
        configuration = _configuration;
    }

    public bool Send(string from, string to, string subject, string body)
    {
        try
        {
            var host = configuration["Gmail:Host"];
            var port = int.Parse(configuration["Gmail:Port"]);
            var username = configuration["Gmail:Username"];
            var password = configuration["Gmail:Password"];
            var enable = bool.Parse(configuration["Gmail:SMTP:starttls:enable"]);
            var smtpClient = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = enable,
                Credentials = new NetworkCredential(username, password)
            };

            var mailMessage = new MailMessage(from, to);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
			/*using (var stream = new FileStream(configuration["LogoPath"], FileMode.Open, FileAccess.Read))
			{
				mailMessage.Attachments.Add(new Attachment(stream, "CDExcellent.png"));
				mailMessage.Attachments.Add(new Attachment(stream, "signature.png"));
			}*/

			smtpClient.Send(mailMessage);

            return true;
        }
        catch(Exception ex)
        {
            Debug.WriteLine("Email---------"+ ex);
            return false;
        }
    }
    public static string HtmlVerify(string code)
    {
        return "\r\n    <table style=\"background-color:#fff;border:0;border-collapse:collapse;border-color:#ddd;border-spacing:0;border-style:solid;border-width:1px;box-sizing:inherit;font:inherit;font-size:100%;margin:0 auto;max-width:680px;padding:0;text-align:center;vertical-align:baseline;width:100%\">\r\n        <tbody style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:baseline\">\r\n            <tr style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:baseline\">\r\n                <td style=\"border:0;border-bottom-color:#ddd;border-bottom-style:solid;border-bottom-width:1px;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:middle\" align=\"left\">\r\n                    <div style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:20px;padding:0;vertical-align:baseline\">\r\n                        <img width=\"119\" height=\"35\" style=\"border:0;border-style:none;box-sizing:inherit;font:inherit;font-size:100%;margin:0;outline:none;padding:0;text-decoration:none;vertical-align:baseline\" src=\"https://iili.io/Jc931pe.png\" >\r\n                    </div>\r\n                </td>\r\n            </tr>\r\n            <tr style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:baseline\">\r\n                <td style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:middle\">\r\n                    <table style=\"border:0;border-collapse:collapse;border-spacing:0;box-sizing:inherit;display:inline-block;font:inherit;font-size:100%;margin:0;padding:10px 20px;text-align:center;vertical-align:baseline;width:unset\">\r\n                        <tbody style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:baseline\">\r\n                            <tr style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:baseline\">\r\n                                <td style=\"border:0;border-bottom-color:#ddd;border-bottom-style:solid;border-bottom-width:1px;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:middle\">\r\n                                    <table style=\"border:0;border-collapse:collapse;border-spacing:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;max-width:558px;padding:0;text-align:center;vertical-align:baseline;width:100%\">\r\n                                        <tbody style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:baseline\">\r\n                                            <tr style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:baseline\">\r\n                                                <td style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;height:60px;margin:0;padding:0;vertical-align:middle\" align=\"left\">\r\n                                                    <p style=\"border:0;box-sizing:inherit;color:rgb(34,34,34);font:inherit;font-size:20px;font-weight:bold;line-height:20px;margin:0;padding:0;vertical-align:baseline\">Verify your email address</p>\r\n                                                </td>\r\n                                            </tr>\r\n                                            <tr style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:baseline\">\r\n                                                <td style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;height:50px;margin:0;padding:0;vertical-align:middle\" align=\"left\">\r\n                                                    <p style=\"border:0;box-sizing:inherit;color:rgb(34,34,34);font:inherit;font-size:14px;font-weight:300;line-height:20px;margin:0;padding:0;vertical-align:baseline\">You need to verify your email address to continue using your CDExcellent account. Enter the following code to verify your email address:</p>\r\n                                                </td>\r\n                                            </tr>\r\n                                            <tr style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:baseline\">\r\n                                                <td style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;height:60px;margin:0;padding:0;vertical-align:middle\" align=\"left\">\r\n                                                    <p style=\"border:0;box-sizing:inherit;color:rgb(34,34,34);font:inherit;font-size:20px;font-weight:bold;line-height:20px;margin:0;padding:0;vertical-align:baseline\">\r\n                                                        <span id=\"m_-8483616267167387556verification-code\">" + code+"</span>\r\n                                                    </p>\r\n                                                </td>\r\n                                            </tr>\r\n                                        </tbody>\r\n                                    </table>\r\n                                </td>\r\n                            </tr>\r\n                        </tbody>\r\n                    </table>\r\n                </td>\r\n            </tr>\r\n        </tbody>\r\n    </table>\r\n   ";
    }
    public static string HtmlNewAccount(string fullname,string username, string password)
    {
        return "\r\n<table style=\"background-color:#fff;border:0;border-collapse:collapse;border-color:#ddd;border-spacing:0;border-style:solid;border-width:1px;box-sizing:inherit;font:inherit;font-size:100%;margin:0 auto;max-width:680px;padding:0;text-align:center;vertical-align:baseline;width:100%\">\r\n    <tbody style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:baseline\">\r\n        <tr style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:baseline\">\r\n            <td style=\"border:0;border-bottom-color:#ddd;border-bottom-style:solid;border-bottom-width:1px;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:middle\" align=\"left\">\r\n                <div style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:20px;padding:0;vertical-align:baseline\">\r\n                    <img width=\"119\" height=\"35\" style=\"border:0;border-style:none;box-sizing:inherit;font:inherit;font-size:100%;margin:0;outline:none;padding:0;text-decoration:none;vertical-align:baseline\" src=\"https://iili.io/Jc931pe.png\" alt=\"\" class=\"\" data-bit=\"\">\r\n                </div>\r\n            </td>\r\n        </tr>\r\n        <tr style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:baseline\">\r\n            <td style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:middle\">\r\n                <table style=\"border:0;border-collapse:collapse;border-spacing:0;box-sizing:inherit;display:inline-block;font:inherit;font-size:100%;margin:0;padding:10px 20px;text-align:center;vertical-align:baseline;width:unset\">\r\n                    <tbody style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:baseline\">\r\n                        <tr style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:baseline\">\r\n                            <td style=\"border:0;border-bottom-color:#ddd;border-bottom-style:solid;border-bottom-width:1px;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:middle\">\r\n                                <table style=\"border:0;border-collapse:collapse;border-spacing:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;max-width:558px;padding:0;text-align:center;vertical-align:baseline;width:100%\">\r\n                                    <tbody style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:baseline\">\r\n                                        <tr style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:baseline\">\r\n                                            <td style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;height:60px;margin:0;padding:0;vertical-align:middle\" align=\"left\">\r\n                                                <p style=\"border:0;box-sizing:inherit;color:rgb(34,34,34);font:inherit;font-size:20px;font-weight:bold;line-height:20px;margin:0;padding:0;vertical-align:baseline\">Hello " + fullname+"</p>\r\n                                            </td>\r\n                                        </tr>\r\n                                        <tr style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:baseline\">\r\n                                            <td style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;height:50px;margin:0;padding:0;vertical-align:middle\" align=\"left\">\r\n                                                <p style=\"border:0;box-sizing:inherit;color:rgb(34,34,34);font:inherit;font-size:14px;font-weight:300;line-height:20px;margin:0;padding:0;vertical-align:baseline\">We are happy that you have chosen and trusted our company. Below is your account and temporary password.</p>\r\n                                            </td>\r\n                                        </tr>\r\n                                        <tr style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;margin:0;padding:0;vertical-align:baseline\">\r\n                                            <td style=\"border:0;box-sizing:inherit;font:inherit;font-size:100%;height:60px;margin:0;padding:0;vertical-align:middle\" align=\"left\">\r\n                                                <p style=\"border:0;box-sizing:inherit;color:rgb(34,34,34);font:inherit;font-size:20px;font-weight:bold;line-height:20px;margin:0;padding:0;vertical-align:baseline\">\r\n                                                    <span id=\"m_-8483616267167387556verification-code\">Username : "+username+"</span> \r\n                                                </p>\r\n                                                <p style=\"border:0;box-sizing:inherit;color:rgb(34,34,34);font:inherit;font-size:20px;font-weight:bold;line-height:20px;margin:0;padding:0;vertical-align:baseline;margin-top: 10px;\">\r\n                                                    <span id=\"m_-8483616267167387556verification-code\">Password : "+password+"</span> \r\n                                                </p>\r\n                                            </td>\r\n                                        </tr>\r\n                                    </tbody>\r\n                                </table>\r\n                            </td>\r\n                        </tr>\r\n                    </tbody>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </tbody>\r\n</table>\r\n";
    }
    public static string HtmlNotification(string fullname, string content)
    {
        return
			"\r\n<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout:fixed;background-color:#f9f9f9\" id=\"bodyTable\">\r\n  <tbody>\r\n    <tr>\r\n      <td style=\"padding-right:10px;padding-left:10px;\" align=\"center\" valign=\"top\" id=\"bodyCell\">\r\n        <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"wrapperWebview\" style=\"max-width:600px\">\r\n          <tbody>\r\n            <tr>\r\n              <td align=\"center\" valign=\"top\">\r\n                <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n                  <tbody>\r\n                    <tr>\r\n                      <td style=\"padding-top: 20px; padding-bottom: 20px; padding-right: 0px;\" align=\"right\" valign=\"middle\" class=\"webview\"> \r\n                      </td>\r\n                    </tr>\r\n                  </tbody>\r\n                </table>\r\n              </td>\r\n            </tr>\r\n          </tbody>\r\n        </table>\r\n        <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"wrapperBody\" style=\"max-width:600px\">\r\n          <tbody>\r\n            <tr>\r\n              <td align=\"center\" valign=\"top\">\r\n                <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"tableCard\" style=\"background-color:#fff;border-color:#e5e5e5;border-style:solid;border-width:0 1px 1px 1px;\">\r\n                  <tbody>\r\n                    <tr>\r\n                      <td style=\"background-color:#00d2f4;font-size:1px;line-height:3px\" class=\"topBorder\" height=\"3\">&nbsp;</td>\r\n                    </tr>\r\n                    <tr>\r\n                      <td style=\"padding-top: 60px; padding-bottom: 20px;\" align=\"center\" valign=\"middle\" class=\"emailLogo\">\r\n                        <a href=\"#\" style=\"text-decoration:none\" target=\"_blank\">\r\n                          <img alt=\"\" border=\"0\" src=\"https://iili.io/Jc931pe.png\" style=\"width:100%;max-width:150px;height:auto;display:block\" width=\"150\">\r\n                        </a>\r\n                      </td>\r\n                    </tr>\r\n                    <tr>\r\n                   \r\n                    <tr>\r\n                      <td style=\"padding-bottom: 5px; padding-left: 20px; padding-right: 20px;\" align=\"center\" valign=\"top\" class=\"mainTitle\">\r\n                        <h2 class=\"text\" style=\"color:#000;font-family:Poppins,Helvetica,Arial,sans-serif;font-size:28px;font-weight:500;font-style:normal;letter-spacing:normal;line-height:36px;text-transform:none;text-align:center;padding:0;margin:0\">Hi \"" + fullname+"\"</h2>\r\n                      </td>\r\n                    </tr>\r\n                    <tr>\r\n                      <td style=\"padding-bottom: 30px; padding-left: 20px; padding-right: 20px;\" align=\"center\" valign=\"top\" class=\"subTitle\">\r\n                        <h4 class=\"text\" style=\"color:#999;font-family:Poppins,Helvetica,Arial,sans-serif;font-size:16px;font-weight:500;font-style:normal;letter-spacing:normal;line-height:24px;text-transform:none;text-align:center;padding:0;margin:0\"></h4>\r\n                      </td>\r\n                    </tr>\r\n                    <tr>\r\n                      <td style=\"padding-left:20px;padding-right:20px\" align=\"center\" valign=\"top\" class=\"containtTable ui-sortable\">\r\n                        <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"tableDescription\" style=\"\">\r\n                          <tbody>\r\n                            <tr>\r\n                              <td style=\"padding-bottom: 20px;\" align=\"center\" valign=\"top\" class=\"description\">\r\n                                <p class=\"text\" style=\"color:#666;font-family:'Open Sans',Helvetica,Arial,sans-serif;font-size:14px;font-weight:400;font-style:normal;letter-spacing:normal;line-height:22px;text-transform:none;text-align:center;padding:0;margin:0\">"+content+ "</p>\r\n                              </td>\r\n                            </tr>\r\n                          </tbody>\r\n                        </table>\r\n                        <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"tableButton\" style=\"\">\r\n                      </td>\r\n                    </tr>\r\n                    <tr>\r\n                      <td style=\"font-size:1px;line-height:1px\" height=\"20\">&nbsp;</td>\r\n                    </tr>\r\n                    <tr>\r\n                      <td align=\"center\" valign=\"middle\" style=\"padding-bottom: 40px;\" class=\"emailRegards\">\r\n                        <!-- Image and Link // -->\r\n                        <a href=\"#\" target=\"_blank\" style=\"text-decoration:none;\">\r\n                          <img mc:edit=\"signature\" src=\"https://iili.io/Jc9F4Du.png\" alt=\"\" width=\"150\" border=\"0\" style=\"width:100%;\r\n    max-width:150px; height:auto; display:block;\">\r\n                        </a>\r\n                      </td>\r\n                    </tr>\r\n                  </tbody>\r\n                </table>\r\n                <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"space\">\r\n                  <tbody>\r\n                    <tr>\r\n                      <td style=\"font-size:1px;line-height:1px\" height=\"30\">&nbsp;</td>\r\n                    </tr>\r\n                  </tbody>\r\n                </table>\r\n              </td>\r\n            </tr>\r\n          </tbody>\r\n        </table>\r\n              </td>\r\n            </tr>\r\n            <tr>\r\n              <td style=\"font-size:1px;line-height:1px\" height=\"30\">&nbsp;</td>\r\n            </tr>\r\n          </tbody>\r\n        </table>\r\n      </td>\r\n    </tr>\r\n  </tbody>\r\n</table>";
    }
}
