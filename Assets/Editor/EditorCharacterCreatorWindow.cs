using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Stat
{
    public string Name;
    public int Value;
    public string Description;

    public Stat(string name, int value, string desc)
    {
        Name = name;
        Value = value;
        Description = desc;
    }
}

public class StatButton
{
    public Stat stat;
    public Rect buttonRect = new UnityEngine.Rect(75, 75, 150, 150);
    public bool isSelected = false;

    public void Draw()
    {
        Stat stat = new Stat("Str", 0, "Strength");
        var contentRect =
            new Rect(buttonRect) {position = new Vector2(buttonRect.position.x, buttonRect.position.y + 15)};
        GUI.Box(buttonRect, "stat");
        GUILayout.BeginArea(contentRect);
        stat.Name = GUILayout.TextField(stat.Name);
        stat.Value = EditorGUILayout.IntSlider(stat.Value, 0, 10);
        //GUILayout.TextArea(stat.Description, GUILayout.ExpandHeight(true));
        GUILayout.Toggle(isSelected, "isSelected");
        buttonRect.width = GUILayout.HorizontalSlider(buttonRect.width, 150, 300, GUILayout.MaxWidth(300));
        buttonRect.height = EditorGUILayout.FloatField(buttonRect.height);
        GUILayout.EndArea();
        var current = Event.current;
    }

    public void PollEvents()
    {
        switch (Event.current.type)
        {
            case EventType.MouseDown:
                if (Event.current.button == 0)
                {
                    if (buttonRect.Contains(Event.current.mousePosition))
                    {
                        isSelected = true;
                    }
                    else
                    {
                        isSelected = false;
                    }
                }

                break;
            case EventType.MouseDrag:
                if (Event.current.button == 0)
                {
                    if (isSelected)
                    {
                        buttonRect.position += Event.current.delta;
                        Event.current.Use();
                    }
                }

                break;
        }
    }
}

public class EditorCharacterCreatorWindow : UnityEditor.EditorWindow
{
    public System.Collections.Generic.List<StatButton> StatButtons = new System.Collections.Generic.List<StatButton>();
    [UnityEditor.MenuItem("Tools/Button")]
    public static void Init()
    {
        var window = ScriptableObject.CreateInstance<EditorCharacterCreatorWindow>();
        window.Show(); 
    }

    private void CreateButton()
    {
        StatButtons.Add(new StatButton());
    }
    private void OnGUI() 
    {
        foreach (var sb in StatButtons)
        {
            sb.Draw();
            sb.PollEvents();
        }
        Repaint();
        switch (Event.current.type)
        {
        case EventType.MouseDown:
            if (Event.current.button == 1)
            {
                var gm = new UnityEditor.GenericMenu();
                gm.AddItem(new GUIContent("Create Stat"), false, CreateButton);
                gm.AddItem(new GUIContent("Print Info"), false, () => { Debug.Log("Info Printed"); });
                gm.ShowAsContext();
            }
            break;
        }
    }
}
