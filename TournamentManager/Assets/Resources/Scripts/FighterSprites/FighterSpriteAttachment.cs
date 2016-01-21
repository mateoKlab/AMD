using UnityEngine;
using System.Collections;

[System.Serializable]
public class FighterSpriteAttachment {

	// FighterSpriteAttachment.Type
	public enum AttachmentType {
		Front_Upper_Leg,
		Front_Lower_Leg,
		Front_Ankle,
		Hind_Upper_Leg,
		Hind_Lower_Leg,
		Hind_Ankle,
		Body,
		Front_Upper_Arm,
		Front_Lower_Arm,
		Front_Fist,
		Sword,
		Head,
		Helm,
		Hind_Upper_Arm,
		Hind_Lower_Arm,
		Hind_Fist,
		Shield,
		Weapon

	}

	public AttachmentType type;
	public GameObject gameObject;
}
