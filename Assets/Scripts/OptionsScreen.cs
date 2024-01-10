using UnityEngine;
using UnityEngine.UI;

namespace ABCFC
{
	public class OptionsScreen : MonoBehaviour
	{
		#region Variables
		
		[ SerializeField ] GameObject _panel;
		[ SerializeField ] Button _button;

		[ SerializeField ] Toggle _namesToggle;
		bool _useNames = false;
		public bool UseNames { get { return _useNames; } }
		
		[ SerializeField ] Toggle _randomToggle;
		bool _isRandom = false;
		public bool IsRandom { get { return _isRandom; } }
		
		[ SerializeField ] Toggle _hintsToggle;
		bool _useHints = false;
		public bool UseHints { get { return _useHints; } }

		#endregion

		#region Functions
		
		void Start()
		{
			_namesToggle.isOn = _useNames;
			_randomToggle.isOn = _isRandom;
			_hintsToggle.isOn = _useHints;
		}
		
		public void ToggleActive()
		{
			_button.interactable = _panel.activeSelf;
			_panel.SetActive( ! _panel.activeSelf );
		}
		
		public void ToggleNames()
		{
			_useNames = ! _useNames;
		}
		
		public void ToggleRandom()
		{
			_isRandom = ! _isRandom;
		}
		
		public void ToggleHints()
		{
			_useHints = ! _useHints;
		}

		//void LoadOptions()
		//{

		//}

		//void SaveOptions()
		//{

		//}

		#endregion
		
	}
	
}