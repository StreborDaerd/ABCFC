using UnityEngine;

namespace ABCFC
{
	public class PlayScreen : MonoBehaviour
	{
		#region Variables
		
		[ SerializeField ] GameObject _startButtonsPanel;

		[ SerializeField ] GameObject _playPanel;

		[ SerializeField ] GameObject _gameTitle;

		[ SerializeField ] Letters _letters;
		
		#endregion

		#region Functions
		
		public void Activate()
		{
			_startButtonsPanel.SetActive( false );
			_gameTitle.SetActive( false );
			_playPanel.SetActive( true );

			_letters.StartThis();
		}
		
		public void Deactivate()
		{
			_playPanel.SetActive( false );
			_gameTitle.SetActive( true );
			_startButtonsPanel.SetActive( true );

			_letters.StopThis();
			_letters.InstantiateLetters();
		}
		
		#endregion
		
	}
	
}