using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CodeDead.UpdateManager.Objects;
using Newtonsoft.Json;

namespace CodeDead.UpdateManager.Utils
{
    /// <summary>
    /// Class that contains logic to deserialize an XML or JSON file to a PlatformUpdates object
    /// </summary>
    internal static class PlatformUpdatesDeserializer
    {
        /// <summary>
        /// Deserialize XML data into a PlatformUpdates object
        /// </summary>
        /// <param name="data">The XML data that should be deserialized</param>
        /// <returns>The PlatformUpdates object that was deserialized</returns>
        internal static PlatformUpdates DeserializeXml(string data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length == 0) throw new ArgumentException(nameof(data));

            PlatformUpdates updates;
            XmlSerializer serializer = new XmlSerializer(typeof(PlatformUpdates));
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(data);
                    writer.Flush();

                    stream.Position = 0;
                    updates = (PlatformUpdates)serializer.Deserialize(stream);
                }
            }

            return updates;
        }

        /// <summary>
        /// Deserialize XML data into a PlatformUpdates object asynchronously
        /// </summary>
        /// <param name="data">The XML data that should be deserialized</param>
        /// <returns>The PlatformUpdates object that was deserialized or null if an error occurred</returns>
        internal static async Task<PlatformUpdates> DeserializeXmlAsync(string data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length == 0) throw new ArgumentException(nameof(data));

            PlatformUpdates updates = null;
            await Task.Run(async () =>
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PlatformUpdates));
                using (MemoryStream stream = new MemoryStream())
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        await writer.WriteAsync(data);
                        await writer.FlushAsync();

                        stream.Position = 0;
                        updates = (PlatformUpdates)serializer.Deserialize(stream);
                    }
                }
            });
            return updates;
        }

        /// <summary>
        /// Deserialize JSON data into a PlatformUpdates object
        /// </summary>
        /// <param name="data">The JSON data that should be deserialized</param>
        /// <returns>The PlatformUpdates object that was deserialized</returns>
        internal static PlatformUpdates DeserializeJson(string data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length == 0) throw new ArgumentException(nameof(data));

            PlatformUpdates updates = JsonConvert.DeserializeObject<PlatformUpdates>(data);
            return updates;
        }

        /// <summary>
        /// Deserialize JSON data into a PlatformUpdates object asynchronously
        /// </summary>
        /// <param name="data">The JSON data that should be deserialized</param>
        /// <returns>The PlatformUpdates object that was deserialized</returns>
        internal static async Task<PlatformUpdates> DeserializeJsonAsync(string data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length == 0) throw new ArgumentException(nameof(data));

            PlatformUpdates updates = null;
            await Task.Run(() =>
            {
                updates = JsonConvert.DeserializeObject<PlatformUpdates>(data);
            });
            return updates;
        }
    }
}
