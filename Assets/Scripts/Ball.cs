using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABCFC
{
	public class Ball : MonoBehaviour
	{
		#region Variables
		
		bool _firstKick = true;
		public bool FirstKick { get { return _firstKick; } }

		[ SerializeField ] Transform _transform;

		Vector3 _startPo;
		public Vector3 StartPosition { get { return _startPo; } }

		[ SerializeField ] Rigidbody _rigidbody;

		[ SerializeField ] AudioSource _audioSource;

		[ SerializeField ] float _kickMagnitude = 37.5f;

		#endregion

		#region Functions
		
		void Start()
		{
			_startPo = _transform.position;
		}
		
		public void Kick ( Vector3 kickAt )
		{
			_audioSource.PlayOneShot( _audioSource.clip );
			_rigidbody.velocity = kickAt * _kickMagnitude;
			_firstKick = false;
		}

		void OnCollisionEnter( Collision collision )
		{
			if( ! _firstKick )
			{
				if( ! _audioSource.isPlaying )
				{
					_audioSource.PlayOneShot( _audioSource.clip );
				}
			}
		}

		public void Again()
		{
			_transform.position = _startPo;
			_rigidbody.velocity = new Vector3( 0.0f, 0.0f, 0.0f );
			_rigidbody.Sleep();
			_firstKick = true;
		}
		
		#endregion
		
	}
	
}