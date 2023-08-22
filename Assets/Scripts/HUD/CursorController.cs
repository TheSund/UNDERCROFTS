using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour {

    public Texture2D cursorBattle1;
    public Texture2D cursorMenu1;
    public static Texture2D cursorBattle;
    public static Texture2D cursorMenu;

    // Use this for initialization
    void Start () 
	{
        cursorBattle = cursorBattle1;
		cursorMenu = cursorMenu1;
    Cursor.SetCursor(cursorBattle, new Vector2(32f, 32f), CursorMode.Auto);
	}

	// Update is called once per frame
	public static void ChangeCursor(Texture2D newCursor, float x, float y)
	{
		Cursor.SetCursor(newCursor, new Vector2(x, y), CursorMode.Auto);
	}
}
