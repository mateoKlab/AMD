using UnityEngine;
using System.Collections;
using Bingo;

public class OpeningSceneController : Controller<OpeningScene>
{
	public void GoToMainMenu(params object[] args) 
	{
		Application.LoadLevel("MainMenuScene");
	}
}

