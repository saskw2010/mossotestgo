using System;
using Newtonsoft.Json;
using mossotest.Support;

namespace mossotest.Models.Base
{
    public class UserBase : BaseBindableObject
    {
         // Id Start
		string _id;
		[JsonProperty(PropertyName = "_id")]
		public string Id
		{
			get { return _id; }
			set { SetValue(ref _id, value); }
		}
		 // Id End 
        
        string mail;
        [JsonProperty(PropertyName = "mail")]
        public string Mail
        {
            get { return mail; }
            set { SetValue(ref mail, value); }
        }
        
        string name;
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get { return name; }
            set { SetValue(ref name, value); }
        }
        
        string password;
        [JsonProperty(PropertyName = "password")]
        public string Password
        {
            get { return password; }
            set { SetValue(ref password, value); }
        }
        
        string surname;
        [JsonProperty(PropertyName = "surname")]
        public string Surname
        {
            get { return surname; }
            set { SetValue(ref surname, value); }
        }
        
        string username;
        [JsonProperty(PropertyName = "username")]
        public string Username
        {
            get { return username; }
            set { SetValue(ref username, value); }
        }
        
        string[] roles;
        [JsonProperty(PropertyName = "roles")]
        public string[] Roles
        {
            get { return roles; }
            set { SetValue(ref roles, value); }
        }
        

        string token;
        [JsonProperty(PropertyName = "token")]
        public string Token
        {
            get{ return token; }
            set{ SetValue(ref token, value); }
        }
    }
}