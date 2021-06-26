using Common_Layer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using FundooMSMQ;
using Microsoft.EntityFrameworkCore;

namespace Repository_Layer.Services
{
    public class UserR : IUserRL
    {
        IList<User> Users = new List<User>();
        public bool SampleUserApi(User user)
        {
            try
            {
                Users.Add(user);
                if (Users.Contains(user) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private UserContext _userDbContext;
        private readonly IConfiguration configuration;
        private const string SECRET_KEY = "SuperSecretKey@345fghhhhhhhhhhhhhhhhhhhhhhhhhhhhhfggggggg";

        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(UserR.SECRET_KEY));
        public UserR(UserContext userDbContext, IConfiguration configuration)
        {
            _userDbContext = userDbContext;
            this.configuration = configuration;
        }
        public User AddUser(User user)
        {
            _userDbContext.Users.Add(user);
            user.Password = EncryptPassword(user.Password);
            _userDbContext.SaveChanges();
            return user;
        }

        public static string EncryptPassword(string password)
        {
            try
            {
                byte[] encryptData = new byte[password.Length];
                encryptData = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encryptData);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        /// <summary>
        /// method to login
         
        /// <param name="email"></email>
        /// <param name="password"></password>
        /// <returns></returns>

        /// <summary>
        /// method to generate token
        /// </summary>
        /// <param name="Email"></Email>
        /// <returns></returns>
        public string Login(string email, string password)
        {
            string encodedPassword = EncryptPassword(password);
            var result = _userDbContext.Users.FirstOrDefault(u => u.Email == email && u.Password == encodedPassword);
            if (result == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("HelloThisTokenIsGeneretedByMe");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim ("ID", result.ID.ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
       /* public string Login(string email, string password)
        {
            try
            {
                string message;
                string encodedPassword = EncryptPassword(password);
                var result = _userDbContext.Users.FirstOrDefault(u => u.Email == email && u.Password == encodedPassword);

                if (result != null)
                {
                    message = "LOGIN SUCCESS";
                }
                else
                {
                    message = "LOGIN UNSUCCESSFUL, Email Or Password is Wrong";
                }

                return message;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }*/

        public string GenerateToken(string Email)
        {
            try
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Email, Email)
                        }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:SecureKey"])), SecurityAlgorithms.HmacSha256)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return token;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }
        public bool ForgotPassword(string email)
        {
            try
            {
                bool result;
                string user;
                string mailSubject = "Link to reset your FundooNotes App Credentials";
                var userCheck = _userDbContext.Users.SingleOrDefault(x => x.Email == email);
                if (userCheck != null)
                {
                    Sender sender = new Sender();
                    sender.SendMessage();
                    Receiver receiver = new Receiver();
                    var messageBody = receiver.receiverMessage();
                    user = messageBody;
                    using (MailMessage mailMessage = new MailMessage("donot.copyme@gmail.com", email))
                    {
                        mailMessage.Subject = mailSubject;
                        mailMessage.Body = user;
                        mailMessage.IsBodyHtml = true;
                        SmtpClient Smtp = new SmtpClient();
                        Smtp.Host = "smtp.gmail.com";
                        Smtp.EnableSsl = true;
                        Smtp.UseDefaultCredentials = false;
                        Smtp.Credentials = new NetworkCredential("donot.copyme@gmail.com", "HeyListen@852");
                        Smtp.Port = 587;
                        Smtp.Send(mailMessage);
                    }

                    result = true;
                    return result;
                }

                result = false;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to reset old user password with new one.
        /// </summary>
        /// <param name="resetPassword">variable of type ResetPasswordModel</param>
        /// <returns>boolean result</returns>
        public bool ResetPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                bool result;
                string encodedPassword = EncryptPassword(resetPassword.Password);
                var userPassword = _userDbContext.Users.SingleOrDefault(x => x.Email == resetPassword.Email);
                if (userPassword != null)
                {
                    userPassword.Password = encodedPassword;
                    _userDbContext.Entry(userPassword).State = EntityState.Modified;
                    _userDbContext.SaveChanges();

                    result = true;
                    return result;
                }

                result = false;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
