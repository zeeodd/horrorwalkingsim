// by @torahhorse

using UnityEngine;
using System.Collections;

public class LockMouse : MonoBehaviour
{
    public Texture2D cursorTexture;

	void Start()
	{
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.SetCursor(cursorTexture,
                         new Vector2(cursorTexture.width / 2, cursorTexture.height / 2),
                         CursorMode.Auto);
	}

    void Update()
    {
        Cursor.visible = true;

        // Lock when mouse is clicked
        if ( Input.GetMouseButtonDown(0) && Time.timeScale > 0.0f )
    	{
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Unlock when escape is hit
        if  ( Input.GetKeyDown(KeyCode.Escape) )
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

}