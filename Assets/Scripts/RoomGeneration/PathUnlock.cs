using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathUnlock : MonoBehaviour 
{
	public GameObject Corridor;
	public GameObject Wall;
	private int x = 0;
	public bool isSpawned = false;
	private float waitTime = 2;
	private Object created;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("CorridorWall"))
		    Destroy(gameObject);
		if (col.CompareTag("Wall"))
			x++;
		if (x == 2 && !isSpawned)
		{
			switch (gameObject.name)
			{
				case "pathUnlockerT":
					transform.parent.GetComponent<AddRoom>().topNeighbor = col.transform.parent.parent.gameObject;
					break;
				case "pathUnlockerR":
					transform.parent.GetComponent<AddRoom>().rightNeighbor = col.transform.parent.parent.gameObject;
					break;
				case "pathUnlockerB":
					transform.parent.GetComponent<AddRoom>().bottomNeighbor = col.transform.parent.parent.gameObject;
					break;
				case "pathUnlockerL":
					transform.parent.GetComponent<AddRoom>().leftNeighbor = col.transform.parent.parent.gameObject;
					break;
			}

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
