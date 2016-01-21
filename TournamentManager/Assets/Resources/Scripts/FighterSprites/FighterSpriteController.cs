using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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
		Sprite newSprite;
		foreach (KeyValuePair<string, string> spritePair in skinData) {

			newSprite = Resources.Load ("Sprites/UnitSpritesUpdated/" + spritePair.Key + "/" + spritePair.Value, typeof(Sprite)) as Sprite;

			attachmentDictionary [spritePair.Key].GetComponent <SpriteRenderer> ().sprite = newSprite;
		}
	}
}
