using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour {

    public Texture2D cursorBattle1;
    public Texture2D cursorMenu1;
    public static Texture2D cursorBattle;
    public static Texture2D cursorMenu;
    public float startOffsetX;
    public float startOffsetY;

    // Use this for initialization
    void Start () 
	{
        cursorBattle = cursorBattle1;
		cursorMenu = cursorMenu1;
    Cursor.SetCursor(cursorBattle, new Vector2(startOffsetX, startOffsetY), CursorMode.Auto);
	}

	public static void ChangeCursor(Texture2D newCursor, float x, float y)
	{
		Cursor.SetCursor(newCursor, new Vector2(x, y), CursorMode.Auto);
	}
}
