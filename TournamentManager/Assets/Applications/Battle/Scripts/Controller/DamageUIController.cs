using UnityEngine;
using System.Collections;
using Bingo;

public class DamageUIController : Controller <DamageManager, DamageUIModel, DamageUIView>
{
	public void ShowDamage(int damageReceived, bool isAlly) {
		if(isAlly) 
		{
			view.damageLabel.color = Color.red;
		} 
		else 
		{
			view.damageLabel.color = Color.white;
		}
		view.damageLabel.text = damageReceived.ToString();
		view.damageAnim.SetTrigger("PopUp");
	}

	public void AddToDamagePool(){
		DamageManager.instance.AddToPool(gameObject);
		gameObject.SetActive(false);
	}
}
