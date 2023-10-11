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
	private string[] coordLetters = { "A", "B", "C", "D", "E", "F", "G", "H" };


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetupBoard();
	}

	private void SetupBoard()
	{
		// preload the square scene
		PackedScene squareScene = ResourceLoader.Load<PackedScene>("res://scenes/chess_square.tscn");

		// TODO: invert board if player wants to play as black

		// keep track of current square coordinate

		vp = GetViewportRect().Size;

		// create the chessboard
		for (int rank = 1; rank <= 8; rank++)
		{
			for (int file = 1; file <= 8; file++)
			{
				Control squareControl = squareScene.Instantiate<Control>();
				ColorRect square = squareControl.GetChild<ColorRect>(0);

				// square styling
				square.Color = isWhiteSquare ? Colors.White : Colors.DarkGray;
				Label coordLabel = squareControl.GetChild<Label>(1);
				AnimatedSprite2D squarePiece = squareControl.GetChild<AnimatedSprite2D>(2);
				string currentCoord = $"{coordLetters[file - 1]}{9 - rank}";
				coordLabel.Text = currentCoord;


				// lays out the squares
				float squareLength = square.GetRect().Size.Y;
				float squareWidth = square.GetRect().Size.X;

				float fileOffset = (file - 1) * squareWidth;
				float rankOffset = (rank - 1) * squareLength;

				squareControl.Position = new Vector2(fileOffset, rankOffset);

				// enures beginning and ending rank squares are colored correctly
				isWhiteSquare = file != 8 ? !isWhiteSquare : isWhiteSquare;

				PlacePieces(currentCoord, squarePiece);

				// determines whether or not a square is occupied or empty
				((CheckMovement)squareControl).squareOccupied = squarePiece.Animation == "empty" ? false : true;

				// determines what piece is occupying the square
				((CheckMovement)squareControl).pieceOnSquare = squarePiece.Animation;

				chessBoard.AddChild(squareControl);
			}
		}

		// get a reference to one of the squares
		ColorRect squareRef = chessBoard.GetChild<Control>(0).GetChild<ColorRect>(0);

		// calculate the size of the squares. used for centering the board
		float xDifference = vp.X - (squareRef.GetRect().Size.X * 8);
		float yDifference = vp.Y - (squareRef.GetRect().Size.Y * 8);

		// centers the chessboard
		chessBoard.Position = new Vector2(xDifference / 2, yDifference / 2);
	}

	private void PlacePieces(string currentCoordinate, AnimatedSprite2D pieceOnSquare)
	{
		switch (currentCoordinate)
		{
			// Black Rook placement
			case "A8":
				pieceOnSquare.Animation = "b-R";
				break;
			case "H8":
				pieceOnSquare.Animation = "b-R";
				break;

			// Black Knight placement
			case "B8":
				pieceOnSquare.Animation = "b-N";
				break;
			case "G8":
				pieceOnSquare.Animation = "b-N";
				break;

			// Black Bishop placement
			case "C8":
				pieceOnSquare.Animation = "b-B";
				break;
			case "F8":
				pieceOnSquare.Animation = "b-B";
				break;

			// Black King & Queen placement
			case "D8":
				pieceOnSquare.Animation = "b-Q";
				break;
			case "E8":
				pieceOnSquare.Animation = "b-K";
				break;

			// White Rook placement
			case "A1":
				pieceOnSquare.Animation = "w-R";
				break;
			case "H1":
				pieceOnSquare.Animation = "w-R";
				break;

			// White Knight placement
			case "B1":
				pieceOnSquare.Animation = "w-N";
				break;
			case "G1":
				pieceOnSquare.Animation = "w-N";
				break;

			case "C1":
				pieceOnSquare.Animation = "w-B";
				break;
			case "F1":
				pieceOnSquare.Animation = "w-B";
				break;

			// White King and Queen placement
			case "D1":
				pieceOnSquare.Animation = "w-Q";
				break;
			case "E1":
				pieceOnSquare.Animation = "w-K";
				break;
			default:
				pieceOnSquare.Animation = "empty";
				break;
		}

		// placement of Pawns
		pieceOnSquare.Animation = currentCoordinate[1] == '2' ? "w-P" : pieceOnSquare.Animation;
		pieceOnSquare.Animation = currentCoordinate[1] == '7' ? "b-P" : pieceOnSquare.Animation;
	}
}