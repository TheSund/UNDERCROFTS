using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour 
{
	private Animator anim;
	private int levelToLoad;

	public GameObject loadingScreen;

	// Use this for initialization
	private void Start () 
	{
		anim = GetComponentInChildren<Animator> ();
		levelToLoad = 0;
	}
	
	// Update is called once per frame
	public void FadeToLevel(int l)
	{
		anim.SetTrigger("fade");
		levelToLoad = l;
	}

	public void OnFadeComplete ()
	{
		Application.LoadLevel(levelToLoad);
		StartCoroutine(LoadingScreeenOnFade ());
	}

	private IEnumerator LoadingScreeenOnFade() 
	{
		AsyncOperation operation = Application.LoadLevelAsync(levelToLoad);
		loadingScreen.SetActive (true);
		while (!operation.isDone)
			yield return null;
	}

}
