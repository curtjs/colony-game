namespace ColonyGame.scenes.world;

using Godot;

public partial class World : Node2D {
    private PlayerManager _playerManager;

    public void Start() {
        Show();
        
        // Start player management
        _playerManager = GetNode<PlayerManager>("PlayerManager");
        _playerManager.Start();
    }
}
