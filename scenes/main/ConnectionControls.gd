extends VBoxContainer

signal connected

@onready var host_id_in = $HostIDInput
@onready var online_id_lbl = $OnlineID
@onready var host_btn = $Host
@onready var join_btn = $Join

var peer: NodeTunnelPeer

func _ready() -> void:
	host_btn.disabled = true
	join_btn.disabled = true
	
	peer = NodeTunnelPeer.new()
	multiplayer.multiplayer_peer = peer
	
	peer.connect_to_relay("relay.nodetunnel.io", 9998)
	
	online_id_lbl.text = await peer.relay_connected
	
	host_btn.disabled = false
	join_btn.disabled = false

## Starts hosting a session
## Connected to Host button
func _host() -> void:
	host_btn.disabled = true
	join_btn.disabled = true
	
	peer.host()
	DisplayServer.clipboard_set(peer.online_id)
	
	await peer.hosting
	
	print("Hosting session with ID: ", peer.online_id)
	connected.emit()

## Joins a session
## Connected to Join button
func _join() -> void:
	host_btn.disabled = true
	join_btn.disabled = true
	
	peer.join(host_id_in.text)
	
	await peer.joined
	
	print("Joined session with ID: ", host_id_in.text)
	connected.emit()
