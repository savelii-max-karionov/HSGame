using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "Skill/SkillData", fileName = "new skill")]
public class SkillData : ScriptableObject
{
    public enum SkillEffectType
    {
        reduceHealth,
        invisiblility,
        placeItem,
        LookThroughObject
    }

    public enum SelectorType
    {
        Circle,
        Point,
        None,
    }

    public enum PlayerTag
    {
        Escapee,
        Monster,
    }

    public enum DeployerType
    {
        ToSingle,
        ToMultiple,
        Placement
    }

    

    public string id;
    public new string name;
    public string description;
    public float coolDown;
    public DeployerType deployerType;
    public List<PlayerTag> targetTag;
    public List<SkillEffectType> skillEffects;
    public SelectorType selectorType;
    public float damage;
    public float durationTime;
    public float range;
    public Sprite icon;
    public GameObject prefab;

    

}


[CustomEditor(typeof(SkillData))]
public class SkillDataEditor:Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        
    }
}