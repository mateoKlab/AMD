using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot ("KeyValuePair")]
[System.Serializable]
public struct SerializableKVP<K, V>
{
	public K Key 
	{ get; set; }
	
	public V Value 
	{ get; set; }

	public SerializableKVP (K key, V value)
	{
		Key = key;
		Value = value;
	}
}

[XmlRoot ("FighterSkin")]
public class FighterSkinData : SerializableDictionary<string, string>
{
	public void AddSkin (string attachmentType, string spriteName)
	{
		if (!this.ContainsKey (attachmentType)) {
			this.Add (attachmentType, spriteName);
		}
	}

	public void ReplaceSkin (string attachmentType, string spriteName)
	{
		if (this.ContainsKey (attachmentType)) {
			this.Remove (attachmentType);
			this.Add (attachmentType, spriteName);
		}
	}
}