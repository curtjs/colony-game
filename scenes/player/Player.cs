using Godot;

namespace ColonyGame.scenes.player;

public partial class Player : CharacterBody2D {
    [Export] public float Speed = 200.0f;

    public override void _EnterTree() {
        SetMultiplayerAuthority(Name.ToString().ToInt());
    }

    public override void _PhysicsProcess(double delta) {
        // Only process input for the player we control
        if (!IsMultiplayerAuthority()) return;

        HandleMovement(delta);
    }

    private void HandleMovement(double delta) {
        Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_up", "move_down");
    
        Velocity = inputDir * Speed;

        MoveAndSlide();
    }
}