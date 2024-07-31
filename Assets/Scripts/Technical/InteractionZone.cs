using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionZone : MonoBehaviour {
	
	private void OnTriggerStay2D(Collider2D col)
	{
		if (Input.GetKey(KeyCode.E) && col.GetComponent<ObjectInteractable>() != null)
		{
			col.GetComponent<ObjectInteractable>().Interact();
		}
	}
}
