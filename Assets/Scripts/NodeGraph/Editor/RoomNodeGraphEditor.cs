using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RoomNodeGraphEditor : EditorWindow
{
    private GUIStyle roomModeStyle;

    private const float nodeWidth = 160f;
    private const float nodeHeight = 75f;
    private const int nodePadding = 25;
    private const int nodeBorder = 12;

   [MenuItem("Room Node Graph Editor", menuItem = "Window/Dungeon Editor/Room Mode Graph Editor")]

   private static void OpenWindow()
   {
        GetWindow<RoomNodeGraphEditor>("Room Node Graph Editor");

   }

    private void OnEnable()
    {
        roomModeStyle = new GUIStyle();
        roomModeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
        roomModeStyle.normal.textColor = Color.white;
        roomModeStyle.padding = new RectOffset(nodePadding, nodePadding, nodePadding, nodePadding);
        roomModeStyle.border = new RectOffset(nodeBorder, nodeBorder, nodeBorder, nodeBorder);
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(new Vector2(100f, 100f), new Vector2(nodeWidth, nodeHeight)), roomModeStyle);
        EditorGUILayout.LabelField("Node 1");
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(new Vector2(300f, 300f), new Vector2(nodeWidth, nodeHeight)), roomModeStyle);
        EditorGUILayout.LabelField("Node 2");
        GUILayout.EndArea();



    }
}
