using UnityEngine;

namespace ABCFC
{
	[ System.Serializable ]

	public class LettersSet
	{
		#region Variables
		
		[ SerializeField ] string _name;
		public string Name { get { return _name; } }
		
		[ SerializeField ] Letter[] _letters;
		public Letter[] Letters { get { return _letters; } }
		public int Length { get { return _letters.Length; } }

		#endregion

		#region Functions
		
		public void Initialise()
		{
			int l = _letters.Length;
			for( int i = 0; i < l; i ++ )
			{
				_letters[i].Initialise(i);
			}
		}

		#endregion
		
	}
	
}