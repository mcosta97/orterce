using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ProyectoTallerData {
    public class daUtils {
        public static void EnviarMail(string asunto, string mensaje, string mail) {
            try {
                new SmtpClient {
                    Host = "smtp.gmail.com", Port = 587, EnableSsl = true, Timeout = 10000,
                    DeliveryMethod = SmtpDeliveryMethod.Network, UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("orterce@gmail.com", "orterce2017")
                }.Send(new MailMessage { From = new MailAddress("orterce@gmail.com", "Orterce Soporte"), To = { mail }, Subject = asunto, Body = mensaje, BodyEncoding = Encoding.UTF8 });
            Console.WriteLine("Mail enviado!");
            } catch (Exception ex) {
                Console.WriteLine("Error: " + ex);
            }
        }

        public static bool IsNumeric(string s) {
            float output;
            return float.TryParse(s, out output);
        }

        public static string Encriptar(string cadena) {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(cadena);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public static string DesEncriptar(string cadena) {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(cadena);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
    }
}
