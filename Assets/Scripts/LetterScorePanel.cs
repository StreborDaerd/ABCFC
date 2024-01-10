using UnityEngine;
using UnityEngine.UI;

namespace ABCFC
{
	public class LetterScorePanel : MonoBehaviour
	{
		#region Variables
		
		[ SerializeField ] Text _letter;
		[ SerializeField ] Text _correct;
		[ SerializeField ] Text _incorrect;

		[ SerializeField ] RectTransform _rectTransform;
		
		#endregion

		#region Functions
		
		public void StartThis( string letter, int correct, int incorrect, Transform parent, Vector2 sizeDelta, Vector2 position )
		{
			_letter.text = letter;
			_correct.text = correct.ToString();
			_incorrect.text = incorrect.ToString();

			_rectTransform.parent = parent;
			
			_rectTransform.sizeDelta = sizeDelta;
			_rectTransform.anchoredPosition = position;
		}

		public void UpdateThis( int correct, int incorrect )
		{
			_correct.text = correct.ToString();
			_incorrect.text = incorrect.ToString();
		}
		
		#endregion
		
	}
	
}