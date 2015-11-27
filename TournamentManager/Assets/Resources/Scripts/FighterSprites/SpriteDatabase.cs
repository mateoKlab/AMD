using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot]
// List of available sprite attachments/body parts grouped by Fighter BaseType (Warrior, Archer, Mage)
public class SpriteDatabase : SerializableDictionary <FighterClass, SerializableDictionary <FighterSpriteAttachment.AttachmentType, List <string>>>
{
	public static SpriteDatabase LoadDatabase ()
	{
		XmlSerializer ser = new XmlSerializer(typeof(SpriteDatabase));
		
		TextAsset textAsset = Resources.Load("Data/SpriteData") as TextAsset;
		System.IO.StringReader stringReader;
		
		if (textAsset == null)
		{
			Debug.LogError ("Sprite Database not found!");
			return null;
		}
		else
		{
			stringReader = new System.IO.StringReader(textAsset.text);
			using (XmlReader reader = XmlReader.Create(stringReader))
			{
				return (SpriteDatabase)ser.Deserialize(reader);
			}
		}
	} 
}

//public class SpriteDatabaseController : MonoBehaviour {
//	
//	public SpriteDatabase spriteDatabase;
//	
//	// Use this for initialization
//	void Start () {
//		//		TestDatabase ();
//		LoadDatabase ();
//	}
//	
//	void TestDatabase ()
//	{
//		SpriteDatabase testData = new SpriteDatabase ();
//		SerializableDictionary <FighterSpriteAttachment.AttachmentType, List <string>> attachmentDic = new SerializableDictionary<FighterSpriteAttachment.AttachmentType, List <string>> ();
//		List <string> headList = new List<string> ();
//		headList.Add ("head");
//		headList.Add ("head_blue");
//		headList.Add ("head_green");
//		headList.Add ("head_red");
//		
//		attachmentDic.Add (FighterSpriteAttachment.AttachmentType.Head, headList);
//		
//		testData.Add (FighterSpriteController.BaseType.Warrior, attachmentDic);
//		
//		
//		XmlSerializer xmls = new XmlSerializer(typeof(SpriteDatabase));
//		
//		using(StringWriter sww = new StringWriter())
//			using(XmlWriter writer = XmlWriter.Create(sww))
//		{
//			xmls.Serialize(writer, testData);
//			
//			// Using XmlDocument guarantees a properly formatted xml file.
//			XmlDocument xdoc = new XmlDocument();
//			xdoc.LoadXml(sww.ToString());
//			xdoc.Save(Application.dataPath + "/Resources/Data/SpriteData.xml");
//		}
//	}
//	
//}