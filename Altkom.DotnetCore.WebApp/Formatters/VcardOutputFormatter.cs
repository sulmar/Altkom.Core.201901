using Altkom.DotnetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Altkom.DotnetCore.WebApp.Formatters
{
    
    public class VcardOutputFormatter : TextOutputFormatter
    {
        public VcardOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(Customer).IsAssignableFrom(type)
                || typeof(IEnumerable<Customer>).IsAssignableFrom(type))

            {
                return base.CanWriteType(type);
            }

            return false;
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;

            StringBuilder builder = new StringBuilder();

            if (context.Object is IEnumerable<Customer> customers)
            {
                foreach (Customer customer in customers)
                {
                    FormatVcard(builder, customer);
                }
            }
            else if (context.Object is Customer customer)
            {
                FormatVcard(builder, customer);
            }

            return response.WriteAsync(builder.ToString());

        }

        private static void FormatVcard(StringBuilder builder, Customer customer)
        {
            builder.AppendLine("BEGIN:VCARD");
            builder.AppendLine("VERSION:2.1");
            builder.AppendFormat($"N:{customer.FirstName}\r\n");
            builder.AppendFormat($"FN:{customer.LastName}\r\n");
            builder.AppendLine("END:VCARD");
        }
    }
}
