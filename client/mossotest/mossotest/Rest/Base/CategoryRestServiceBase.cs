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
    public class CategoryRestServiceBase : RestClient
    {
        const string CategoryApi = "category/";

        //POST
        /// <summary>
        /// Add a new Category
        /// </summary>
        /// <param name="item">Category to Add</param>
        /// <returns>void</returns>
        public async Task POST(Category item)
        {
            try
            {   
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await Client.PostAsync(CategoryApi, content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //DELETE
        /// <summary>
        /// Delete a Category
        /// </summary>
        /// <param name="id">Id of the Category to Delete</param>
        /// <returns>void</returns>
        public async Task DELETE(string id)
        {
            try
            {
                var response = await Client.DeleteAsync(CategoryApi + id);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //GET ID
        /// <summary>
        /// Get a Category
        /// </summary>
        /// <returns>Category</returns>
        public async Task<Category> GETId(string categoryId)
        {
            Category category = new Category();
            try
            {
                var content = await Client.GetStringAsync(CategoryApi + categoryId);
                category = JsonConvert.DeserializeObject<Category>(content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return category;
        }

        //GET
        /// <summary>
        /// Get the complete list of Categorys
        /// </summary>
        /// <returns>Category List</returns>
        public async Task<ObservableCollection<Category>> GETList()
        {
            ObservableCollection<Category> categorylist = new ObservableCollection<Category>();
            try
            {
                var content = await Client.GetStringAsync(CategoryApi);
                categorylist = JsonConvert.DeserializeObject<ObservableCollection<Category>>(content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return categorylist;
        }

        //PUT
        /// <summary>
        /// Update info of a Category
        /// </summary>
        /// <param name="item">Category to Update</param>
        /// <returns></returns>
        public async Task PUT(Category item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await Client.PostAsync(CategoryApi + item.Id, content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }
    }
}
