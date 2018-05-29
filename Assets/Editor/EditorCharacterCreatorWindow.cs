using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class EditorCharacterCreatorWindow : UnityEditor.EditorWindow
{
    [UnityEditor.MenuItem("Tools/Button")]
    public static void Init()
    {
        var window = ScriptableObject.CreateInstance<EditorCharacterCreatorWindow>();
        window.Show();
    }
    private void OnGUI()
    {
        var rect = new Rect(100,100,300,300);
        if (GUI.Button(rect,"tutturu"))
        {
            UnityEngine.Debug.Log("Hello World");
        }
        var buttonRect = new UnityEngine.Rect();
        var contentRect = new Rect(buttonRect);
        contentRect.position = new Vector2(buttonRect.position.x,buttonRect.position.y + 15);
        GUI.Box(buttonRect, "stat");
        GUI.BeginGroup(contentRect);
    }

}
