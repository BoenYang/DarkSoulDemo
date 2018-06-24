using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon {

    public string Name;

    public string AniName;

    public string PunchAniName;

    public string ModelName;

    public bool Mirror;
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

        ChangeLeftHandWeapon(Weapons[0]);
        ChangeRightHandWeapon(Weapons[1]);
    }

    public void ChangeLeftHandWeapon(Weapon weapon) {
        Transform rightHand = animator.GetBoneTransform(HumanBodyBones.RightHand);
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
