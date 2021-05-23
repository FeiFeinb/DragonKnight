
using UnityEngine;
using UnityEditor;
[System.Serializable]
public class Person
{
    public int age;
    public int height;
}

public class Children : MonoBehaviour
{
    [SerializeField] private Person p1;
    private Person p2;

    [ContextMenu("UndoTest")]
    private void UndoTest()
    {
        Undo.RecordObject(this, "Change Info");
        p1.age = 100;
        p1.height = 1000;
        p2.age = 200;
        p2.height = 2000;
    }
}