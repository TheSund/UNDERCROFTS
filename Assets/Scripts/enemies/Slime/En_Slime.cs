using UnityEngine;
using System.Collections;

public class En_Slime : MonoBehaviour 
{
	public float startDashTimeMin;
	public float startDashTimeMax;
	private float dashTime;
	private Animator anim;
	private float direction;
	private float force;
	private bool playerInRange;

	// Use this for initialization
	void Start () 
	{
		dashTime = Random.Range(startDashTimeMin, startDashTimeMax);
		anim = GetComponent<Animator> ();
		direction = Mathf.Round (Random.Range (-10, 10));
		force = Mathf.Round (Random.Range (-10, 10));

	}
	
	// Update is called once per frame
	void Update () 
	{	
		playerInRange = GetComponentInChildren<En_Slime_Radius>().playerInRange; 
		anim.SetFloat ("dashTime", dashTime);
		if (dashTime <= 0) 
		{	
			direction = Mathf.Round (Random.Range (-10, 10));
			force = Mathf.Round (Random.Range (-10, 10));
			if (playerInRange == false)
			{
				GetComponent<Rigidbody2D> ().AddForce (new Vector2(direction,force), ForceMode2D.Impulse);
			}
			else 
			{
				GetComponent<Rigidbody2D> ().AddForce ((GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized*10, ForceMode2D.Impulse);
			}
			direction = Mathf.Round (Random.Range (-10, 10));
			force = Mathf.Round (Random.Range (-10, 10));
			dashTime = Random.Range(startDashTimeMin, startDashTimeMax);
		} else 
		{
			dashTime -= Time.deltaTime;
		}
	}
	
}