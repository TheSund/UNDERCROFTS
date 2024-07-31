using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSerial : MonoBehaviour {
	public static float totalTime;

	// Use this for initialization
	public static void SaveGameData()
	{
		totalTime += Time.time;
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/SaveSlot0.dat");
		SaveData data = new SaveData();
		data.savedTotalTime = totalTime;
		bf.Serialize(file, data);
		file.Close();
	}
	public static void LoadGameData()
	{
		if (File.Exists (Application.persistentDataPath + "/SaveSlot0.dat")) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + "/SaveSlot0.dat", FileMode.Open);
			SaveData data = (SaveData)bf.Deserialize(file);
			file.Close();
            totalTime = data.savedTotalTime;
		}
	}
	public static void ResetData()
	{
		if (File.Exists (Application.persistentDataPath + "/SaveSlot0.dat")) 
		{
			File.Delete(Application.persistentDataPath + "/SaveSlot0.dat");
            totalTime = 0f;
		}
	}
}
[Serializable]
class SaveData
{
	public float savedTotalTime;
}