using UnityEngine;
using System.Collections;

public class FighterSpriteAttachment : MonoBehaviour {

	// FighterSpriteAttachment.Type
	public enum AttachmentType {
		Head,
		Weapon,
		Mantle,
		Body,
		Static
	}

	public AttachmentType type;	
}
