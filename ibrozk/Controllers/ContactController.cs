using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using ibrozk.Data.Abstract;
using ibrozk.Data.Concrete.EfCore;
using ibrozk.Entity;
using ibrozk.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ibrozk.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(Mail m)  //Mail sınıfından m diye bir değişken tanımlarız
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Burası aynı kalacak
                client.Credentials = new NetworkCredential("contactibrahimozkadir@gmail.com", "essy ytym qxnn mixv");
                client.EnableSsl = true;
                MailMessage msj = new MailMessage(); //Yeni bir MailMesajı oluşturuyoruz
                msj.From = new MailAddress(m.Email, m.Adsoyad + " "); //iletişim kısmında girilecek mail buaraya
                msj.To.Add("contactibrahimozkadir@gmail.com"); //Buraya kendi mail adresimizi yazıyoruz
                msj.Subject = m.Telefon + " " + m.Email; //Buraya iletişim sayfasında gelecek Telefon ve mail adresini mail içeriğine yazacaktır
                msj.Body = m.Mesaj; //Mail içeriği burada aktarılacakır
                client.Send(msj); //Clien sent kısmında gönderme işlemi gerçeklecektir.
                //Bu kısımdan itibaren sizden kullanıcıya gidecek mail bilgisidir 
                MailMessage msj1 = new MailMessage();
                msj1.From = new MailAddress("contactibrahimozkadir@gmail.com", "New Message");
                msj1.To.Add(m.Email); //Buraua iletişim sayfasında gelecek mail adresi gelecktir.
                msj1.Subject = "Mail'inize Cevap";
                msj1.Body = "Size En kısa zamanda Döneceğiz. Teşekkür Ederiz Bizi tercih ettiğiniz için";
                client.Send(msj1);
                ViewBag.Succes = "teşekkürler Mailniz başarı bir şekilde gönderildi"; //Bu kısımlarda ise kullanıcıya bilgi vermek amacı ile olur
                return View();
            }
            catch (Exception)
            {
                ViewBag.Error = "Mesaj Gönderilken hata olıuştu"; //Bu kısımlarda ise kullanıcıya bilgi vermek amacı ile olur
                return View();
            }
        }

    }
}