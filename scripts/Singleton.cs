using Godot;
using System;
using System.Collections.Generic;


public partial class Singleton : Node
{
	public List<string> coordLetters = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H" };

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
}
