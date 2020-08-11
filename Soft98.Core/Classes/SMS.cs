using Kavenegar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soft98.Core.Classes
{
    public class SMS
    {
        public void Send(string To, string Body)
        {
            var sender = "10004346";
            var receptor = To;
            var message = Body;
            var api = new KavenegarApi("45324A5470316434744357506A66746650354462747A65457A6B6C6242794341");

            api.Send(sender, receptor, message);
        }
    }
}
