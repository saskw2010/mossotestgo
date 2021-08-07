using System;
using Newtonsoft.Json;
using mossotest.Support;

namespace mossotest.Models.Base
{
    public class ProductBase : BaseBindableObject
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
        
        int catid;
        [JsonProperty(PropertyName = "catid")]
        public int Catid
        {
            get { return catid; }
            set { SetValue(ref catid, value); }
        }
        
        string productname;
        [JsonProperty(PropertyName = "productname")]
        public string Productname
        {
            get { return productname; }
            set { SetValue(ref productname, value); }
        }
        
        string catpro;
        [JsonProperty(PropertyName = "catpro")]
        public string Catpro
        {
            get { return catpro; }
            set { SetValue(ref catpro, value); }
        }
        
    }
}