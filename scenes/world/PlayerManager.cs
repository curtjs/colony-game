using ColonyGame.scenes.player;
using Godot;

namespace ColonyGame.scenes.world;

public partial class PlayerManager : Node {
    [Export] public PackedScene PlayerScene { get; set; }
    
    public void Start() {
        // Only the host manages players
        if (!Multiplayer.IsServer()) return;
        
        // Spawn in the host player
        SpawnPlayer(1);
        
        // Spawn/remove players when peers connect/disconnect
        Multiplayer.PeerConnected += SpawnPlayer;
        Multiplayer.PeerDisconnected += RemovePlayer;
    }

    public override void _ExitTree() {
        Multiplayer.PeerConnected -= SpawnPlayer;
        Multiplayer.PeerDisconnected -= RemovePlayer;
    }

    private void SpawnPlayer(long peerId) {
        GD.Print($"Adding player for: {peerId}");

        var player = PlayerScene.Instantiate<Player>();
        player.Name = $"{peerId}";
        
        // Each client controls their own player
        player.SetMultiplayerAuthority((int)peerId);
        
        AddChild(player);
    }

    private void RemovePlayer(long peerId) {
        GD.Print($"Player {peerId} has left, removing their player");

        var player = GetNodeOrNull<Player>($"{peerId}");
        player?.QueueFree();
    }
}
