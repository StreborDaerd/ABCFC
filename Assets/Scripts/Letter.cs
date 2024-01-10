using UnityEngine;

namespace ABCFC
{
	public class Letter : MonoBehaviour
	{
		#region Variables

		int _letterID;
		public int LetterID { get { return _letterID; } }

		[ SerializeField ] AudioSource _letterSoundsSource;

		[ SerializeField ] public AudioClip[] _letterSounds;

		bool _useName = false;

		[ SerializeField ] Rigidbody _rigidbody;
		public Rigidbody Rigidbody { get { return _rigidbody; } }

		[ SerializeField ] string _name;
		public string Name { get { return _name; } }

		[ SerializeField ] float _mass = 0.45f;

		[ SerializeField ] Transform _transform;

		Vector3 _startPo;
		public Vector3 StartPosition { get { return _startPo; } }

		#endregion

		#region Functions
		
		void Start()
		{
			_startPo = _transform.position;
			_rigidbody.Sleep();
		}

		public void Initialise( int letterID, bool useName = false )
		{
			_letterID = letterID;
			_useName = useName;
		}

		public void PlayAnswer()
		{
			_letterSoundsSource.Stop();

			if( ! _useName )
			{
				_letterSoundsSource.clip = _letterSounds[1];
			}
			else
			{
				_letterSoundsSource.clip = _letterSounds[0];
			}

			_letterSoundsSource.Play();
		}

		public void Again()
		{
			Rigidbody.mass = _mass;
		
			_transform.position = _startPo;
		
			_transform.rotation = Quaternion.identity;
		
			_rigidbody.velocity = new Vector3( 0.0f, 0.0f, 0.0f );
		
			_rigidbody.Sleep();
		
		}
		
		#endregion
		
	}
	
}