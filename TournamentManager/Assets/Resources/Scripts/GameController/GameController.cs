using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	// DO game related stuff here. Gacha. Equip unlock. etc.
	// Access/Set data from here.. TODO: GameDatabase should be readonly.

	private static LevelUpController levelUpController;
	private static EquipmentController equipmentController;


	void Start () {
	
		// Do dependency injection here.
		levelUpController = new LevelUpController (GameDatabase.xpDatabase, GameDatabase.classDatabase);
		equipmentController = new EquipmentController (GameDatabase.equipmentDatabase, GameData.instance.playerData);

	}
	

}
