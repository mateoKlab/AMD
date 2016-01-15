using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SpriteTest {

	public FighterSpriteAttachment.AttachmentType attachmentType;
	public GameObject spriteRenderer;



}

[SerializeField]
public class FighterSpriteController : MonoBehaviour {
	
	public List<FighterSpriteAttachment> spriteAttachments;
	
	public Class fighterClass;

	private Dictionary<string, GameObject> attachmentDictionary;

	void Awake ()
	{
		// Build attachmentDictionary for faster access to sprite objects.
		attachmentDictionary = new Dictionary<string, GameObject> ();

		foreach (FighterSpriteAttachment spriteAttachment in spriteAttachments) {

			attachmentDictionary.Add (spriteAttachment.type.ToString (), spriteAttachment.gameObject);
		}
	}

	public void SetFighterSkin (FighterSkinData skinData)
	{
		Debug.Log ("SET");
		Sprite newSprite;

		foreach (SerializableKVP<string, string> spritePair in skinData) {

			Debug.Log ("KEY: " + spritePair.Key + " VALUE: " + spritePair.Value);
			newSprite = Resources.Load ("Sprites/UnitSpritesUpdated/" + spritePair.Key + "/" + spritePair.Value, typeof(Sprite)) as Sprite;

			attachmentDictionary [spritePair.Key].GetComponent <SpriteRenderer> ().sprite = newSprite;
//			attachment.GetComponent <SpriteRenderer> ().sprite = newSprite;

		}
	}
}
