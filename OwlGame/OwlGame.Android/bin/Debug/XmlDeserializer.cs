using System;
using System.IO;
using System.Xml.Serialization;


namespace OwlGame.ContentLoading
{

    //Student Num: c3260058
    //Student Name: Kira Khristosova
    //Date: 10/10/18
    //Description: This code is responsible for importing XML file formats. This is the reason this class uses Input/Output feature.
    //Tutorial Used: Coin Time Demo in Microsoft Website

    public class XmlDeserializer
    {
        static Lazy<XmlDeserializer> self = new Lazy<XmlDeserializer>(
            () => new XmlDeserializer());

    #if ANDROID
    public Android.App.Activity Activity { get; set;} //It used to be greyed out and it didn't recognize the commpilator. I fixed it by going to "Cofiguration Manager" -> "Build" and adding keyword "ANDROID" to "Conditional Compilation Symbols".
    #endif

        public static XmlDeserializer Self
        {
            get
            {
                return self.Value;
            }
        }


        public T XmlDeserialize<T>(string fileName)
        {
            using (var stream = GetStreamForFile(fileName))
            {
                return XmlDeserialize<T>(stream);
            }
        }

        public T XmlDeserialize<T>(Stream stream)
        {

            if (stream == null)
            {
                return default(T); // this happens if the file can't be found
            }
            else
            {
                XmlSerializer serializer = GetXmlSerializer<T>();
                T objectToReturn;
                objectToReturn = (T)serializer.Deserialize(stream);

                return objectToReturn;
            }
        }

        XmlSerializer GetXmlSerializer<T>()
        {
            XmlSerializer newSerializer = new XmlSerializer(typeof(T));

            return newSerializer;
        }


        public Stream GetStreamForFile(string fileName)
        {
        #if ANDROID

			if(Activity == null)
			{
				throw new InvalidOperationException("Activity must first be set before deserializing on Android");
			}

			return Activity.Assets.Open(fileName);

        #else
            return Microsoft.Xna.Framework.TitleContainer.OpenStream(fileName);
        #endif
        }




    }
}