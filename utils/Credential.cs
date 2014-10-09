using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Utils
{
    public class Credential
    {
        [Key]
        public string UserEmail { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public Credential() { }

        public Credential(string pUserEmail, string pAccessToken, string pRefreshToken)
        {
            this.UserEmail = pUserEmail;
            this.AccessToken = pAccessToken;
            this.RefreshToken = pRefreshToken;
        }
    }
}
