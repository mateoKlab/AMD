using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot ("KeyValuePair")]
public struct SerializableKVP<K, V>
{
	public K Key 
	{ get; set; }
	
	public V Value 
	{ get; set; }
}

[XmlRoot ("FighterSkin")]
public class FighterSkinData : SerializableDictionary <FighterSpriteAttachment.AttachmentType, string> 
{
//	List<SerializableKVP> attachmentPairs;
}
