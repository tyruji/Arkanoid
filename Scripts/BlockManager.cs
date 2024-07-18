using Godot;
using System;

public partial class BlockManager : Node2D
{
	public int blockCount = 0;
	
	public event Action<Vector2> OnBlockHit;
	public event Action<Vector2> OnBlockDestroy;
	
	public void NotifyBlockHit( Vector2 position )
	{
		OnBlockHit?.Invoke( position );
	}
	
	public void NotifyBlockDestroy( Vector2 position )
	{
		OnBlockDestroy?.Invoke( position );
		--blockCount;
		
		if( blockCount != 0 ) return;
		
		NextLevel();
	}
	
	private void NextLevel()
	{
		GD.Print( "next level" );
	}
}
