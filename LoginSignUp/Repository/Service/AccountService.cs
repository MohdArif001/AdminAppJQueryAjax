using LoginSignUp.Models;
using LoginSignUp.Repository.Interface;
using LoginSignUp.Utils.Enums;
using LoginSignUp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LoginSignUp.Repository.Service
{
    public class AccountService :IUsers
    {
        private ApplicationDbContext _dbContext;
        public AccountService()
        {
            _dbContext = new ApplicationDbContext();
        }
        public SignInEnum SignIn(SignInModel model)
        {
            var user = _dbContext.Users.SingleOrDefault(e => e.Email == model.Email && e.Password == model.Password);
            if (user != null)
            {
                if (user.IsVerified)
                {
                    if (user.IsActive)
                    {
                        return SignInEnum.Success;
                    }
                    else
                    {
                        return SignInEnum.InActive;
                    }
                }
                else
                {
                    return SignInEnum.NotVerified;
                }
            }
            else
            {
                return SignInEnum.WrongCredentials;
            }
        }

        public SignUpEnum SignUp(SignUpModel model)
        {
            if (_dbContext.Users.Any(e => e.Email == model.Email))
            {
                return SignUpEnum.EmailExist;
            }
            else
            {
                var user = new Users()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.ConfirmPassword,
                    Gender = model.Gender
                };
                _dbContext.Users.Add(user);
                string Otp = GenerateOTP();
                SendMail(model.Email, Otp);
                var VAccount = new VerifyAccount()
                {
                    Otp = Otp,
                    UserId = model.Email,
                    SendTime = DateTime.Now
                };
                _dbContext.VerifyAccount.Add(VAccount);
                _dbContext.SaveChanges();
                return SignUpEnum.Success;
            }
            return SignUpEnum.Failure;

        }
        private void SendMail(string to, string Otp)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress("ariffuzu001@gmail.com");
            mail.Subject = "Verify Your Account";
            string Body = $"Your OTP is <b> {Otp}</b>  <br/>thanks for choosing us.";
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("ariffuzu001@gmail.com", "arif001@#"); // Enter seders User name and password  
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
        private string GenerateOTP()
        {
            var chars = "0123456789";
            var random = new Random();
            var list = Enumerable.Repeat(0, 5).Select(x => chars[random.Next(chars.Length)]);
            var r = string.Join("", list);
            return r;
        }

        public bool VerifyAccount(string Otp)
        {
            if (_dbContext.VerifyAccount.Any(e => e.Otp == Otp))
            {
                var Acc = _dbContext.VerifyAccount.SingleOrDefault(e => e.Otp == Otp);
                var User = _dbContext.Users.SingleOrDefault(e => e.Email == Acc.UserId);
                User.IsVerified = true;
                User.IsActive = true;

                _dbContext.VerifyAccount.Remove(Acc);
                _dbContext.Users.Update(User);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ForgotPassEnum ForgotPassword(ForgotpasswordModel model)
        {
            if (_dbContext.Users.Any(e => e.Email == model.Email))
            {
                string Otp = GenerateOTP();
                SendMail(model.Email, Otp);
                var VAccount = new VerifyAccount()
                {
                    Otp = Otp,
                    UserId = model.Email,
                    SendTime = DateTime.Now
                };
                _dbContext.VerifyAccount.Add(VAccount);
                return ForgotPassEnum.Success;
            }
            else
            {
                return ForgotPassEnum.Failure;
            }

        }

    }
}
