using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ASP.NET_MVC_FormVerification.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "user")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "user")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize(Roles = "user")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string name, string password)
        {
            if (name == "admin" && password == "admin")
            {
                string roles = "user";
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, name, DateTime.UtcNow, DateTime.UtcNow.AddDays(7), false, roles, "/");
                //加密序列化验证票为字符串
                string hashTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie userCookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashTicket);
                Response.Cookies.Add(userCookie);
                return RedirectToAction("Index");
            }
            else
            {
                return Content("登录失败");
            }

        }
        [Authorize(Roles = "user")]
        public ActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendMail(string str)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add("372364996@qq.com");
            msg.To.Add("15822771484@163.com");


            msg.From = new MailAddress("372364996@qq.com", "发件人", System.Text.Encoding.UTF8);


            msg.Subject = "这是测试邮件";//邮件标题 
            msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码 
            msg.Body = str;//邮件内容 
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码 
            msg.IsBodyHtml = false;//是否是HTML邮件 
            msg.Priority = MailPriority.High;//邮件优先级


            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("********@qq.com", "************");
            //注册的邮箱和密码 
            client.Host = "smtp.qq.com";
            object userState = msg;
            try
            {
                client.Send(msg);
                return Content("发送成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Content("发送失败");
            }

        }
    }
}