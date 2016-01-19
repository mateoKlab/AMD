// ----- AUTO GENERATED CODE ----- //
public enum EventTags
{
	FIGHTER_KILLED, 			// Called when a fighter reaches 0 hp.
								// args[0]: typeof:GameObject	desc: The fighter killed.
								// args[1]: typeof:GameObject	desc: The attacker.

	FIGHTER_DEATH,				// Called when a fighter's death animation/timer is finished.
								// args[0]: typeof:GameObject   desc: The fighter that died.

	FIGHTER_RECEIVED_DAMAGE,	// Called when a fighter recieves damage.
								// args[0]: typeof:int			desc: The damage amount.
								// args[1]: typeof:GameObject	desc: The fighter that received damage.

	FIGHTER_IN_RANGE,			// Called when an enemy enters the range of a fighter.
								// args[0]: typeof:GameObject	desc: The fighter in range.

	STAGE_WIN, 					// args[0]: typeof:StageData    desc: The stage beaten.
    END_SCREEN_EXP,
    RETURN_TO_MAIN_MENU,	
	DEFAULT
}
