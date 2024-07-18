using Godot;
using System;

public partial class GameOverArea : Area2D
{
	[Export]
	private PackedScene _GameOverScene = null;
	
	private void OnBodyEntered( Node2D body )
	{
		if( body is not Ball ) return;
		
		CallDeferred( nameof( GameOverScene ) );
	}
	
	private void GameOverScene()
	{
		GetTree().ChangeSceneToPacked( _GameOverScene );
	}
}


