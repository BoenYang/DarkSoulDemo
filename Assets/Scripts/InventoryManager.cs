using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon {

    public string Name;

    public string ModelName;

    public WeaponAction[] Actions;

    public WeaponAction GetAction(ActionInputType type) {
        for (int i = 0; i < Actions.Length; i++) {
            if (Actions[i].InputType == type) {
                return Actions[i];
            }
        }
        return null;
    }
}

public enum ActionType {
    Attack,
    Blocking,
    Spells,
    Parry
}

[System.Serializable]
public class ActionSlot
{
    public ActionType ActionType;
    public ActionInputType InputType;
    public string AnimationName;
    public bool Mirror;
}

public enum ActionInputType
{
    LT,
    LB,
    RT,
    RB
}

[System.Serializable]
public class WeaponAction {
    public string AniName;
    public bool LeftMirror;
    public ActionType ActionType;
    public ActionInputType InputType;
}

public class InventoryManager : MonoBehaviour {

    [System.NonSerialized]
    public Weapon LeftHandWeapon = null;

    [System.NonSerialized]
    public Weapon RightHandWeapon = null;

    public List<Weapon> Weapons;

    PlayerController controller;

    ActionManager actionManager;

    Animator animator;

    public void Init(PlayerController controller, ActionManager actionManager) {
        this.controller = controller;
        this.actionManager = actionManager;
        animator = this.controller.GetComponent<Animator>();

        ChangeLeftHandWeapon(Weapons[1]);
        ChangeRightHandWeapon(Weapons[0]);
    }

    public void ChangeLeftHandWeapon(Weapon weapon) {
        LeftHandWeapon = weapon;

        Transform rightHand = animator.GetBoneTransform(HumanBodyBones.LeftHand);
        Transform weaponRoot = null;
        weaponRoot = rightHand.Find("weapons");

        if (weaponRoot == null) {
            return;
        }

        foreach (Transform tr in weaponRoot)
        {
            tr.gameObject.SetActive(false);
        }

        if (weaponRoot != null && weapon != null)
        {
            animator.Play("left_idle");
            Transform weaponTr = weaponRoot.Find(weapon.ModelName);
            weaponTr.gameObject.SetActive(true);
        }

        actionManager.UpdateLeftActionSlot(weapon);
    }

    public void ChangeRightHandWeapon(Weapon weapon) {
        RightHandWeapon = weapon;

        Transform rightHand = animator.GetBoneTransform(HumanBodyBones.RightHand);
        Transform weaponRoot = null;
        weaponRoot = rightHand.Find("weapons");

        if (weaponRoot == null)
        {
       
            return;
        }

        foreach (Transform tr in weaponRoot)
        {
            tr.gameObject.SetActive(false);
        }

        if (weaponRoot != null && weapon != null)
        {
            animator.Play("right_idle");
            Transform weaponTr = weaponRoot.Find(weapon.ModelName);
            weaponTr.gameObject.SetActive(true);
        }

        actionManager.UpdateRightActionSlot(weapon);
    }

}
