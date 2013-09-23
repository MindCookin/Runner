using UnityEngine;
using System.Collections;

public class PlayerSounds : MonoBehaviour {
	
	public AudioSource source1, source2;
	public AudioClip explosion, pickup, grow, shrink, jump, doubleJump, missileOff;
	
	public void PlaySounds( AudioClip clip ) {
		
		if( clip != jump && clip != doubleJump )
		{
			if ( clip == grow )
				source1.volume = .05f;
			else 
				source1.volume = .7f;
			
			source1.Stop();
			source1.clip = clip;
			source1.Play();
		} else {
			
			source2.clip = clip;
			source2.Play();
		}
	}
}
