// by @torahhorse

using UnityEngine;
using System.Collections;

public class LockMouse : MonoBehaviour
{
    public Texture2D cursorTexture;

	void Start()
	{
		LockCursor(true);

        Cursor.SetCursor(cursorTexture,
                         new Vector2(cursorTexture.width / 2, cursorTexture.height / 2),
                         CursorMode.Auto);
        Cursor.visible = true;
	}

    void Update()
    {
    	// lock when mouse is clicked
    	if( Input.GetMouseButtonDown(0) && Time.timeScale > 0.0f )
    	{
            LockCursor(true);
    	}

        //Cursor.visible = true;
    
    	// unlock when escape is hit
        if  ( Input.GetKeyDown(KeyCode.Escape) )
        {
        	LockCursor(!Screen.lockCursor);
        }
    }
    
    public void LockCursor(bool lockCursor)
    {
    	Screen.lockCursor = lockCursor;
    }
}