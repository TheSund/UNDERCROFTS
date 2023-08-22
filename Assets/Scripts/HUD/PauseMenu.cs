using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{

	public static bool gameIsPaused;

	public GameObject pauseMenuUI;
	private LevelChanger levelChange;

	void Start()
	{
		levelChange = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (gameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	public void Resume()
	{
		pauseMenuUI.GetComponent<Animator>().Play("PauseMenuFadeOut");
        Time.timeScale = 1f;
        StartCoroutine(OnAnimationComplete());
	}

	void Pause()
	{
		pauseMenuUI.SetActive(true);
		CursorController.ChangeCursor(CursorController.cursorMenu, 2f, 2f);
		Time.timeScale = 0f;
		gameIsPaused = true;
	}

	public void LoadToMenu()
	{
		Time.timeScale = 1f;
		levelChange.FadeToLevel(0);
	}
	private IEnumerator OnAnimationComplete()
	{
		yield return new WaitForSeconds(0.3f);
        CursorController.ChangeCursor(CursorController.cursorBattle, 32f, 32f);
        pauseMenuUI.SetActive(false);
		gameIsPaused = false;
	}
}