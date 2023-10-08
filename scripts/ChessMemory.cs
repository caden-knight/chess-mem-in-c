using Godot;
using System;
using System.Collections.Generic;

public partial class ChessMemory : Node2D
{
	[Export]
	public Node2D chessBoard;
	private int squareCount = 0;
	private bool isWhiteSquare = true;
	private Vector2 vp;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetupBoard();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void SetupBoard()
	{
		// preload the square scene
		PackedScene squareScene = ResourceLoader.Load<PackedScene>("res://scenes/chess_square.tscn");

		// TODO: invert board if player wants to play as black

		// keep track of current square coordinate
		string[] coordLetters = { "A", "B", "C", "D", "E", "F", "G", "H" };

		vp = GetViewportRect().Size;

		// create the chessboard
		for (int rank = 1; rank <= 8; rank++)
		{
			for (int file = 1; file <= 8; file++)
			{
				ColorRect square = squareScene.Instantiate<ColorRect>();

				// square styling
				square.Color = isWhiteSquare ? Colors.White : Colors.DarkGray;
				Label coordLabel = square.GetChild(0).GetChild<Label>(0);
				TextureRect squarePiece = square.GetChild(0).GetChild<TextureRect>(1);
				string currentCoord = $"{coordLetters[file - 1]}{9 - rank}";
				coordLabel.Text = currentCoord;

				// lays out the squares
				float squareLength = square.GetRect().Size.Y;
				float squareWidth = square.GetRect().Size.X;

				float fileOffset = (file - 1) * squareWidth;
				float rankOffset = (rank - 1) * squareLength;
				square.Position = new Vector2(fileOffset, rankOffset);

				// enures the next rank is colored correctly
				isWhiteSquare = file != 8 ? !isWhiteSquare : isWhiteSquare;

				PlacePieces(currentCoord, squarePiece);


				chessBoard.AddChild(square);
			}
		}

		// get the reference to one of the squares
		ColorRect squareRef = chessBoard.GetChild<ColorRect>(0);

		// calculate the size of the squares. used for centering the board
		float xDifference = vp.X - (squareRef.GetRect().Size.X * 8);
		float yDifference = vp.Y - (squareRef.GetRect().Size.Y * 8);

		// centers the chessboard
		chessBoard.Position = new Vector2(xDifference / 2, yDifference / 2);
	}

	private void PlacePieces(string currentCoordinate, TextureRect pieceOnSquare)
	{
		Texture2D whiteRook = ResourceLoader.Load<Texture2D>("res://assets/w-R.svg");

		switch (currentCoordinate)
		{
			case "A1":
				pieceOnSquare.Texture = whiteRook;
				break;
			case "H1":
				pieceOnSquare.Texture = whiteRook;
				break;
			case "B1":
				pieceOnSquare.Texture = whiteRook;
				break;

			case "G1":
				break;
		}
	}
}
