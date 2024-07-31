using UnityEngine;
using System.Collections;

public class OpenBigMap : MonoBehaviour {

	public static bool mapIsOpened;

	public GameObject miniMap;
	public GameObject bigMap;

	// Use this for initialization
	void Start () {
		mapIsOpened = false;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab) && (bigMap.activeSelf ^ miniMap.activeSelf))
		{
			if (mapIsOpened)
				CloseMap();
			else
				OpenMap();
		}
	}
	void OpenMap()
	{
		bigMap.SetActive(true);
		CursorController.ChangeCursor(CursorController.cursorMenu, 2f, 2f);
		miniMap.GetComponent<Animator>().Play("close");
		StartCoroutine(OnAnimationComplete(miniMap));
	}
	void CloseMap()
	{
		miniMap.SetActive(true);
		CursorController.ChangeCursor(CursorController.cursorBattle, 32f, 32f);
		bigMap.GetComponent<Animator>().Play("close");
		StartCoroutine(OnAnimationComplete(bigMap));
	}
	private IEnumerator OnAnimationComplete(GameObject map)
	{
		yield return new WaitForSeconds(0.5f);
		if (mapIsOpened)
			mapIsOpened = false;
		else
			mapIsOpened = true;
		map.SetActive(false);
	}
}
