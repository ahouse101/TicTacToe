using System.Collections.Generic;

namespace TicTacToe
{
	public class TicTacToe
	{
		List<List<Space>> gameboard;

		public TicTacToe()
		{
			gameboard = new List<List<Space>>();
			// Initialize with empty spaces.
			for (int i = 0; i < 3; ++i)
			{
				gameboard.Add(new List<Space>());
				for (int j = 0; j < 3; ++j)
				{
					gameboard[i].Add(Space.Empty);
				}
			}
		}

		public Space getWinner()
		{
			Space start = gameboard[0][0];
			if (start != Space.Empty)
			{
				if ((gameboard[0][1] == start &&
					gameboard[0][2] == start) ||
					(gameboard[1][0] == start &&
					gameboard[2][0] == start) ||
					(gameboard[1][1] == start &&
					gameboard[2][2] == start))
					return start;
			}
			start = gameboard[0][1];
			if (start != Space.Empty)
			{
				if ((gameboard[1][1] == start &&
					gameboard[2][1] == start))
					return start;
			}
			start = gameboard[0][2];
			if (start != Space.Empty)
			{
				if ((gameboard[1][2] == start &&
					gameboard[2][2] == start))
					return start;
			}
			start = gameboard[1][0];
			if (start != Space.Empty)
			{
				if ((gameboard[1][1] == start &&
					gameboard[1][2] == start))
					return start;
			}
			start = gameboard[2][0];
			if (start != Space.Empty)
			{
				if ((gameboard[2][1] == start &&
					gameboard[2][2] == start))
					return start;
				if ((gameboard[1][1] == start &&
					gameboard[0][2] == start))
					return start;
			}
			return Space.Empty;
		}

		// Returns true if the space was set.
		public bool setSpace(int row, int column, Space value)
		{
			gameboard[row][column] = value;
			return true;
		}

		// Returns true if the space was set.
		public Space readSpace(int row, int column)
		{
			return gameboard[row][column];
		}
	}

	public enum Space 
	{
		Empty,
		X,
		O
	}
}
