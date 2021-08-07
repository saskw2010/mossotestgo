using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using mossotest.Models;
using Newtonsoft.Json;

namespace mossotest.Rest.Base
{
    public class UserRestServiceBase : RestClient
    {
        const string UserApi = "user/";

        //CHANGEPASSWORD
        /// <summary>
        ///This is your API     
        ///</summary>
        public async Task changePassword()
        {

        }

        //POST
        /// <summary>
        /// Add a new User
        /// </summary>
        /// <param name="item">User to Add</param>
        /// <returns>void</returns>
        public async Task POST(User item)
        {
            try
            {   item.Password = App.LoginService.EncryptPassword(item.Password);
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await Client.PostAsync(UserApi, content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //DELETE
        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="id">Id of the User to Delete</param>
        /// <returns>void</returns>
        public async Task DELETE(string id)
        {
            try
            {
                var response = await Client.DeleteAsync(UserApi + id);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //GET ID
        /// <summary>
        /// Get a User
        /// </summary>
        /// <returns>User</returns>
        public async Task<User> GETId(string userId)
        {
            User user = new User();
            try
            {
                var content = await Client.GetStringAsync(UserApi + userId);
                user = JsonConvert.DeserializeObject<User>(content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return user;
        }

        //GET
        /// <summary>
        /// Get the complete list of Users
        /// </summary>
        /// <returns>User List</returns>
        public async Task<ObservableCollection<User>> GETList()
        {
            ObservableCollection<User> userlist = new ObservableCollection<User>();
            try
            {
                var content = await Client.GetStringAsync(UserApi);
                userlist = JsonConvert.DeserializeObject<ObservableCollection<User>>(content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return userlist;
        }

        //PUT
        /// <summary>
        /// Update info of a User
        /// </summary>
        /// <param name="item">User to Update</param>
        /// <returns></returns>
        public async Task PUT(User item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await Client.PostAsync(UserApi + item.Id, content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }
    }
}
