using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace AttendeeList
{
    public class VCardFormatter : OutputFormatter
    {
        public VCardFormatter()
        {
            SupportedMediaTypes.Add("text/vcard");
        }

        protected override bool CanWriteType(Type type)
        {
            return type == typeof(Attendee);
        }

        public override void WriteResponseHeaders(OutputFormatterWriteContext context)
        {
            var attendee = context.Object as Attendee;
            var fileName = $"{attendee.FirstName}_{attendee.LastName}.vcf";
            fileName = new string(fileName.Select(c => Path.GetInvalidPathChars().Contains(c) ? '_' : c).ToArray(), 0, fileName.Length);
            context.HttpContext.Response.Headers.Add("content-disposition", $"attachment; filename=\"" + fileName + "\"");
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var attendee = context.Object as Attendee;

            FormattableString card = $@"BEGIN:VCARD
VERSION: 3.0
N:{attendee.LastName};{attendee.FirstName};;;
FN:{attendee.FirstName} {attendee.LastName}
EMAIL;type=INTERNET;type=pref:{attendee.Email}
ORG:{attendee.Company};
END:VCARD";

            using (var writer = context.WriterFactory(context.HttpContext.Response.Body, Encoding.UTF8))
            {
                return writer.WriteAsync(VCardEncoder.Encode(card));
            }
        }

        private class VCardEncoder : IFormatProvider, ICustomFormatter
        {
            public static VCardEncoder Instance = new VCardEncoder();

            public static string Encode(FormattableString card)
            {
                return card.ToString(Instance);
            }

            public string Format(string format, object arg, IFormatProvider formatProvider)
            {
                return Encode(arg);
            }

            public object GetFormat(Type formatType)
            {
                return (formatType == typeof(ICustomFormatter)) ? this : null;
            }

            private static string Encode(object arg)
            {
                return arg.ToString()
                    .Replace("\\", "\\\\")
                    .Replace(",", "\\,")
                    .Replace(";", "\\;");
            }
        }
    }
}
