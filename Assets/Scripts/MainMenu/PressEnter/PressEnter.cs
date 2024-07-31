using UnityEngine;
using System.Collections;
using System.IO.IsolatedStorage;
using UnityEngine.UI;

public class PressEnter : MonoBehaviour {

	public bool ready;
	public Canvas maincanvas;
	public Canvas levelChanger;

	// Use this for initialization
	void Start()
	{
		SaveSerial.LoadGameData();
		Cursor.visible = false;
		ready = false;
		StartCoroutine(PressEnterBecomeReady());
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.Return) && ready)
		{
			GetComponent<Animator>().Play("closing");
			StartCoroutine(DisablePressEnterScreen());
        }
	}

    private IEnumerator PressEnterBecomeReady()
	{
		yield return new WaitForSeconds(4.1f);
		ready = true;
	}

	private IEnumerator DisablePressEnterScreen()
	{
		yield return new WaitForSeconds(1.3f);
        maincanvas.gameObject.SetActive(true);
        levelChanger.gameObject.SetActive(true);
        Cursor.visible = true;
        gameObject.SetActive(false);
    }


}
