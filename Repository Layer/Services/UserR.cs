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
        private const string SECRET_KEY = "SuperSecretKey@345fghfghgfhgfhfghfghf";

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
        /// </summary>
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
                    new Claim(ClaimTypes.Email, email)

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

        public bool SendEmail(string emailAddress)
        {
            try
            {
                MailMessage message = new MailMessage();
                //SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("dikole.shubh@gmail.com");
                message.To.Add(new MailAddress(emailAddress));
                message.Subject = "forget password link";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "body";
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("dikole.shubh@gmail.com", "Pass@123")
                };
                //smtp.Port = 587;
                //smtp.Host = "smtp.gmail.com"; //for gmail host  
                //smtp.EnableSsl = true;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = new NetworkCredential("nurainkk0110@gmail.com", "nurainkk@0110");
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

       
        public bool ResetPassword(ResetPasswordModel resetModel)
        {
            try
            {
                var user = this._userDbContext.Users.Where(x => x.Email == resetModel.Email).SingleOrDefault();
                if (resetModel != null && user != null)
                {
                    user.Password = resetModel.NewPassword;
                    this._userDbContext.Users.Update(user);
                    return true;
                }

                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
