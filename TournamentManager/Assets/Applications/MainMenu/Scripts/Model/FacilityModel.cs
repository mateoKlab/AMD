using UnityEngine;
using System.Collections;
using Bingo;
using System;

public class FacilityModel : Model
{
    [HideInInspector]
    public int level;
    [HideInInspector]
    public int cost;
    [HideInInspector]
	public float cooldown;
    [HideInInspector]
	public DateTime timeClicked;
    
}
