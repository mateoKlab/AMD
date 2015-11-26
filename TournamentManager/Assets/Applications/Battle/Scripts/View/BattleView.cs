using UnityEngine;
using System.Collections;
using Bingo;

public class BattleView : View<Battle>
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    [Inject]
    public BattleEndView battleEndView { get; private set; }
    
    [Inject]
    public BattleMenuView battleMenuView { get; private set; }
    
    //////// END MVCCodeEditor GENERATED CODE ////////
    
    

	public void OnClickBackButton ()
	{
		((BattleController)controller).OnBackButtonClicked ();
	}
}

