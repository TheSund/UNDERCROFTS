using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CustomHUDMainCameraFinder : MonoBehaviour {
	private Canvas canvas;
	

	// Use this for initialization
	void Start () {
		canvas = gameObject.GetComponent<Canvas>();
		canvas.worldCamera = Camera.main;
	}
}
