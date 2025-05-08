using Godot;
using System;

public partial class ColorTweener : Node2D
{
	[Export] public float TransitionDuration = 3f;
	
	private CanvasModulate _canvasModulate;
	private Tween _currentTween;
	
	public override void _Ready()
	{
		_canvasModulate = GetNode<CanvasModulate>("CanvasModulate");
		StartDarkening(TransitionDuration);
	}
	
	public void StartDarkening(float duration)
	{
		_currentTween?.Kill();
		_canvasModulate.Color = Colors.White;
		
		_currentTween = CreateTween()
			.SetEase(Tween.EaseType.InOut)
			.SetTrans(Tween.TransitionType.Linear);
			
		_currentTween.TweenProperty(_canvasModulate, "color", Colors.Black, duration);
	}
	
	public void RestoreLight(float restoreDuration)
	{
		_currentTween?.Kill();
		
		_currentTween = CreateTween()
			.SetEase(Tween.EaseType.Out)
			.SetTrans(Tween.TransitionType.Quad);
			
		_currentTween.TweenProperty(_canvasModulate, "color", Colors.White, 0f);
		_currentTween.TweenInterval(restoreDuration);
		_currentTween.TweenCallback(Callable.From(() => StartDarkening(TransitionDuration)));
	}

}
