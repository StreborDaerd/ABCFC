using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABCFC
{
	public class LetterScore
	{
		#region Variables

		int _letterScoreID;
		public int LetterScoreID { get { return _letterScoreID; } }

		string _name;
		public string Name { get { return _name; } }

		int _correct;
		public int Correct { get { return _correct; } }

		int _incorrect;
		public int Incorrect { get { return _incorrect; } }

		//public float averageTimeToAnswer;

		#endregion

		#region Functions
		
		public LetterScore( int letterScoreID, string name, int correct, int incorrect )
		{
			_letterScoreID = letterScoreID;
			_name = name;
			_correct = correct;
			_incorrect = incorrect;
		}
		
		public void CorrectAnswer()
		{
			_correct ++;
		}
		
		public void IncorrectAnswer()
		{
			_incorrect ++;
		}
		
		#endregion
		
	}
	
}