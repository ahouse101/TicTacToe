using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe
{
	public partial class MainWin : Form
	{
		TicTacToe game;
		Space player;
		Space starter;
		List<List<Button>> grid;
		char playerOneChar = 'X';
		char playerTwoChar = 'O';
		int playerOneScore = 0;
		int playerTwoScore = 0;
		int turns = 0;

		public MainWin()
		{
			InitializeComponent();

			// Keep track of the buttons.
			List<Button> row0 = new List<Button>();
			row0.Add(button00);
			row0.Add(button01);
			row0.Add(button02);
			List<Button> row1 = new List<Button>();
			row1.Add(button10);
			row1.Add(button11);
			row1.Add(button12);
			List<Button> row2 = new List<Button>();
			row2.Add(button20);
			row2.Add(button21);
			row2.Add(button22);
			grid = new List<List<Button>>();
			grid.Add(row0);
			grid.Add(row1);
			grid.Add(row2);

			game = new TicTacToe();
			starter = Space.X;
			player = starter;
		}

		private void button_Click(object sender, EventArgs e)
		{
			Button button = (Button)sender;
			
			string[] coordinates = button.Tag.ToString().Split(',');
			game.setSpace(Int32.Parse(coordinates[0]), Int32.Parse(coordinates[1]), player);
			refreshGUI();
			ChangeNamesButton.Enabled = false;
			StartingPlayerLabel.Visible = false; 

			player = (player == Space.X) ? Space.O : Space.X;
			turns++;

			Space winTest = game.getWinner();
			if (winTest != Space.Empty) gameOver(winTest);
			else if (turns >= 9) gameOver(Space.Empty);
		}

		private void gameOver(Space winner)
		{
			GameOverPanel.Visible = true;
			StartingPlayerLabel.Visible = false; ;
			NewGameButton.Focus();
			if (winner == Space.Empty)
			{
				WinnerLabel.Text = "No winner.";
				CatBox.Visible = true;
			}
			else
			{
				WinnerLabel.Text = "Player \'" + spaceToString(winner) + "\' wins!";
				CatBox.Visible = false;
				if (winner == Space.X)
				{
					++playerOneScore;
					PlayerOneScoreLabel.Text = playerOneScore.ToString();
				}
				else
				{
					++playerTwoScore;
					PlayerTwoScoreLabel.Text = playerTwoScore.ToString();
				}
			}
			for (int i = 0; i < 3; ++i)
			{
				for (int j = 0; j < 3; ++j)
				{
					grid[i][j].BackColor = Color.White;
					grid[i][j].ForeColor = Color.White;
					grid[i][j].Enabled = false;
				}
			}
			ChangeNamesButton.Enabled = true;
		}

		private void resetBoard()
		{
			starter = (starter  == Space.X) ? Space.O : Space.X;
			player = starter;
			game = new TicTacToe();
			turns = 0;
			WinnerLabel.Text = "";
			GameOverPanel.Visible = false;
			for (int i = 0; i < 3; ++i)
			{
				for (int j = 0; j < 3; ++j)
				{
					grid[i][j].BackColor = Color.Turquoise;
					grid[i][j].ForeColor = Color.Turquoise;
				}
			}
			StartingPlayerLabel.Text = "Player " + spaceToString(starter) + " has the first move.";
			StartingPlayerLabel.Visible = true; 
			refreshGUI();
		}

		private void refreshGUI()
		{
			for (int i = 0; i < 3; ++i)
			{
				for (int j = 0; j < 3; ++j)
				{
					Space space = game.readSpace(i, j);
					grid[i][j].Text = spaceToString(space);
					if (space == Space.Empty)
					{
						grid[i][j].Enabled = true;
					}
					else
					{
						grid[i][j].Enabled = false;
					}
				}
			}
		}

		private string spaceToString(Space space)
		{
			switch (space)
			{
				case Space.Empty:
					return "";
					break;
				case Space.O:
					return playerTwoChar.ToString();
					break;
				case Space.X:
					return playerOneChar.ToString();
					break;
				default:
					return "ERROR";
					break;
			}
			return "ERROR";
		}

		private void NewGameButton_Click(object sender, EventArgs e)
		{
			resetBoard();
		}

		private void ChangeNamesButton_Click(object sender, EventArgs e)
		{
			string one = Interaction.InputBox(
				"Enter a one character name for player 1.",
				"Player 1 Name",
				playerOneChar.ToString());
			string two = Interaction.InputBox(
				"Enter a one character name for player 2.",
				"Player 2 Name", 
				playerTwoChar.ToString());
			if (one.Length == 1 && two.Length == 1)
			{
				playerOneChar = one[0];
				playerTwoChar = two[0];
				PlayerOneLabel.Text = "Player " + spaceToString(Space.X) + ": ";
				PlayerTwoLabel.Text = "Player " + spaceToString(Space.O) + ": ";
			}
			else
			{
				MessageBox.Show("Only one character names are allowed.");
			}
			StartingPlayerLabel.Text = "Player " + spaceToString(starter) + " has the first move.";
			StartingPlayerLabel.Visible = true; 
		}

		private void ResetScoreButton_Click(object sender, EventArgs e)
		{
			playerOneScore = 0;
			playerTwoScore = 0;
			PlayerOneScoreLabel.Text = "0";
			PlayerTwoScoreLabel.Text = "0";
		}
	}
}
