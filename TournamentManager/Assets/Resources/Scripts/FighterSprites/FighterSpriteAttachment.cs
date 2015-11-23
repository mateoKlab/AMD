using UnityEngine;
using System.Collections;

public class FighterSpriteAttachment : MonoBehaviour {

	public enum AttachmentType {
		Head,
		Weapon,
		Mantle,
		Body
	}

	public AttachmentType type;	
}
