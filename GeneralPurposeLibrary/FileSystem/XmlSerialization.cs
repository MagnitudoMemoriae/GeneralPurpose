using System.IO;
using System.Xml.Serialization;

namespace GeneralPurposeLibrary.FileSystem
{
    public static class XmlSerialization
    {
        /// <summary>
        /// Serialize a object of class T in a xml file fileInfo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toSerialize"></param>
        /// <param name="fileInfo"></param>
        public static void Serialize<T>(T toSerialize, FileInfo fileInfo)
        {
            // Insert code to set properties and fields of the object.
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            // To write to a file, create a StreamWriter object.
            using (StreamWriter myWriter = new StreamWriter(fileInfo.FullName))
            {
                mySerializer.Serialize(myWriter, toSerialize);
                myWriter.Close();
            }
        }

        /// <summary>
        /// Deserialize a xml file fileInfo in object of class T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public static T Deserialize<T>(FileInfo fileInfo)
        {
            T ReturnValue;
            // Construct an instance of the XmlSerializer with the type
            // of object that is being deserialized.
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));

            // To read the file, create a FileStream.
            using (FileStream myFileStream = new FileStream(fileInfo.FullName, FileMode.Open))
            {
                // Call the Deserialize method and cast to the object type.
                ReturnValue = (T)mySerializer.Deserialize(myFileStream);
            }

            return ReturnValue;
        }
    }
}