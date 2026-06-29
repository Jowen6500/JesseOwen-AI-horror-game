using UnityEngine;

public class Battery : Item
{
    public override void Pickup(PlayerCharacter character)//override pickup function
    {
        base.Pickup(character);//call pickup function from base/parent
        character.Flashlight.CallRefillBatteryLevel(25);//call refill battery function from PlayerCharacter
        
        HUDManager.Instance.BatteryUI.BatteryBG.CrossFadeAlpha(1, 0.5f, false);//invoke crossfade StaminaBG Alpha to 1
        HUDManager.Instance.BatteryUI.BatteryFill.CrossFadeAlpha(1, 0.5f, false);//invoke crossfade StaminaFill Alpha to 1
    }
}
