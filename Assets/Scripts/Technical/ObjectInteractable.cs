using UnityEngine;
using System.Collections;

public class ObjectInteractable : MonoBehaviour{
	

	public void Interact() 
	{
		SendMessage("OnInteract");
	}
}
