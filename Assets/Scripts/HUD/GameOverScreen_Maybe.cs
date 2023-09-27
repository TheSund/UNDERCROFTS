using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScreen_Maybe : MonoBehaviour 
{
    private Text mytext;
    public string[] quotes;
    private int rng;

	// Use this for initialization
	void Start () 
    {
        mytext = GetComponent<Text>();
        rng = Random.Range(0, quotes.Length);
        mytext.text = quotes[rng];
    }
	
}
