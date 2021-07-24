using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class GUIeditor : EditorWindow
{
    Vector2 offset;
    Vector2 drag;
    List<List<Node>> nodes;
    GUIStyle empty;
    [MenuItem("Window/Map Editor")]
    private static void OpenWindow()
    {
        GUIeditor window = GetWindow<GUIeditor>();
        window.titleContent = new GUIContent("Map Editor");

        //empty = new GUIStyle();

    }

    private void OnEnable()
    {
        //Texture2D = null;
        SetUpNodes();
    }

    private void SetUpNodes()
    {
        nodes = new List<List<Node>>();
        for(int i=0; i<20; i++)
        {
            nodes.Add(new List<Node>());
            for(int j=0; j<10; j++)
            {
                //nodes[i].Add(new Node(Vector2.zero, 30, 30,))
            }
        }
    }

    private void OnGUI()
    {
        DrawGrid();
        ProcessGrid(Event.current);
        if (GUI.changed)
        {
            Repaint();
        }

    }

    private void ProcessGrid(Event e)
    {
        drag = Vector2.zero;
        switch (e.type)
        {
            case EventType.MouseDrag:
                if(e.button == 0)
                {
                    OnMouseDrag(e.delta);
                }
                break;
        }
    }

    private void OnMouseDrag(Vector2 delta)
    {
        drag = delta;
        GUI.changed = true; 
    }
    
    private void DrawGrid()
    {
        int widthDivider = Mathf.CeilToInt(position.width/20);
        int heightDivider = Mathf.CeilToInt(position.height / 20);
        Handles.BeginGUI();
        Handles.color = new Color(0.5f, 0.5f, 0.5f, 0.2f);
        offset += drag;
        Vector3 newOffset = new Vector3(offset.x % 20, offset.y % 20, 0);
        for(int i=0; i<widthDivider; i++)
        {
            Handles.DrawLine(new Vector3(20 * i, -20, 0) + newOffset, new Vector3(20 * i, position.height, 0) + newOffset);
        }
        for (int i = 0; i < heightDivider; i++)
        {
            Handles.DrawLine(new Vector3(-20, 20*i, 0) + newOffset, new Vector3(position.width, 20*i, 0) + newOffset);
        }
        Handles.color = Color.white;
        Handles.EndGUI();
    }
}
