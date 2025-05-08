using Godot;
using System;

public partial class DeadZone : Area2D
{
	[Export] public string _playerGroup = "Player";
	
	private CollisionShape2D _col;
	
	
	public override void _Ready()
	{
		_col = GetNode<CollisionShape2D>("CollisionShape2D");
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup(_playerGroup))
		{
			GameManager.Instance.LoadCurrentLevel();
		}
	}
}
