using UnityEngine;
using System.Collections;

public class RoomSpawner : MonoBehaviour 
{
	public Direction direction;
	public enum Direction
	{
		Left,
		Top,
		Right,
		Bottom,
		None
	}
	private RoomDirections directions;
	private int rng;
	public bool isSpawned;
	private float waitTime;

	private void Start()
	{
        directions = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomDirections>();
        isSpawned = false;
        waitTime = 1f;

        Invoke("Spawn", 0.2f);
        if (direction != Direction.None)
			Destroy(gameObject, waitTime);
		else
		{
            Invoke("Spawn", 0.2f);
            Destroy(gameObject, waitTime * 5f);
		}
	}

	public void Spawn()
	{
		if (!isSpawned && direction != Direction.None && directions.roomCounter > 0)
		{
			if (direction == Direction.Left)
			{
				rng = Random.Range(0, directions.leftRooms.Length);
				Instantiate(directions.leftRooms[rng], transform.position, directions.leftRooms[rng].transform.rotation);
			}
			else if (direction == Direction.Top)
			{
				rng = Random.Range(0, directions.topRooms.Length);
				Instantiate(directions.topRooms[rng], transform.position, directions.topRooms[rng].transform.rotation);
			}
			else if (direction == Direction.Right)
			{
				rng = Random.Range(0, directions.rightRooms.Length);
				Instantiate(directions.rightRooms[rng], transform.position, directions.rightRooms[rng].transform.rotation);
			}
			else if (direction == Direction.Bottom)
			{
				rng = Random.Range(0, directions.bottomRooms.Length);
				Instantiate(directions.bottomRooms[rng], transform.position, directions.bottomRooms[rng].transform.rotation);
			}
			isSpawned = true;
			directions.roomCounter--;
		}
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag("RoomSpawnPoint") && direction != Direction.None) 
		{
			Destroy (gameObject);
		}
	}
}
