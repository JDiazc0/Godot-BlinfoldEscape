using Godot;
using System;

public partial class CoffeeLighter : Area2D
{
	[Export] public float LightDuration = 0.5f;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			var colorTweener = GetParent().GetNode<ColorTweener>("ColorTweener");
			if (colorTweener != null)
			{
				colorTweener.RestoreLight(LightDuration);
			}

			QueueFree();
		}
	}
}
