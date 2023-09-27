using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class AddRoom : MonoBehaviour 
{
	[Header("Doors")]
	public List<GameObject> doors;

	[Header("Enemies")]
	public GameObject[] enemyTypes;
	public List<Transform> enemySpawners;

	[HideInInspector] public List<GameObject> enemies;

	private LayoutList variants;

// Проверки
	private bool spawned;
	private bool cleared = false;
	private bool ready = false;
	private bool playerInRoom = false;
	public bool isLastRoom = false;
	public bool isBossRoom = false;
	public bool isStartRoom;
    // Для засады
    public int waves = 3;
	private GameObject wavecounter;

	public GameObject fogOfWar;
	private Animator fogOfWarAnim;
	private void Awake() 
	{
		variants = GameObject.FindGameObjectWithTag("Layouts").GetComponent<LayoutList>();
		wavecounter = GameObject.Find("Canvas/HUD_MAIN/HUD_AMBUSH_COUNTER");
		fogOfWarAnim = fogOfWar.GetComponent<Animator>();
		Invoke("BecomeReady", 2f);
	}
	private void Start() 
	{
		variants.rooms.Add (gameObject);
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
			{
            fogOfWarAnim.SetTrigger("fadeout");
            // Если обычная комната
            if (!isLastRoom && !isStartRoom) {
				if (ready && !spawned && enemySpawners.Count != 0)
				{
					playerInRoom = true;
					spawned = true;
					foreach (GameObject door in doors) {
						door.GetComponent<Door>().Close();
					}

					foreach (Transform spawner in enemySpawners) {
						int rand = Random.Range(0, 11);
						if (rand < 9) {
							GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
							GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject;
							enemy.transform.parent = transform;
							enemies.Add(enemy);
						}
					}
				}
				CheckEnemies();
				//StartCoroutine(CheckEnemies());
			}
			// Если последняя комната - засада
			else if (!isBossRoom && !isStartRoom)
			{
				if (ready && !spawned && enemySpawners.Count != 0)
				{
					playerInRoom = true;
					spawned = true;
					foreach (GameObject door in doors)
					{
						door.GetComponent<Door>().Close();
					}

					wavecounter.SetActive(true);
					AmbushSequence();
				}
			}
		}
	}
	/*	ПОКА UNITY НЕ ОБНОВЛЕН КОРОТКИЙ ПУТЬ НЕ СРАБОТАЕТ
	IEnumerator CheckEnemies()
	{
		yield return new WaitForSeconds (1f);
		while (enemies.Count > 0)
			yield return new WaitForSeconds (0.2f);
		foreach (GameObject door in doors) 
		{
			door.GetComponent<Door>().Open();
		}
	*/
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
            fogOfWarAnim.SetTrigger("fadein");
			if (!cleared)
			{
				foreach (GameObject enemy in enemies)
				{
					Destroy(enemy);
				}
				playerInRoom = false;
				spawned = false;
			}
		}
	}
	public void CheckEnemies()
	{	
		if (!isLastRoom) 
		{
			if (enemies.Count == 0 && playerInRoom) 
			{
				cleared = true;
				foreach (GameObject door in doors) 
				{
					door.GetComponent<Door> ().Open ();
				}
			}
		} 
		else if (!isBossRoom) 
		{
			if (enemies.Count == 0 && waves == 0 && playerInRoom) 
			{	
				wavecounter.SetActive(false);
				cleared = true;
				foreach (GameObject door in doors) 
				{
					door.GetComponent<Door> ().Open ();
				}
			}
			else if (enemies.Count == 0 && waves > 0 && playerInRoom)
			{
				waves--;
				AmbushSequence();
			}
		}
	}
// Подготовка комнаты
	private void BecomeReady()
	{
		foreach (Transform child in transform)
		{
			if (child.CompareTag("Corridor"))
				foreach (Transform door in child.transform) 
			{
					if (door.CompareTag("Door"))
						doors.Add(door.gameObject);
			}
			if (child.CompareTag("Layout"))
			foreach (Transform subchild in child.transform) 
			{
				if (subchild.CompareTag("Door"))
					doors.Add(subchild.gameObject);
				if (subchild.CompareTag("EnemySpawner"))
					enemySpawners.Add(subchild);
			}
			if (child.name == "WarFog") 
			{
				fogOfWarAnim = child.GetComponent<Animator>();
			}

        }
		ready = true;
	}

// Скрипт Засады
	private void AmbushSequence()
	{	
		if (waves > 0)
		{
			wavecounter.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Осталось волн: " + waves;
			foreach (Transform spawner in enemySpawners) 
			{
				int rand = Random.Range (0, 11);
				if (rand < 9) 
				{
					GameObject enemyType = enemyTypes [Random.Range (0, enemyTypes.Length)];
					GameObject enemy = Instantiate (enemyType, spawner.position, Quaternion.identity) as GameObject;
					enemy.transform.parent = transform;
					enemies.Add (enemy);
				}
			}
		}
	}

}
