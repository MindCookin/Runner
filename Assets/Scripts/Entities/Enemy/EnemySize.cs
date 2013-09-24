using UnityEngine;
using System.Collections;

public class EnemySize : MonoBehaviour
{
	public float minSize, reductionSpeed;

	// Use this for initialization
	public void Shrink()
	{
		if ( transform.localScale.x > minSize )
			transform.localScale -= Vector3.one * reductionSpeed;
	}
}

