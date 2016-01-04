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
		
		#if UNITY_EDITOR || UNITY_IOS
		using(StringWriter sww = new StringWriter())
			using(XmlWriter writer = XmlWriter.Create(sww))
		{
			xmls.Serialize(writer, obj);
			
			// Using XmlDocument guarantees a properly formatted xml file.
			XmlDocument xdoc = new XmlDocument();
			xdoc.LoadXml(sww.ToString());

			xdoc.Save(Application.dataPath + "/Resources/Data/" + filename + ".xml");
		}
		
		#elif UNITY_ANDROID
		string filePath = GetPath("typeof (T).ToString ()" + Database.xml");
		using (var stream = System.IO.File.CreateText(filePath))
		{
			xmls.Serialize(stream, this);
			stream.Close();
		}
		#endif
		
		#if UNITY_EDITOR
		AssetDatabase.Refresh();
		#endif
	}


}
