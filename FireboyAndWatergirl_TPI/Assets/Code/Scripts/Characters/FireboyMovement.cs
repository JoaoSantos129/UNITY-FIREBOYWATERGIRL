using UnityEngine;

public class FireboyMovement : CharacterMovement
{
    protected override string HorizontalInput => "FireboyHorizontal";
    protected override KeyCode JumpKey => KeyCode.UpArrow;

    // Fireboy's unique head offset
    protected override Vector3 RunningHeadOffset => new Vector3(-0.2f, -0.18f, 0);
    protected override Vector3 JumpingHeadOffset => new Vector3(0, -0.1f, 0);
}
