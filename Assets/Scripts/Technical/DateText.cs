using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DateText : MonoBehaviour {
	private Text text;
	private float time;


	// Use this for initialization
	void Awake () {
		text = GetComponent<Text>();
		time = SaveSerial.totalTime;
		if (time < 7200)
		{
			time = time / 60;
            text.text = "Вы играли " + time.ToString("0.0") + " минут";
        }
		else
		{
            time = time / 3600;
            text.text = "Вы играли " + time.ToString("0.0") + " часов";
        }
	}
}
