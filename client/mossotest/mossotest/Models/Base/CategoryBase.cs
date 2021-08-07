using System;
using Newtonsoft.Json;
using mossotest.Support;

namespace mossotest.Models.Base
{
    public class CategoryBase : BaseBindableObject
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
        
        string name;
        [JsonProperty(PropertyName = "Name")]
        public string Name
        {
            get { return name; }
            set { SetValue(ref name, value); }
        }
        
        public string QualifiedName
        {
            get { return $"{ Name }"; }
        }
    }
}