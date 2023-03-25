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
    // [ReadOnly]
    public List<string> variables_do_not_modify = new List<string>();
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
        if (scriptReference == null) return;
        variables_do_not_modify.Clear();
        System.Type myObjectType = scriptReference.GetType();
        System.Reflection.FieldInfo[] memberArray = myObjectType.GetFields();
        foreach (var item in memberArray)
        {
            variables_do_not_modify.Add(item.ToString().Split(' ')[1]);
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
    public string description;
    public float bounty;
    public List<Condition> conditions;
    public bool done;
}

// public class ReadOnlyAttribute : PropertyAttribute
//  {
 
//  }
 
//  [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
//  public class ReadOnlyDrawer : PropertyDrawer
//  {
//      public override float GetPropertyHeight(SerializedProperty property,
//                                              GUIContent label)
//      {
//          return EditorGUI.GetPropertyHeight(property, label, true);
//      }
 
//      public override void OnGUI(Rect position,
//                                 SerializedProperty property,
//                                 GUIContent label)
//      {
//          GUI.enabled = false;
//          EditorGUI.PropertyField(position, property, label, true);
//          GUI.enabled = true;
//      }
//  }

public class Achievements : MonoBehaviour
{
    public static Achievements Instance;

    void Awake()
    {
        if (Instance != null) Destroy(this);
        Instance = this;
    }

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
                {
                    achievement.done = b;
                    AudioManager.Instance.PlaySFX("AchievementGet");
                }
                
            }
        }
    }

    void OnValidate()
    {
        if (achievements == null) return;
        foreach(var achievement in achievements)
        {
            foreach (var condition in achievement.conditions)
            {
                condition.OnValidate();
            }
        }
    }
}
