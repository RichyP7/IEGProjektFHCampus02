using BusinessLogic.Core.Entities;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Formatters
{
    /// <summary>
    /// Based on https://code-maze.com/content-negotiation-dotnet-core/
    /// </summary>
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(Payment).IsAssignableFrom(type) || typeof(IEnumerable<Payment>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<Payment>)
            {
                foreach (var Payment in (IEnumerable<Payment>)context.Object)
                {
                    FormatCsv(buffer, Payment);
                }
            }
            else
            {
                FormatCsv(buffer, (Payment)context.Object);
            }

            using (var writer = context.WriterFactory(response.Body, selectedEncoding))
            {
                return writer.WriteAsync(buffer.ToString());
            }
        }

        private static void FormatCsv(StringBuilder buffer, Payment Payment)
        {
            buffer.AppendLine($"{Payment.Id};{Payment.Name};{Payment.Description}");
        }
    }
}
