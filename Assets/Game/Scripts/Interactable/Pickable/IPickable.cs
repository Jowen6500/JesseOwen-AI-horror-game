using UnityEngine;

//interface to tag object that will be pickable
public interface IPickable
{
    public void Pickup(PlayerCharacter character);//creating its method with argument "PlayerCharacter"
}
