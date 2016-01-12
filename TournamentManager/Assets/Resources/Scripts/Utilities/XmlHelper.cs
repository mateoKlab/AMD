using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public static class XmlHelper {

	public static void Save<T> (object obj, string filename)
	{
		XmlSerializer xmls = new XmlSerializer(typeof(T));
//		
//		#if UNITY_EDITOR || UNITY_IOS
//		using(StringWriter sww = new StringWriter())
//			using(XmlWriter writer = XmlWriter.Create(sww))
//		{
//			xmls.Serialize(writer, obj);
//			
//			// Using XmlDocument guarantees a properly formatted xml file.
//			XmlDocument xdoc = new XmlDocument();
//			xdoc.LoadXml(sww.ToString());
//
//			xdoc.Save(Application.dataPath + "/Resources/Data/" + filename + ".xml");
//		}
		
//		#elif UNITY_ANDROID
		string filePath = GetPath (filename + ".xml"); // typeof (T).ToString () + ".xml");

		using (var stream = System.IO.File.CreateText(filePath))
		{
			xmls.Serialize(stream, obj);
			stream.Close();
		}
//		#endif

		#if UNITY_EDITOR
		AssetDatabase.Refresh();
		#endif
	}
	
	public static T Load<T> (string filename) where T : new ()
	{

		XmlSerializer ser = new XmlSerializer(typeof(T));

//        #if UNITY_EDITOR || UNITY_IOS
//        TextAsset textAsset = Resources.Load("Data/" + filename) as TextAsset;
//        System.IO.StringReader stringReader;
//		
//        if (textAsset == null)
//        {
//            Debug.Log("File not found. Returning default values...");
//            return new T ();
//        }
//        else
//        {
//            stringReader = new System.IO.StringReader(textAsset.text);
//            using (XmlReader reader = XmlReader.Create(stringReader))
//            {
//                return (T) ser.Deserialize(reader);
//            }
//        }
//        #elif UNITY_ANDROID

		XmlReaderSettings settings = new XmlReaderSettings ();
		settings.CloseInput = true;

        string filePath = GetPath(filename + ".xml");

        if (System.IO.File.Exists(filePath))
        {
            using (XmlReader reader = XmlReader.Create(filePath, settings))
            {
				var t = (T) ser.Deserialize(reader);
				reader.Close ();

				return t;
            }
        }
        else
        {
            Debug.Log("No Player save file found. Returning default values...");
            return new T ();
        }
//        #endif

	}

 	// Retrieve relative path depending on platform
    private static string GetPath(string fileName)
    {
        #if UNITY_EDITOR
        return Application.dataPath + "/Resources/Data/" + fileName;
        #elif UNITY_ANDROID
        return Application.persistentDataPath+fileName;
        #elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+fileName;
        #else
        return Application.dataPath +"/"+ fileName;
        #endif
    }
}
