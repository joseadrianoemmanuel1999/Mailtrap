// Looking to send emails in production? Check out our Email API/SMTP product!
using System.Net;
using System.Net.Mail;

var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
{
    Credentials = new NetworkCredential("5a84d8ec850539", "8e85621418e063"),
    EnableSsl = true
};
client.Send("joseadriano066@gmail.com", "bitmarketao@gmail.com", "Hello world", "testbody");

System.Console.WriteLine("Sent");