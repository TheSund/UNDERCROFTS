using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {
	public GameObject[] characters = new GameObject[8];

	public Character characterType;
	public enum Character 
	{
		Mayne,
		Chunk,
		None
	}

	public float currentHealth;
	public float currentMaxHealth;
	public float currentDamage;
	private GameObject[] items;

	void Start () 
	{
		if (GameObject.Find (gameObject.name) != null && GameObject.Find (gameObject.name) != gameObject)
			Destroy(gameObject);
        characterType = Character.None;
        DontDestroyOnLoad(gameObject);
	}
	
	public void SetCharacter(int id) 
	{
		characterType = (Character)id;
	}

	public void OnLevelWasLoaded() 
	{
		if (Application.loadedLevel > 0) 
		{
			if (Application.loadedLevelName != "ThatsAllForNow")
			switch (characterType) {
				case Character.Mayne:
				{
					if (Application.loadedLevel == 1) {
						currentDamage = 3.5f;
						currentMaxHealth = 4;
						currentHealth = currentMaxHealth;
					}
					SpawnPlayer (characters [0]);
					break;
				}
				case Character.Chunk:
				{
					if (Application.loadedLevel == 1) {
						currentDamage = 4f;
						currentMaxHealth = 5;
						currentHealth = currentMaxHealth;
					}
					SpawnPlayer (characters [1]);
					break;
				}
				case Character.None:
				{
					break;
				}
			}
			else
				Destroy (gameObject);
		}
	}
	private void SpawnPlayer(GameObject characterToSpawn) 
	{
		Instantiate(characterToSpawn, Vector3.zero, Quaternion.Euler(0,0,0));
	}
}
