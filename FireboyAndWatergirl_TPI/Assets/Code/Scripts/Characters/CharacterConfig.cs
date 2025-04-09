using UnityEngine;

/// <summary>
/// A ScriptableObject that holds the configuration data for a character's movement and behavior.
/// This allows different characters to have customized controls and movement characteristics.
/// </summary>
[CreateAssetMenu(fileName = "NewCharacterConfig", menuName = "Character Config", order = 51)]
public class CharacterConfig : ScriptableObject
{
    // The input axis used for horizontal movement (e.g., "Horizontal" or custom input axes)
    public string horizontalInputAxis;

    // The key used to make the character jump (e.g., Spacebar, W key)
    public KeyCode jumpKey;

    // Offset for the character's head position when running (used to adjust the head position during running animations)
    public Vector3 runningHeadOffset;

    // Offset for the character's head position when jumping (used to adjust the head position during jump animations)
    public Vector3 jumpingHeadOffset;
}
