using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class EventManager
{
    public static Action OnSwitchToUIControllerState;

    public static Func<EquipmentSlotUI, bool> IsSlotItemAssigned;
}
