using UnityEngine;
using System.Collections;

public class Trapdoor : MonoBehaviour {

	private LevelChanger levelChange;
	private bool activated = false;

	private PlayerScript playerScript;
	private Rigidbody2D rb;
	private Animator anim;
	private int nextChapter;

	// Use this for initialization
	void Start () {
		levelChange = GameObject.Find ("LevelChanger").GetComponent<LevelChanger>();
		nextChapter = GameObject.FindGameObjectWithTag ("Layouts").GetComponent<LayoutList> ().chapterNumber;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (activated) 
		{
			rb.transform.position = Vector2.MoveTowards(rb.transform.position, transform.position, 0.5f);
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{
			playerScript = col.gameObject.GetComponent<PlayerScript>();
			rb = col.gameObject.GetComponent<Rigidbody2D>();
			anim = col.gameObject.GetComponent<Animator>();

			playerScript.lockMovement = true;
			anim.Play("jump");
			GameObject.Find ("Canvas/HUD_MAIN").SetActive(false);
			foreach (Transform child in col.transform)
				if (!child.CompareTag("PlayerSprite"))
					Destroy(child.gameObject);

			activated = true;
			StartCoroutine(OnJumpComplete());
		}
	}
	private IEnumerator OnJumpComplete()
	{
		yield return new WaitForSeconds(0.8f);
		levelChange.FadeToLevel(nextChapter);
	}
}
