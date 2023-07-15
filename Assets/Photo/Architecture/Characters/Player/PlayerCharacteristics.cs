using UnityEngine;

namespace Photo.Architecture.Characters.Player
{
    [CreateAssetMenu(fileName = "PlayerCharacteristics", menuName = "Settings/PlayerCharacteristics", order = 51)]
    public class PlayerCharacteristics : ScriptableObject
    {
        public float Speed;
        public float JumpForce;
    }
}