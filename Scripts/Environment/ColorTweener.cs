using Godot;
using System;

public partial class ColorTweener : Node2D
{
	[Export]
	public float TransitionDuration = 3f;
	
	private CanvasModulate _canvasModulate;
	
	public override void _Ready()
	{
		_canvasModulate = GetNode<CanvasModulate>("CanvasModulate");
		_canvasModulate.Color = Colors.White;
		
		var tween = CreateTween();
		
		tween.TweenProperty(
			_canvasModulate,
			"color",
			Colors.Black,
			TransitionDuration
		);
		
		tween.SetEase(Tween.EaseType.InOut);
		tween.SetTrans(Tween.TransitionType.Linear);
	}
}
