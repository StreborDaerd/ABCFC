using UnityEngine;
using UnityEngine.UI;

namespace ABCFC
{
	public class Score : MonoBehaviour
	{
		#region Variables
		
		LetterScore[] _letterScores;
		public LetterScore[] LetterScores { get { return _letterScores; } }

		[ SerializeField ] Letters _letters;

		int _correct = 0;
		int _incorrect = 0;

		[ SerializeField ] Text _correctText;
		[ SerializeField ] Text _incorrectText;

		#endregion

		#region Functions
		
		void Start()
		{
			LoadScore();
		}

		public void LoadScore()
		{
			Letter[] letters = _letters.LettersSet.Letters;
			int l = letters.Length;
			_letterScores = new LetterScore[l];
			string setName = _letters.LettersSet.Name;
			for( int i = 0; i < l; i ++ )
			{
				int correct = 0;
				if( PlayerPrefs.HasKey( setName + "_c_" + i ) )
				{
					correct = PlayerPrefs.GetInt( setName + "_c_" + i );
				}

				int incorrect = 0;
				if( PlayerPrefs.HasKey( setName + "_i_" + i ) )
				{
					incorrect = PlayerPrefs.GetInt( setName + "_i_" + i );
				}

				_letterScores[i] = new LetterScore( i, letters[i].Name, correct, incorrect );
			}
		}

		public void Correct( int letterID )
		{
			_letterScores[ letterID ].CorrectAnswer();
			_correct ++;
			_correctText.text = _correct.ToString();
		}
		
		public void Incorrect( int letterID )
		{
			_letterScores[ letterID ].IncorrectAnswer();
			_incorrect ++;
			_incorrectText.text = _incorrect.ToString();
		}

		public void StartThis()
		{

		}

		public void StopThis()
		{

		}

		public void SaveScore()
		{
			int l = _letters.LettersSet.Length;
			string setName = _letters.LettersSet.Name;
			for( int i = 0; i < l; i ++ )
			{
				PlayerPrefs.SetInt( setName + "_c_" + i, _letterScores[i].Correct );
				PlayerPrefs.SetInt( setName + "_i_" + i, _letterScores[i].Incorrect );
			}
		}
		
		#endregion
		
	}
	
}