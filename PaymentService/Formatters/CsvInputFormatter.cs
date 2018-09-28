using System;
using BusinessLogic.Core.Entities;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Formatters
{
    /// <summary>
    /// Based on https://www.c-sharpcorner.com/article/custom-formatters-in-asp-net-core-mvc-web-api/
    /// </summary>
    public class CsvInputFormatter : TextInputFormatter
    {
        public CsvInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanReadType(Type type)
        {
            if (type == typeof(Payment))
            {
                return base.CanReadType(type);
            }
            return false;
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            var request = context.HttpContext.Request;

            using (var reader = new StreamReader(request.Body, encoding))
            {
                try
                {
                    var line = await reader.ReadLineAsync();
                   
                    var split = line.Split(new char[] { ';' });
                    var payment = new Payment()
                    {
                        Id = (split[0]),
                        Name = split[1],
                        Description = split[2]
                    };

                    return await InputFormatterResult.SuccessAsync(payment);
                }
                catch
                {
                    return await InputFormatterResult.FailureAsync();
                }
            }
        }
    }
}
