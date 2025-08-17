extends Node

var peer: NodeTunnelPeer
var online_id: String

func _ready() -> void:
	peer = NodeTunnelPeer.new()
	multiplayer.multiplayer_peer = peer
	
	peer.connect_to_relay("relay.nodetunnel.io", 9998)
	
	online_id = await peer.relay_connected

func host() -> void:
	peer.host()
	DisplayServer.clipboard_set(online_id)

func join(host_online_id: String) -> void:
	peer.join(host_online_id)
