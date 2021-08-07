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
    public class ProductRestServiceBase : RestClient
    {
        const string ProductApi = "product/";

        //POST
        /// <summary>
        /// Add a new Product
        /// </summary>
        /// <param name="item">Product to Add</param>
        /// <returns>void</returns>
        public async Task POST(Product item)
        {
            try
            {   
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await Client.PostAsync(ProductApi, content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //DELETE
        /// <summary>
        /// Delete a Product
        /// </summary>
        /// <param name="id">Id of the Product to Delete</param>
        /// <returns>void</returns>
        public async Task DELETE(string id)
        {
            try
            {
                var response = await Client.DeleteAsync(ProductApi + id);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //FindBycatpro
        /// <summary>
        /// Get a  Product using FindBycatpro
        /// </summary>
        /// <returns>Product</returns>
        public async Task<Product> findBycatpro(string id)
        {
            Product product = new Product();
            try
            {
                var content = await Client.GetStringAsync(ProductApi + "findBycatpro/" + id  );
                product = JsonConvert.DeserializeObject<Product>(content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return product;
        }

        //GET ID
        /// <summary>
        /// Get a Product
        /// </summary>
        /// <returns>Product</returns>
        public async Task<Product> GETId(string productId)
        {
            Product product = new Product();
            try
            {
                var content = await Client.GetStringAsync(ProductApi + productId);
                product = JsonConvert.DeserializeObject<Product>(content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return product;
        }

        //GET
        /// <summary>
        /// Get the complete list of Products
        /// </summary>
        /// <returns>Product List</returns>
        public async Task<ObservableCollection<Product>> GETList()
        {
            ObservableCollection<Product> productlist = new ObservableCollection<Product>();
            try
            {
                var content = await Client.GetStringAsync(ProductApi);
                productlist = JsonConvert.DeserializeObject<ObservableCollection<Product>>(content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return productlist;
        }

        //PUT
        /// <summary>
        /// Update info of a Product
        /// </summary>
        /// <param name="item">Product to Update</param>
        /// <returns></returns>
        public async Task PUT(Product item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await Client.PostAsync(ProductApi + item.Id, content);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }
    }
}
