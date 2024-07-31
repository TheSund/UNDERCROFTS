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

	public void FadeToLevel(int l)
	{
		Cursor.visible = false;
		anim.SetTrigger("fade");
		levelToLoad = l;
	}

	public void OnFadeComplete ()
	{
		StartCoroutine(LoadingScreenOnFade());
	}

	private IEnumerator LoadingScreenOnFade() 
	{
		yield return null;
		AsyncOperation operation = Application.LoadLevelAsync(levelToLoad);
		operation.allowSceneActivation = false;
		loadingScreen.SetActive (true);
		while (!operation.isDone)
		{
			if (operation.progress >= 0.9f)
			{
				operation.allowSceneActivation = true;
				Cursor.visible = true;
			}

				
			yield return null;
		}
	}

}
