using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public float parameter;
    public float valueCheck;
    public ConditionType conditionType;
    public bool Value => value();
    public bool value()
    {
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
}

[System.Serializable]
public class Achievement
{
    public string name;
    public Condition condition;
    public bool done;
}

public class Achievements : MonoBehaviour
{
    public List<Achievement> achievements;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
