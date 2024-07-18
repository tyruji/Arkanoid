using Godot;
using System;

public partial class Block : StaticBody2D
{
	private BlockManager _blockManager = null;
	
	public override void _Ready()
	{
		_blockManager = GetNode<BlockManager>( ".." );
		++_blockManager.blockCount;
	}
	
	public void Hit( Vector2 pos )
	{
		
		_blockManager.NotifyBlockHit( pos );
		_blockManager.NotifyBlockDestroy( GlobalPosition );
		QueueFree();
	}
}
