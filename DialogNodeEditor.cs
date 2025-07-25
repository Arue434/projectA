using UnityEngine;
using UnityEditor;
using System.Collections.Generic; 

[CustomEditor(typeof(DialogNode))]
public class DialogNodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DialogNode dialogNode = (DialogNode)target;

        dialogNode.characterName = EditorGUILayout.TextField("nama char", dialogNode.characterName);

        EditorGUILayout.LabelField("teks dialog");
        dialogNode.dialogText = EditorGUILayout.TextArea(dialogNode.dialogText, GUILayout.Height(60));

        dialogNode.dialogSprite = (Sprite)EditorGUILayout.ObjectField("dialog sprite", dialogNode.dialogSprite, typeof(Sprite), false);

        dialogNode.hasAskOption = EditorGUILayout.Toggle("pertanyaan", dialogNode.hasAskOption);

        if (dialogNode.hasAskOption)
        {
            EditorGUILayout.LabelField("tanya");
            if (dialogNode.askOptions == null)
                dialogNode.askOptions = new List<DialogNode.AskOption>();

            for (int i = 0; i < dialogNode.askOptions.Count; i++)
            {
                EditorGUILayout.BeginVertical("box");

                dialogNode.askOptions[i].responseText = EditorGUILayout.TextField($"Response Text {i + 1}", dialogNode.askOptions[i].responseText);
                dialogNode.askOptions[i].nextNode = (DialogNode)EditorGUILayout.ObjectField("Next Node", dialogNode.askOptions[i].nextNode, typeof(DialogNode), false);

                if (GUILayout.Button("hapus opsi"))
                {
                    dialogNode.askOptions.RemoveAt(i);
                }

                EditorGUILayout.EndVertical();
            }

            if (GUILayout.Button("tambah opsi tanya"))
            {
                dialogNode.askOptions.Add(new DialogNode.AskOption());
            }
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Next Node");
        if (dialogNode.nextNodes == null)
            dialogNode.nextNodes = new List<DialogNode>();

        for (int i = 0; i < dialogNode.nextNodes.Count; i++)
        {
            dialogNode.nextNodes[i] = (DialogNode)EditorGUILayout.ObjectField($"Node {i + 1}", dialogNode.nextNodes[i], typeof(DialogNode), false);
        }

        if (GUILayout.Button("tambah node berikutnya"))
        {
            dialogNode.nextNodes.Add(null);
        }

        if (GUILayout.Button("hapus node terakhir"))
        {
            if (dialogNode.nextNodes.Count > 0)
                dialogNode.nextNodes.RemoveAt(dialogNode.nextNodes.Count - 1);
        }

        if (GUILayout.Button("validasi opsi tanya"))
        {
            dialogNode.ValidateAskOptions();
        }

        if (GUILayout.Button("hapus duplikat opsi tanya"))
        {
            dialogNode.RemoveDuplicates();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(dialogNode);
        }
    }
}
