using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployerFactory 
{
    public static ITargetSelector getTargetSelector(SkillData data)
    {
        ITargetSelector targetSelector = null;
        switch (data.selectorType)
        {
            case SkillData.SelectorType.None:
                targetSelector = null;
                break;
            case SkillData.SelectorType.Circle:
                targetSelector = new CircleSelector();
                break;
            case SkillData.SelectorType.Point:
                targetSelector = new PointSelector();
                break;
        }
        return targetSelector;
    }


    public static List<IEffect> getEffects(SkillData data)
    {
        List<IEffect> effectList = new List<IEffect>();
        if(!data)return effectList;
        foreach(var effectType in data.skillEffects)
        {
            switch (effectType)
            {
                case SkillData.SkillEffectType.reduceHealth:
                    effectList.Add(new ReduceHealthEffect(data));
                    break;
                case SkillData.SkillEffectType.invisiblility:
                    effectList.Add(new InvisibilityEffect(data));
                    break;
                case SkillData.SkillEffectType.LookThroughObject:
                    effectList.Add(new LookThroughObjectEffect(data));
                    break;
                case SkillData.SkillEffectType.placeItem:
                    effectList.Add(new PlaceItemEffect(data));
                    break;
            }
        }

        return effectList;
        
    }

    public static Deployer createDeployer(SkillData data)
    {
        var targetSelector = getTargetSelector(data);
        var effects = getEffects(data);
        

        switch (data.deployerType)
        {
            case SkillData.DeployerType.ToSingle:
                return new ToSingleDeployer(targetSelector, effects);
            case SkillData.DeployerType.ToMultiple:
                return new ToMultipleDeployer(targetSelector, effects);
            case SkillData.DeployerType.Placement:
                return new PlacementDeployer(targetSelector, effects, data.prefab);
        }
        return null;

    }
}
