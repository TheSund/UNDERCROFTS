using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour 
{
	private Animator anim;
	private Collider2D col;
	private bool closed;
	public bool isADoor;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		col = GetComponent<BoxCollider2D>();
		if (isADoor) 
		{
			if (col.enabled)
				closed = true;
			else
				closed = false;
		} 
		else 
		{
			if (col.enabled)
				closed = false;
			else
				closed = true;
		}
	}

	public void Close() 
	{
		if (!closed) 
		{
			closed = true;
			anim.SetTrigger("close");
			if (isADoor)
				col.enabled = true;
			else 
				col.enabled = false;
		}
	}
	public void Open() 
	{
		if(closed)
		{
			closed = false;
			anim.SetTrigger("open");
			if (isADoor)
				col.enabled = false;
			else 
				col.enabled = true;
		}
	}
}
