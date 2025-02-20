using UnityEngine;

public class InputActions : Singleton<InputActions>
{
    private PlayerControls input;

    public PlayerControls Input { get { return input; } }
    protected override void Awake()
    {
        base.Awake();

        input = new PlayerControls();
    }

    public void EnableFuseUI()
    {
        input.PlayerAction.Disable();
        input.Arrow_Fuse_UI.Enable();
    }

    public void EnablePlayer()
    {
        input.PlayerAction.Enable();
        input.Arrow_Fuse_UI.Disable();
    }
}
