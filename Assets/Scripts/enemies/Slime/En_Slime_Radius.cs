using UnityEngine;
using System.Collections;

public class En_Slime_Radius : MonoBehaviour 
{
	public bool playerInRange;

	void OnTriggerStay2D(Collider2D collider) 
	{	
		if (collider.gameObject.tag == "Player") {
			playerInRange = true;
		}
		
	}
	void OnTriggerExit2D(Collider2D collider) 
	{
		playerInRange = false;
		
	}
}
