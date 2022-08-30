using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIConfig : MonoBehaviour
{
    public SkillManager skillManager;
    GameObject controlledPlayer;
    Button[] buttons;

    private void OnEnable()
    {
        controlledPlayer = GameStatistics.findControledPlayer();

        if(controlledPlayer == null)
        {
            return;
        }

        skillManager = controlledPlayer.GetComponent<SkillManager>();

        buttons = GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() =>
            {
                skillManager.triggerSkill(index);
            });
        }


    } 

}
