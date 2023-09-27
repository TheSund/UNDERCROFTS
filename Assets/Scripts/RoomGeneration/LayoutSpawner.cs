using UnityEngine;
using System.Collections;

public class LayoutSpawner : MonoBehaviour 
{	
	[Header("Directions")]
	public GameObject topDirection;
	public GameObject rightDirection;
	public GameObject bottomDirection;
	public GameObject leftDirection;
	private PathUnlock top;
	private PathUnlock right;
	private PathUnlock bottom;
	private PathUnlock left;

	private int rng;
	private LayoutList layouts;

	private Object created;

	// Use this for initialization
	void Start () 
	{
		layouts = GameObject.FindGameObjectWithTag("Layouts").GetComponent<LayoutList>();
		top = topDirection.GetComponent<PathUnlock>();
		right = rightDirection.GetComponent<PathUnlock>();
		bottom = bottomDirection.GetComponent<PathUnlock>();
		left = leftDirection.GetComponent<PathUnlock>();
	}

	public void Spawn () 
	{	
		if (!GetComponentInParent<AddRoom> ().isLastRoom && !GetComponentInParent<AddRoom>().isStartRoom) {
			//Если 1 путь - T
			if (top.isSpawned && !right.isSpawned && !bottom.isSpawned && !left.isSpawned) {
				rng = Random.Range (0, layouts.T.Length);
				created = Instantiate (layouts.T [rng], transform.position, layouts.T [rng].transform.rotation);
			}
			//Если 1 путь - R
			else if (!top.isSpawned && right.isSpawned && !bottom.isSpawned && !left.isSpawned) {
				rng = Random.Range (0, layouts.R.Length);
				created = Instantiate (layouts.R [rng], transform.position, layouts.R [rng].transform.rotation);
			}
			//Если 1 путь - B
			else if (!top.isSpawned && !right.isSpawned && bottom.isSpawned && !left.isSpawned) {
				rng = Random.Range (0, layouts.B.Length);
				created = Instantiate (layouts.B [rng], transform.position, layouts.B [rng].transform.rotation);
			}
			//Если 1 путь - L
			else if (!top.isSpawned && !right.isSpawned && !bottom.isSpawned && left.isSpawned) {
				rng = Random.Range (0, layouts.A.Length);
				created = Instantiate (layouts.A [rng], transform.position, layouts.A [rng].transform.rotation);
			}


			//Если 2 пути - TR
			else if (top.isSpawned && right.isSpawned && !bottom.isSpawned && !left.isSpawned) {
				rng = Random.Range (0, layouts.TR.Length);
				created = Instantiate (layouts.TR [rng], transform.position, layouts.TR [rng].transform.rotation);
			}
			//Если 2 пути - TB
			else if (top.isSpawned && !right.isSpawned && bottom.isSpawned && !left.isSpawned) {
				rng = Random.Range (0, layouts.TB.Length);
				created = Instantiate (layouts.TB [rng], transform.position, layouts.TB [rng].transform.rotation);
			}
			//Если 2 пути - TL
			else if (top.isSpawned && !right.isSpawned && !bottom.isSpawned && left.isSpawned) {
				rng = Random.Range (0, layouts.TL.Length);
				created = Instantiate (layouts.TL [rng], transform.position, layouts.TL [rng].transform.rotation);
			}
			//Если 2 пути - RB
			else if (!top.isSpawned && right.isSpawned && bottom.isSpawned && !left.isSpawned) {
				rng = Random.Range (0, layouts.RB.Length);
				created = Instantiate (layouts.RB [rng], transform.position, layouts.RB [rng].transform.rotation);
			}
			//Если 2 пути - RL
			else if (!top.isSpawned && right.isSpawned && !bottom.isSpawned && left.isSpawned) {
				rng = Random.Range (0, layouts.RL.Length);
				created = Instantiate (layouts.RL [rng], transform.position, layouts.RL [rng].transform.rotation);
			}
			//Если 2 пути - BL
			else if (!top.isSpawned && !right.isSpawned && bottom.isSpawned && left.isSpawned) {
				rng = Random.Range (0, layouts.A.Length);
				created = Instantiate (layouts.A [rng], transform.position, layouts.A [rng].transform.rotation);
			}

	
			//Если 3 пути - nT
			else if (!top.isSpawned && right.isSpawned && bottom.isSpawned && left.isSpawned) {
				rng = Random.Range (0, layouts.nT.Length);
				created = Instantiate (layouts.nT [rng], transform.position, layouts.nT [rng].transform.rotation);
			}
			//Если 3 пути - nR
			else if (top.isSpawned && !right.isSpawned && bottom.isSpawned && left.isSpawned) {
				rng = Random.Range (0, layouts.nR.Length);
				created = Instantiate (layouts.nR [rng], transform.position, layouts.nR [rng].transform.rotation);
			}
			//Если 3 пути - nB
			else if (top.isSpawned && right.isSpawned && !bottom.isSpawned && left.isSpawned) {
				rng = Random.Range (0, layouts.nB.Length);
				created = Instantiate (layouts.nB [rng], transform.position, layouts.nB [rng].transform.rotation);
			}
			//Если 3 пути - nL
			else if (top.isSpawned && right.isSpawned && bottom.isSpawned && !left.isSpawned) {
				rng = Random.Range (0, layouts.nL.Length);
				created = Instantiate (layouts.nL [rng], transform.position, layouts.nL [rng].transform.rotation);
			}
	

			//Если 4 пути - A
			else if (top.isSpawned && right.isSpawned && bottom.isSpawned && left.isSpawned) {
				rng = Random.Range (0, layouts.A.Length);
				created = Instantiate (layouts.A [rng], transform.position, layouts.A [rng].transform.rotation);
			}
            (created as GameObject).transform.SetParent(gameObject.transform.parent);
        } 
		else if (!GetComponentInParent<AddRoom>().isStartRoom)
		{	
			if (GetComponentInParent<AddRoom> ().isBossRoom)
			{
				rng = Random.Range (0, layouts.BOSS.Length);
				created = Instantiate (layouts.BOSS[rng], transform.position, layouts.BOSS[rng].transform.rotation);
			}
			else 
			{
				rng = Random.Range (0, layouts.AMBUSH.Length);
				created = Instantiate (layouts.AMBUSH[rng], transform.position, layouts.AMBUSH[rng].transform.rotation);
			}
            (created as GameObject).transform.SetParent(gameObject.transform.parent);
        }
		Destroy(gameObject);
	}
}
