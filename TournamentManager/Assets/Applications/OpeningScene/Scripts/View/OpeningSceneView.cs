using UnityEngine;
using System.Collections;
using Bingo;

public class OpeningSceneView : View<OpeningScene>
{
	public void GoToMainMenu() {
		((OpeningSceneController)controller).GoToMainMenu();
	}
}

