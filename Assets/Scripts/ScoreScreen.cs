using UnityEngine;
using UnityEngine.UI;

namespace ABCFC
{
	public class ScoreScreen : MonoBehaviour
	{
		#region Variables
		
		[ SerializeField ] GameObject _panel;
		[ SerializeField ] Button _button;
		[ SerializeField ] RectTransform _letterPanelParent;
		[ SerializeField ] LetterScorePanel _letterScorePanel;
		LetterScorePanel[] _letterScorePanels;
		[ SerializeField ] Score _score;

		[ SerializeField ] Vector2 _panelSize;
		
		#endregion

		#region Functions
		
		void Start()
		{
			float x = _letterPanelParent.rect.width * 0.5f;
			float y = _letterPanelParent.rect.height / Mathf.Ceil( _score.LetterScores.Length / 2f );

			//Debug.Log( "x = " + x + "  y = " + y );

			_panelSize = new Vector2( x, y );
		}
		
		public void ToggleActive()
		{
			if( _panel.activeSelf )
			{
				Deactivate();
			}
			else
			{
				Activate();
			}
		}

		void Activate()
		{
			_button.interactable = false;
			_panel.SetActive( true );

			if( _letterScorePanels == null )
			{
				Initialise();
			}
			else
			{
				UpdateThis();
			}
		}

		void Deactivate()
		{
			_button.interactable = true;
			_panel.SetActive( false );
		}

		void Initialise()
		{
			LetterScore[] letterScores = _score.LetterScores;
			int l = letterScores.Length;
			float x = Mathf.Floor( l * 0.5f );
			float y = x * _panelSize.y;
			x = 1f / x;
			_letterScorePanels = new LetterScorePanel[l];
			for( int i = 0; i < l; i ++ )
			{
				LetterScore ls = letterScores[i];
				float f = Mathf.Floor( i * x );
				Vector2 position = new Vector2( _panelSize.x * f, _panelSize.y * - i + y * f );
				_letterScorePanels[i] = Instantiate< LetterScorePanel >( _letterScorePanel );
				_letterScorePanels[i].StartThis( ls.Name, ls.Correct, ls.Incorrect, _letterPanelParent, _panelSize, position );
			}
		}

		void UpdateThis()
		{
			LetterScore[] letterScores = _score.LetterScores;
			int l = letterScores.Length;
			for( int i = 0; i < l; i ++ )
			{
				_letterScorePanels[i].UpdateThis( letterScores[i].Correct, letterScores[i].Incorrect );
			}
		}
		
		#endregion
		
	}
	
}