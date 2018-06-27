using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour {

    public ActionSlot[] Slots;

    private const string HeavyAttackPrefix = "gs_";

    private const string OhAttackPrefix = "oh_";

    private const string ThAttackPrefix = "th_";

    public ActionSlot GetActionSlot(ActionInputType type) {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].InputType == type) {
                return Slots[i];
            }
        }
        return null;
    }

    public void UpdateLeftActionSlot(Weapon leftWeapon) {
        ActionSlot LTSlot = this.GetActionSlot(ActionInputType.LT);
        WeaponAction lw_lt = leftWeapon.GetAction(ActionInputType.LT);
        LTSlot.AnimationName = lw_lt.AniName;
        LTSlot.Mirror = lw_lt.LeftMirror;
        LTSlot.ActionType = lw_lt.ActionType;

        ActionSlot LBSlot = this.GetActionSlot(ActionInputType.LB);
        WeaponAction lw_lb = leftWeapon.GetAction(ActionInputType.LB);
        LBSlot.AnimationName = lw_lb.AniName;
        LBSlot.Mirror = lw_lb.LeftMirror;
        LBSlot.ActionType = lw_lb.ActionType;
    }

    public void UpdateRightActionSlot(Weapon rightWeapon) {

        ActionSlot RTSlot = this.GetActionSlot(ActionInputType.RT);
        WeaponAction rw_rt = rightWeapon.GetAction(ActionInputType.RT);
        RTSlot.AnimationName = rw_rt.AniName;
        RTSlot.Mirror = rw_rt.LeftMirror;
        RTSlot.ActionType = rw_rt.ActionType;


        ActionSlot RBSlot = this.GetActionSlot(ActionInputType.RB);
        WeaponAction rw_rb = rightWeapon.GetAction(ActionInputType.RB);
        RBSlot.AnimationName = rw_rb.AniName;
        RBSlot.Mirror = rw_rb.LeftMirror;
        RBSlot.ActionType = rw_rb.ActionType;
    }


}
