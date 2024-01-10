using UnityEngine;

namespace ABCFC
{
	delegate void OnEvent();

	public class App : MonoBehaviour
	{
		#region Variables
		
		OnEvent deactivateCurrentPanel;

		[ SerializeField ] OptionsScreen _optionsScreen;

		[ SerializeField ] ScoreScreen _scoreScreen;

		[ SerializeField ] PlayScreen _playScreen;
		
		#endregion

		#region Functions

		void DeactivateCurrentPanel()
		{
			if( deactivateCurrentPanel != null )
			{
				deactivateCurrentPanel();
			}
		}

		public void ActivateOptionsPanel()
		{
			DeactivateCurrentPanel();

			_optionsScreen.ToggleActive();

			deactivateCurrentPanel = DeactivateOptionsPanel;
		}

		void DeactivateOptionsPanel()
		{
			deactivateCurrentPanel = null;

			_optionsScreen.ToggleActive();
		}


		public void ActivateScorePanel()
		{
			DeactivateCurrentPanel();

			_scoreScreen.ToggleActive();

			deactivateCurrentPanel = DeactivateScorePanel;
		}

		void DeactivateScorePanel()
		{
			deactivateCurrentPanel = null;

			_scoreScreen.ToggleActive();
		}


		public void ActivatePlayPanel()
		{
			DeactivateCurrentPanel();

			_playScreen.Activate();
		}

		public void DeactivatePlayPanel()
		{
			deactivateCurrentPanel = null;

			_playScreen.Deactivate();
		}

		public void Exit()
		{
			Application.Quit();
		}
		
		#endregion
		
	}
	
}