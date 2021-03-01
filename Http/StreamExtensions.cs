using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;

namespace MailSender.Http
{
    public static class StreamExtensions
    {
        public static T ReadAndDeserializeFromJson<T>(this Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));

            }
            if (!stream.CanRead)
            {
                throw new NotSupportedException("Can not read from this stream.");
            }

            using (var streamReader = new StreamReader(stream))
            {
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    var jsonSerializer = new JsonSerializer()
                    {
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        DateParseHandling = DateParseHandling.None,
                        // DateTimeZoneHandling = DateTimeZoneHandling.Utc                        
                        Converters = { new FixedIsoDateTimeOffsetConverter() }
                    };

                    return jsonSerializer.Deserialize<T>(jsonTextReader);
                }
            }

        }
        public class FixedIsoDateTimeOffsetConverter : IsoDateTimeConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?);
            }

            public FixedIsoDateTimeOffsetConverter() : base()
            {
                this.DateTimeStyles = System.Globalization.DateTimeStyles.AssumeUniversal;
            }
        }
    }
}
