using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScreen_Cause : MonoBehaviour {
	private Text mytext;
	private string causeOfDeath;

	// Use this for initialization
	void Start () {
		mytext = GetComponent<Text> ();
		causeOfDeath = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ().lastDamagedBy;

		if (causeOfDeath != null)
			mytext.text = "Причина смерти: " + causeOfDeath + ".";
		else
			mytext.text = "Причина смерти: ???";
	}
}
