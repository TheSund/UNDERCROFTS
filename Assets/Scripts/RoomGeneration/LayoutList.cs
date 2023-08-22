using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LayoutList : MonoBehaviour 
{
	[Header("One-way layouts")]
	public GameObject[] T;
	public GameObject[] R;
	public GameObject[] B;
	public GameObject[] L;
	[Header("Two-way layouts")]
	public GameObject[] TR;
	public GameObject[] TB;
	public GameObject[] TL;
	public GameObject[] RB;
	public GameObject[] RL;
	public GameObject[] BL;
	[Header("Three-way layouts")]
	public GameObject[] nT;
	public GameObject[] nR;
	public GameObject[] nB;
	public GameObject[] nL;
	[Header("All-way layouts")]
	public GameObject[] A;
	[Header("Boss Layouts")]
	public int chapterNumber;
	public bool isBossChapter;
	public GameObject[] AMBUSH;
	public GameObject[] BOSS;

	[HideInInspector] public List<GameObject> rooms;
	
	private void Start()
	{
		StartCoroutine (LastRoomFinder());
	}
	
	IEnumerator LastRoomFinder()
	{
		yield return new WaitForSeconds (2f);
		AddRoom lastRoom = rooms [rooms.Count - 1].GetComponent<AddRoom>();
		lastRoom.isLastRoom = true;
		if (isBossChapter == true)
			lastRoom.isBossRoom = true;
		foreach (GameObject room in rooms) 
		{
			room.GetComponentInChildren<LayoutSpawner>().Spawn();
		}
	} 
}
