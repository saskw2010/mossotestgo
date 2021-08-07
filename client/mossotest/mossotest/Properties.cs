using System;

namespace mossotest
{
    public static class Properties
    {
        // replace my-domain with the address of your client server
        public static Uri Endpoint { get; private set; } = new Uri("http://my-domain:3000/api/");
    }
}
