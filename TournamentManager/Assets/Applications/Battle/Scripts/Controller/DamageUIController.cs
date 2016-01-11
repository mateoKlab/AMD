using UnityEngine;
using System.Collections;
using Bingo;

public class DamageUIController : Controller <DamageManager, DamageUIModel, DamageUIView>
{
	public void ShowDamage(int damageReceived) {
		view.damageLabel.text = damageReceived.ToString();
		view.damageAnim.SetTrigger("PopUp");
	}

	public void AddToDamagePool(){
		DamageManager.instance.AddToPool(gameObject);
		gameObject.SetActive(false);
	}
}
