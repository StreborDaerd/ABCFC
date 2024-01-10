using UnityEngine;

namespace ABCFC
{
	public class Letters : MonoBehaviour
	{
		#region Variables
		
		OnEvent OnUpdate;

		[ SerializeField ] OptionsScreen _optionsScreen;

		int[] _letterOrder;
		
		[ SerializeField ] LettersSet[] _lettersSets;
		public LettersSet LettersSet { get { return _lettersSets[ _letterSet ]; } }

		[ SerializeField ] int _letterSet = 0;
		
		Vector3 _startPo = new Vector3( -2.0f, 1.0f, -3.0f );

		Letter[] _currentLetters;

		int _letter = 0;//this and 2 after are shown and part of this round, increments by 3 to go to new set of letters

		int _chosenLetter = 0;
		
		int _timesChosen = 0;

		[ SerializeField ] Ball _ball;

		[ SerializeField ] Score _score;

		[ SerializeField ] Camera _camera;

		[ SerializeField ] LayerMask _raycastMask;

		#endregion

		#region Functions
		
		void Start()
		{
			LettersSet.Initialise();
			InstantiateLetters();
		}

		void Update()
		{
			if( OnUpdate != null )
			{
				OnUpdate();
			}
		}

		public void StartThis()
		{
			if( _optionsScreen.IsRandom )
			{
				PopulateLetterOrder();
				RandomiseLetterOrder();
			}

			DestroyLetters();

			InstantiateLetters();
			ChooseLetter();
			_currentLetters[ _chosenLetter ].PlayAnswer();
			OnUpdate += PlayLoop;
			_ball.Again();

		}

		public void StopThis()
		{
			DestroyLetters();

			OnUpdate -= PlayLoop;

			_score.StopThis();
			_score.SaveScore();
		}

		public void InstantiateLetters()
		{
			if( ! _optionsScreen.IsRandom )
			{
				PopulateLetterOrder();
			}

			Vector3 v = _startPo;

			_currentLetters = new Letter[3];
			
			int l = _letter;
			
			bool hint = _optionsScreen.UseHints;
			bool names = _optionsScreen.UseNames;

			for( int i = 0; i < 3; i ++ )
			{
				int ll = LettersSet.Length;
				if( l >= ll )
				{
					l = l - ll;
				}

				_currentLetters[i] = Instantiate< Letter >( LettersSet.Letters[ _letterOrder[l] ], v, Quaternion.identity );
				_currentLetters[i].Initialise( LettersSet.Letters[ _letterOrder[l] ].LetterID, _optionsScreen.UseNames );

				v.x += 2.0f;
				l ++;
			}

		}

		public void RandomiseLetterOrder()
		{
			int l = _letterOrder.Length;
			for( int i = 0; i < l; i ++ )
			{
				int t = _letterOrder[i];
				int j = Random.Range( i, l );
				_letterOrder[i] = _letterOrder[j];
				_letterOrder[j] = t;
			}

			SeparateHomophones();
		}

		void PopulateLetterOrder()
		{
			int l = LettersSet.Length;
			
			_letterOrder = new int[l];
			
			for( int i = 0; i < l; i ++ )
			{
				_letterOrder[i] = i;
			}
		}

		void SeparateHomophones()
		{
			int c = -1;
			int k = -1;
			int d = 0;
			int l = _letterOrder.Length;
			for( int i = 0; i < l; i ++ )
			{
				if( _letterOrder[i] == 2 )// letter "c"
				{
					c = i;
				}
				else if( _letterOrder[i] == 10 )// letter "k"
				{
					k = i;
				}
			}
			d = Mathf.Abs( c - k );

			while( d > ( l - 3 ) || d < 3 )
			{
				int r = Random.Range( 0, l );
				_letterOrder[c] = _letterOrder[r];
				_letterOrder[r] = 2;
				c = r;
				d = Mathf.Abs( c - k );
			}
		}

		void DestroyLetters()
		{
			if( _currentLetters == null )
			{
				return;
			}

			int l = _currentLetters.Length;

			for( int i = 0; i < l; i ++ )
			{
				if( _currentLetters[i].gameObject != null )
				{
					Destroy( _currentLetters[i].gameObject );
				}
				else
				{
					Debug.Log( "Letters DestroyLetters() currentLetters[i].gameObject == null" );
				}
			}

			_currentLetters = new Letter[0];
		}

		void ChooseLetter()
		{
			int r = Random.Range( 0, 3 );
			if( r == _chosenLetter )
			{
				_timesChosen ++;
			}
			else
			{
				_timesChosen = 0;
			}

			if( _timesChosen > 3 )
			{
				do
				{
					r = Random.Range( 0, 3 );
				}
				while( r == _chosenLetter );
			}

			_chosenLetter = r;

		}

		void LettersOnInput( Letter letter )
		{
			if( _ball.FirstKick )
			{
				if( letter.Name == _currentLetters[ _chosenLetter ].Name )
				{
					_score.Correct( _currentLetters[ _chosenLetter ].LetterID );
				}
				else
				{
					letter.Rigidbody.mass = 1000.0f;
					_score.Incorrect( _currentLetters[ _chosenLetter ].LetterID );
				}
			}

			Vector3 relativePos = letter.StartPosition - _ball.StartPosition;
			relativePos += Vector3.up * 0.85f;
			relativePos.Normalize();
			_ball.Kick( relativePos );
			letter.PlayAnswer();
		}
		
//#if UNITY_EDITOR
		void PlayLoop()
		{
			if( Input.GetMouseButtonUp(0) )
			{
				Ray ray = _camera.ScreenPointToRay( Input.mousePosition );
				RaycastHit hit;
				if( Physics.Raycast( ray, out hit, 50.0f, _raycastMask ) )
				{
					//Debug.Log( "PlayLoop raycast hit " + hit.collider.gameObject.name );

					Letter letter = hit.collider.GetComponent< Letter >();

					if( letter != null )
					{
						LettersOnInput( letter );
					}
				}
			}
		}
//#elif UNITY_ANDROID
//		void PlayLoop()
//		{
//			if( Input.touchCount > 0 )
//			{
//				Touch touch = Input.touches[0];
//				if( touch.tapCount > 0 )
//				{
//					Ray ray = _camera.ScreenPointToRay( touch.position );
//					RaycastHit hit;
//					if( Physics.Raycast( ray, out hit, 50.0f, _raycastMask ) )
//					{
//						//Debug.Log( "PlayLoop raycast hit " + hit.collider.gameObject.name );

//						Letter letter = hit.collider.GetComponent< Letter >();

//						if( letter != null )
//						{
//							LettersOnInput( letter );
//						}
//					}
//				}
//			}
//		}
//#endif

		public void Next()
		{
			DestroyLetters();
			_letter += 3;
			int l = LettersSet.Length;
			if( _letter >= l )
			{
				_letter = _letter - ( l );
			}
			_ball.Again();
			InstantiateLetters();
			ChooseLetter();
			_currentLetters[ _chosenLetter ].PlayAnswer();
		}

		public void Back()
		{
			DestroyLetters();
			_letter -= 3;
			int l = LettersSet.Length;
			if( _letter < 0 )
			{
				_letter = l + _letter;
			}
			_ball.Again();
			InstantiateLetters();
			ChooseLetter();
			_currentLetters[ _chosenLetter ].PlayAnswer();
		}

		public void Again()
		{
			if( ! _ball.FirstKick )
			{
				for( int i = 0; i < 3; i ++ )
				{
					_currentLetters[i].Again();
				}
				ChooseLetter();
				_currentLetters[ _chosenLetter ].PlayAnswer();
				_ball.Again();
			}
			else
			{
				_currentLetters[ _chosenLetter ].PlayAnswer();
			}
		}

#endregion
		
	}
	
}