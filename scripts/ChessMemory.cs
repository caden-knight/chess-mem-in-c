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

		// gets the variable from the CheckMovement script
		// ColorRect square = GetNode<ColorRect>("/root/ChessMemory/ChessBoard/ChessSquare");
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
				AnimatedSprite2D squarePiece = square.GetChild(0).GetChild<AnimatedSprite2D>(1);
				string currentCoord = $"{coordLetters[file - 1]}{9 - rank}";
				coordLabel.Text = currentCoord;

				// lays out the squares
				float squareLength = square.GetRect().Size.Y;
				float squareWidth = square.GetRect().Size.X;

				float fileOffset = (file - 1) * squareWidth;
				float rankOffset = (rank - 1) * squareLength;

				square.Position = new Vector2(fileOffset, rankOffset);

				// enures beginning and ending rank squares are colored correctly
				isWhiteSquare = file != 8 ? !isWhiteSquare : isWhiteSquare;

				PlacePieces(currentCoord, squarePiece, square);

				// determines whether or not a square is occupied or empty
				((CheckMovement)square).squareOccupied = squarePiece.Animation == "empty" ? false : true;

				// determines what piece is occupying the square
				((CheckMovement)square).pieceOnSquare = squarePiece.Animation;

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

	private void PlacePieces(string currentCoordinate, AnimatedSprite2D pieceOnSquare, ColorRect square)
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