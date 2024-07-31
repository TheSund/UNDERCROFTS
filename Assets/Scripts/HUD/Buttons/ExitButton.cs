using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {

	// Use this for initialization
	public void QuitGame() 
	{
		SaveSerial.SaveGameData();
		Application.Quit();
	}
}
