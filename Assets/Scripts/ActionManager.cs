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
            if (Slots[i].Type == type) {
                return Slots[i];
            }
        }
        return null;
    }

    public void UpdateLeftActionSlot(Weapon leftWeapon) {
        ActionSlot LTSlot = this.GetActionSlot(ActionInputType.LT);
        LTSlot.AnimationName = ActionManager.OhAttackPrefix + leftWeapon.Actions[0].AniName;
        LTSlot.Mirror = leftWeapon.Actions[0].Mirror;

    }

    public void UpdateRightActionSlot(Weapon rightWeapon) {
        ActionSlot RTSlot = this.GetActionSlot(ActionInputType.RT);
        RTSlot.AnimationName = ActionManager.OhAttackPrefix + rightWeapon.Actions[0].AniName;
        RTSlot.Mirror = rightWeapon.Actions[0].Mirror;
    }


}
