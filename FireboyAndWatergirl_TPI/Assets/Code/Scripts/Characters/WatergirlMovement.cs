using UnityEngine;

public class WatergirlMovement : CharacterMovement
{
    protected override string HorizontalInput => "WatergirlHorizontal";
    protected override KeyCode JumpKey => KeyCode.W;

    // Watergirl's unique head offset
    protected override Vector3 RunningHeadOffset => new Vector3(-0.15f, -0.03f, 0);
    protected override Vector3 JumpingHeadOffset => new Vector3(0, -0.1f, 0);
}
