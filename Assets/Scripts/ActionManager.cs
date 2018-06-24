using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour {

    [System.Serializable]
    public class ActionSlot {
        public ActionSlotType Type;
        public string AnimationName;
        public bool Mirror;
    }

    public enum ActionSlotType
    {
        LT,
        LB,
        RT,
        RB
    }

    public ActionSlot[] Slots;

    private const string HeavyAttackPrefix = "gs_";

    private const string OhAttackPrefix = "oh_";

    private const string ThAttackPrefix = "th_";

    public ActionSlot GetActionSlot(ActionSlotType type) {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].Type == type) {
                return Slots[i];
            }
        }
        return null;
    }

    public void UpdateLeftActionSlot(Weapon leftWeapon) {
        ActionSlot LTSlot = this.GetActionSlot(ActionSlotType.LT);
        LTSlot.AnimationName = ActionManager.OhAttackPrefix + leftWeapon.AniName;
        LTSlot.Mirror = leftWeapon.Mirror;

    }

    public void UpdateRightActionSlot(Weapon rightWeapon) {
        ActionSlot RTSlot = this.GetActionSlot(ActionSlotType.RT);
        RTSlot.AnimationName = ActionManager.OhAttackPrefix + rightWeapon.AniName;
        RTSlot.Mirror = rightWeapon.Mirror;
    }


}
