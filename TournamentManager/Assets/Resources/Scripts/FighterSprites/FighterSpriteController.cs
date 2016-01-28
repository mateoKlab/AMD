using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


[SerializeField]
public class FighterSpriteController : MonoBehaviour {
	
	public List<FighterSpriteAttachment> spriteAttachments;
	
	public Class fighterClass;

	private Dictionary<string, GameObject> attachmentDictionary;

	void Awake ()
	{
		Initialize ();
	}

	// TEMPORARY.. move to own script.
	void AttackEvent ()
	{
		// Called during attack animation at the point where the fighter strikes the enemy.
	}

	public void SetFighterSkin (FighterSkinData skinData)
	{
		if (attachmentDictionary == null) {
			Initialize ();
		}

		Sprite newSprite;
		foreach (KeyValuePair<string, string> spritePair in skinData) {

			newSprite = Resources.Load ("Sprites/UnitSprites/" + fighterClass.ToString () + "/" + spritePair.Key + "/" + spritePair.Value, typeof(Sprite)) as Sprite;

			if (attachmentDictionary.ContainsKey (spritePair.Key)) {
				if (attachmentDictionary [spritePair.Key].GetComponent <SpriteRenderer> () != null) 
				{
					attachmentDictionary [spritePair.Key].GetComponent <SpriteRenderer> ().sprite = newSprite;
				}

				// Temporary. For UI sprites.
				else if (attachmentDictionary [spritePair.Key].GetComponent <Image> () != null) 
				{
					attachmentDictionary [spritePair.Key].GetComponent <Image> ().sprite = newSprite;
				}
			}
		}
	}


	private void Initialize ()
	{
		// Build attachmentDictionary for faster access to sprite objects.
		attachmentDictionary = new Dictionary<string, GameObject> ();
		
		foreach (FighterSpriteAttachment spriteAttachment in spriteAttachments) {
			attachmentDictionary.Add (spriteAttachment.type.ToString (), spriteAttachment.gameObject);
		}
	}
	
}
