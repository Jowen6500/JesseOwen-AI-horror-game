using UnityEngine;

public class Battery : Item
{
    public override void Pickup(PlayerCharacter character)//override pickup function
    {
        base.Pickup(character);//call pickup function from base/parent
        character.Flashlight.CallRefillBatteryLevel(25);//call refill battery function from PlayerCharacter
    }
}
