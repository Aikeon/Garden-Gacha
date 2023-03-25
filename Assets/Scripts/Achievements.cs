using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public enum ConditionType
{
    Equal,
    GreaterThan,
    GreaterOrEqual,
    LesserThan,
    LesserOrEqual,
    Different
}

[System.Serializable]
public class Condition
{
    public MonoBehaviour ScriptReference{ get => scriptReference; set => scriptReference = value;}
    public MonoBehaviour scriptReference;
    // [System.NonSerialized]
    [ReadOnly]
    public List<string> variables = new List<string>();
    [ListToPopup(typeof(Achievements), "variables")]
    public string variable;

    public float valueCheck;
    public ConditionType conditionType;
    public bool Value => value();
    private bool value()
    {
        var parameter = 0f;
        System.Type myObjectType = scriptReference.GetType();
        System.Reflection.FieldInfo[] memberArray = myObjectType.GetFields();
        foreach (var item in memberArray)
        {
            Debug.Log(item.ToString().Split(' ')[1]);
            if (item.ToString().Split(' ')[1] == variable)
            {
                parameter = (float)item.GetValue(scriptReference);
            }
        }
        switch (conditionType)
        {
            case ConditionType.Equal : return parameter == valueCheck;
            case ConditionType.GreaterThan : return parameter > valueCheck;
            case ConditionType.GreaterOrEqual : return parameter >= valueCheck;
            case ConditionType.LesserThan : return parameter < valueCheck;
            case ConditionType.LesserOrEqual : return parameter <= valueCheck;
            case ConditionType.Different : return parameter != valueCheck;
        }
        return false;
    }

    public void ResetVariables()
    {
        variables.Clear();
        System.Type myObjectType = scriptReference.GetType();
        System.Reflection.FieldInfo[] memberArray = myObjectType.GetFields();
        foreach (var item in memberArray)
        {
            //this if is to look only at variables and forget about methods and constructors...
            Debug.Log(item.ToString().Split(' ')[1]);
            variables.Add(item.ToString().Split(' ')[1]);
            if (item.ToString().Split(' ')[1] == "testInt")
            {
                var value = item.GetValue(scriptReference);
                Debug.Log(value);
            }
        }
    }

    public void OnValidate()
    {
        ResetVariables();
    }
}

[System.Serializable]
public class Achievement
{
    public string name;
    public List<Condition> conditions;
    public bool done;
}

public class ReadOnlyAttribute : PropertyAttribute
 {
 
 }
 
 [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
 public class ReadOnlyDrawer : PropertyDrawer
 {
     public override float GetPropertyHeight(SerializedProperty property,
                                             GUIContent label)
     {
         return EditorGUI.GetPropertyHeight(property, label, true);
     }
 
     public override void OnGUI(Rect position,
                                SerializedProperty property,
                                GUIContent label)
     {
         GUI.enabled = false;
         EditorGUI.PropertyField(position, property, label, true);
         GUI.enabled = true;
     }
 }

public class ListToPopupAttribute : PropertyAttribute
{
    public System.Type myType;
    public string propertyName;

    public ListToPopupAttribute(System.Type _myType, string _propertyName)
    {
        myType = _myType;
        propertyName = _propertyName;
    }
}

[CustomPropertyDrawer(typeof(ListToPopupAttribute))]
public class ListToPopupDrawer : PropertyDrawer
{
    int selectedIndex = 0;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ListToPopupAttribute ttb = attribute as ListToPopupAttribute;
        List<string> stringList = null;

        if (ttb.myType.GetField(ttb.propertyName) != null)
        {
            stringList = ttb.myType.GetField(ttb.propertyName).GetValue(ttb.myType) as List<string>;
        }

        if (stringList != null && stringList.Count != 0)
        {
            selectedIndex = EditorGUI.Popup(position, property.name, selectedIndex, stringList.ToArray());
            property.stringValue = stringList[selectedIndex];
        }
        else EditorGUI.PropertyField(position, property, label);
    }
}

public class Achievements : MonoBehaviour
{
    public List<Achievement> achievements;

    //Update is called once per frame
    void Update()
    {
        foreach (var achievement in achievements)
        {
            if (!achievement.done)
            {
                var b = true;
                foreach (var cond in achievement.conditions)
                {
                    if (!cond.Value)
                    {
                        b = false;
                        break;
                    }
                }
                if (b)
                achievement.done = b;
            }
        }
    }

    void OnValidate()
    {
        foreach(var achievement in achievements)
        {
            foreach (var condition in achievement.conditions)
            {
                condition.OnValidate();
            }
        }
    }
}
