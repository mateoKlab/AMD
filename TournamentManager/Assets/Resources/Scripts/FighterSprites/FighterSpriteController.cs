using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FighterSpriteController : MonoBehaviour {

	public List<FighterSpriteAttachment> spriteAttachments;
	
	public BaseType baseType;
	public enum BaseType {
		Warrior,
		Mage,
		Archer
	}
}
