using ColonyGame.scenes.world;
using Godot;

namespace ColonyGame.scenes.main;

public partial class Main : Node {
    private CanvasLayer _ui;
    private World _world;

    public override void _Ready() {
        _ui = GetNode<CanvasLayer>("UI");
        _world = GetNode<World>("World");
    }

    private void StartGame() {
        GD.Print("Starting Game");
        
        _world.Show();
        _ui.Hide();
    }
}
