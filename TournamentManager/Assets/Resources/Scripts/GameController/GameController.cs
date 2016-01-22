using UnityEngine;
using System.Collections;

public class GameController {


	// DO game related stuff here. Gacha. Equip unlock. etc.
	// Access/Set data from here.. TODO: GameDatabase should be readonly.

	public static LevelUpController levelUpController;
	private static EquipmentController equipmentController;


	// TEMP.
	public static void Initialize () {
	
		// Do dependency injection here.
		levelUpController = new LevelUpController (GameDatabase.xpDatabase, GameDatabase.classDatabase);
		equipmentController = new EquipmentController (GameDatabase.equipmentDatabase, GameData.instance.playerData);

	}
	

}
