using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TimeControlled))]
public class TimeControlledEditor : Editor
{
    //* �б�����ʹ�����嵱ǰλ��
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TimeControlled timeControlledScript = (TimeControlled)target;
        SerializedObject serializedTimeControlled = new SerializedObject(timeControlledScript);

        if (GUILayout.Button("Add New TimeState (Current Position)"))
        {
            Undo.RecordObject(timeControlledScript, "Add New TimeState");
            Vector3 currentPosition = timeControlledScript.transform.position;
            timeControlledScript.TimeStates.Add(new TimeControlled.TimeState()
            {
                time = 0,
                state = false,
                position = currentPosition           });
            EditorUtility.SetDirty(timeControlledScript);
        }

        serializedTimeControlled.ApplyModifiedProperties();
    }
}
