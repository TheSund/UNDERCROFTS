using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trapdoor : MonoBehaviour {

	private LevelChanger levelChange;
	private bool activated = false;

	private PlayerScript playerScript;
	private Rigidbody2D rb;
	private Animator anim;
	private int nextChapter;
	private GameObject player;

	// Use this for initialization
	void Start () {
		levelChange = GameObject.Find ("LevelChanger").GetComponent<LevelChanger>();
		nextChapter = GameObject.FindGameObjectWithTag ("Layouts").GetComponent<LayoutList> ().chapterNumber+1;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (activated) 
		{
			rb.transform.position = Vector2.MoveTowards(rb.transform.position, transform.position, 0.5f);
		}
	}

	public void OnInteract()
	{	
		GameObject player = GameObject.FindGameObjectWithTag("Player");

			playerScript = player.gameObject.GetComponent<PlayerScript>();
			rb = player.gameObject.GetComponent<Rigidbody2D>();
			anim = player.gameObject.GetComponent<Animator>();

			playerScript.lockMovement = true;
			anim.Play("jump");
			GameObject.FindGameObjectWithTag("CharacterHUD").SetActive(false);
			foreach (Transform child in player.transform)
				if (!child.CompareTag("PlayerSprite"))
					Destroy(child.gameObject);

			activated = true;
			StartCoroutine(OnJumpComplete());
		//}
	}
	private IEnumerator OnJumpComplete()
	{
		yield return new WaitForSeconds(0.8f);
		levelChange.FadeToLevel(nextChapter);
	}

}
