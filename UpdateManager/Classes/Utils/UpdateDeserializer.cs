using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace CodeDead.UpdateManager.Classes.Utils
{
    /// <summary>
    /// Class that contains logic to deserialize an XML or JSON file to an Update object
    /// </summary>
    internal static class UpdateDeserializer
    {
        /// <summary>
        /// Deserialize XML data into an Update object
        /// </summary>
        /// <param name="data">The XML data that should be deserialized</param>
        /// <param name="applicationVersion">The current Version of the application</param>
        /// <returns>The Update object that was deserialized</returns>
        internal static Update DeserializeXml(string data, Version applicationVersion)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length == 0) throw new ArgumentException(nameof(data));

            Update update;
            XmlSerializer serializer = new XmlSerializer(typeof(Update));
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(data);
                    writer.Flush();
                }

                stream.Position = 0;
                update = (Update) serializer.Deserialize(stream);
                update.ApplicationVersion = applicationVersion;
            }

            return update;
        }

        /// <summary>
        /// Deserialize XML data into an Update object asynchronously
        /// </summary>
        /// <param name="data">The XML data that should be deserialized</param>
        /// <param name="applicationVersion">The current Version of the application</param>
        /// <returns>The Update object that was deserialized or null if an error occurred</returns>
        internal static async Task<Update> DeserializeXmlAsync(string data, Version applicationVersion)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length == 0) throw new ArgumentException(nameof(data));

            await Task.Run(async () =>
            {
                Update update;
                XmlSerializer serializer = new XmlSerializer(typeof(Update));
                using (MemoryStream stream = new MemoryStream())
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        await writer.WriteAsync(data);
                        await writer.FlushAsync();
                    }

                    stream.Position = 0;
                    update = (Update) serializer.Deserialize(stream);
                    update.ApplicationVersion = applicationVersion;
                }

                return update;
            });
            return null;
        }

        /// <summary>
        /// Deserialize JSON data into an Update object
        /// </summary>
        /// <param name="data">The JSON data that should be deserialized</param>
        /// <param name="applicationVersion">The current Version of the application</param>
        /// <returns>The Update object that was deserialized</returns>
        internal static Update DeserializeJson(string data, Version applicationVersion)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length == 0) throw new ArgumentException(nameof(data));

            Update update = new JavaScriptSerializer().Deserialize<Update>(data);
            update.ApplicationVersion = applicationVersion;
            return update;
        }

        /// <summary>
        /// Deserialize JSON data into an Update object asynchronously
        /// </summary>
        /// <param name="data">The JSON data that should be deserialized</param>
        /// <param name="applicationVersion">The current Version of the application</param>
        /// <returns>The Update object that was deserialized</returns>
        internal static async Task<Update> DeserializeJsonAsync(string data, Version applicationVersion)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length == 0) throw new ArgumentException(nameof(data));

            await Task.Run(() =>
            {
                Update update = new JavaScriptSerializer().Deserialize<Update>(data);
                update.ApplicationVersion = applicationVersion;
                return update;
            });
            return null;
        }
    }
}
