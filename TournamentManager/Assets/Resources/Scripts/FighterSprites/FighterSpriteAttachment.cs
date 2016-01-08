using UnityEngine;
using System.Collections;

public class FighterSpriteAttachment : MonoBehaviour {

	// FighterSpriteAttachment.Type
	public enum AttachmentType {
		Front_Upper_Leg,
		Front_Lower_Leg,
		Front_Ankle,
		Hind_Upper_Leg,
		Hind_Lower_Leg,
		Hind_Ankle,
		Upper_Body,
		Front_Upper_Arm,
		Front_Lower_Arm,
		Front_Fist,
		Sword,
		Head,
		Helm,
		Hind_Upper_Arm,
		Hind_Lower_Arm,
		Hind_Fist,
		Shield

	}

	public AttachmentType type;	
}
