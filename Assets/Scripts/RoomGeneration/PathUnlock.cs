using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathUnlock : MonoBehaviour 
{
	public GameObject Corridor;
	public GameObject Wall;
	private int x = 0;
	public bool isSpawned = false;
	public float waitTime;
	private Object created;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("CorridorWall"))
		    Destroy(gameObject);
		if (col.CompareTag("Wall"))
			x++;
		if (x == 2 && !isSpawned)
		{
			isSpawned = true;
			created = Instantiate(Corridor, gameObject.transform.position, gameObject.transform.rotation);
			(created as GameObject).transform.SetParent(gameObject.transform.parent);
			Destroy(gameObject);
		}
	}
	void Start ()
	{
		StartCoroutine(NoPaths());
	}

	IEnumerator NoPaths()
	{
		yield return new WaitForSeconds(waitTime);
		if (!isSpawned) 
		{
			created = Instantiate (Wall, gameObject.transform.position, gameObject.transform.rotation);
			(created as GameObject).transform.SetParent (gameObject.transform.parent);
			Destroy(gameObject);
		}
	}
}
