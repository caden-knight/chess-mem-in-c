using Godot;
using System;
using System.Collections.Generic;


public partial class Singleton : Node
{
	// TODO: get rid of this ↓
	public List<string> coordLetters = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H" };

	public List<Vector2> allCoords = new List<Vector2>() { };
	public Node2D chessBoard;

	public Godot.Collections.Array<Node> squares;

	public enum Piece
	{
		None,
		Pawn,
		Knight,
		Bishop,
		Rook,
		Queen,
		King
	}

	public Piece pieceSelected = Piece.None;





	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		chessBoard = GetNode<Node2D>("/root/ChessMemory/ChessBoard");
		squares = chessBoard.GetChildren();
	}
}
