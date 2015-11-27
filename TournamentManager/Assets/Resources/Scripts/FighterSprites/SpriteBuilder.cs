using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteBuilder : MonoBehaviour {

	public GameObject WarriorPrefab;
	
	// List of sprites available for each sprite attachment/body part.
	public SpriteDatabase spriteDatabase;

	private bool firstInstance = false;
	
	// Singleton instance. _instance will be set on Awake().
	private static SpriteBuilder _instance;
	public static SpriteBuilder instance {
		get { 
			if (!_instance) {
				GameObject newObject = new GameObject ();
				newObject.name = "SpriteBuilder";
				newObject.AddComponent<SpriteBuilder> ();
				
				_instance = newObject.GetComponent<SpriteBuilder> ();
			}
			return _instance; 
		}
		private set { }
	}

	void Awake()
	{
		_instance = this;
		
		// Keep object alive between scenes.
		DontDestroyOnLoad(gameObject);
		
		// If there is more than 1 of this object, destroy the second instance.
		if (!firstInstance && FindObjectsOfType (typeof(SpriteBuilder)).Length > 1) {
			DestroyImmediate (gameObject);
		} else {
			firstInstance = true;
		}
	}

	public SpriteBuilder () 
	{
		spriteDatabase = SpriteDatabase.LoadDatabase ();
	}

	public void BuildSprite (FighterSpriteController spriteController)
	{
//		Sprite newSprite;
//
//		foreach (FighterSpriteAttachment attachment in spriteController.spriteAttachments) {
//			if (attachment.type == FighterSpriteAttachment.AttachmentType.Static) {
//				continue;
//			}
//
//			List<string> spritePool = spriteDatabase[spriteController.baseType][attachment.type];
//
//			int randomSprite = UnityEngine.Random.Range (0, spritePool.Count);
//
//			newSprite = Resources.Load ("Sprites/UnitSprites/" + attachment.type.ToString() + "/" + spritePool[randomSprite], typeof(Sprite)) as Sprite;
//			attachment.gameObject.GetComponent<SpriteRenderer> ().sprite = newSprite;
//		}
	}

	public void SetSprites (FighterSpriteController spriteController)
	{
		

	}

//	public FighterSkinData BuildSprite (FighterSpriteController spriteController)
//	{
//		FighterSkinData newSkin = new FighterSkinData ();
//
//		Sprite newSprite;
//		
//		foreach (FighterSpriteAttachment attachment in spriteController.spriteAttachments) {
//			if (attachment.type == FighterSpriteAttachment.AttachmentType.Static) {
//				continue;
//			}
//			
//			List<string> spritePool = spriteDatabase[spriteController.baseType][attachment.type];
//			
//			int randomSprite = UnityEngine.Random.Range (0, spritePool.Count);
//			
//			newSprite = Resources.Load ("Sprites/UnitSprites/" + attachment.type.ToString() + "/" + spritePool[randomSprite], typeof(Sprite)) as Sprite;
//			attachment.gameObject.GetComponent<SpriteRenderer> ().sprite = newSprite;
//		}
//	}
}
