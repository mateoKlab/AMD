using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FighterSpriteController : MonoBehaviour {

	public List<FighterSpriteAttachment> spriteAttachments;
	
	public FighterClass fighterClass;

	public void SetFighterSkin (FighterSkinData skinData)
	{
		Sprite newSprite;

		foreach (FighterSpriteAttachment attachment in spriteAttachments) {

			if (skinData.ContainsKey (attachment.type)) {
				newSprite = Resources.Load ("Sprites/UnitSprites/" + attachment.type.ToString() + "/" + skinData[attachment.type], typeof(Sprite)) as Sprite;
				attachment.GetComponent <SpriteRenderer> ().sprite = newSprite;
			}
		}
	}
}
