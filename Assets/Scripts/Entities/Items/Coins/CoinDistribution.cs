using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CoinDistribution
{
	public static Vector3 RandomDistribution( Queue<Transform> coinQueue, Vector3 nextPosition )
	{
		int index = Random.Range(0, 3);
		Vector3 response = Vector3.zero;
		
		switch ( index )
		{
			case 0 : response = DrawLine( coinQueue, nextPosition ); break;
//			case 1 : response = DrawSquare( coinQueue, nextPosition ); break;
//			case 2 : response = DrawCircle( coinQueue, nextPosition ); break;
			case 1 : response = DrawFullWave( coinQueue, nextPosition ); break;
			case 2 : response = DrawHalfWave( coinQueue, nextPosition ); break; 	
		}
		
		return response;
	}
	
	public static Vector3 DrawLine( Queue<Transform> coinQueue, Vector3 nextPosition )
	{
		Transform actualCoin;
		
		for ( int i = 0; i < coinQueue.Count; i++ )
		{
			actualCoin = coinQueue.Dequeue();
			actualCoin.GetComponent<Coin>().Reset();
			nextPosition.x += 3;
			actualCoin.position = nextPosition;
			coinQueue.Enqueue( actualCoin );
		}
		
		return nextPosition;
	}
	
	
	public static Vector3 DrawSquare( Queue<Transform> coinQueue, Vector3 nextPosition )
	{ 
		Transform actualCoin;
		float startingX = nextPosition.x; 
		float startingY = nextPosition.y; 
		
		for ( int i = 0; i < 9; i++ )
		{
			actualCoin = coinQueue.Dequeue();
			actualCoin.GetComponent<Coin>().Reset();
			
			nextPosition.x = startingX + ((i)/3 ) * 3f;
			nextPosition.y = startingY + ((i)%3 ) * 3f;
			
			actualCoin.position = nextPosition;
			coinQueue.Enqueue( actualCoin );
		}
		
		actualCoin = coinQueue.Dequeue();
		actualCoin.GetComponent<Coin>().Disable();
		coinQueue.Enqueue( actualCoin );
		
		Vector3 lastCoinPosition = nextPosition;
		lastCoinPosition.y 		 = startingY;
		
		return lastCoinPosition;
	}
	
	public static Vector3 DrawCircle( Queue<Transform> coinQueue, Vector3 nextPosition )
	{ 
		Transform actualCoin;
		float startingX = nextPosition.x; 
		float startingY = nextPosition.y; 
		
		for ( float i = 0; i < coinQueue.Count; i++ )
		{
			actualCoin = coinQueue.Dequeue();
			actualCoin.GetComponent<Coin>().Reset();
			nextPosition.x = startingX + Mathf.Cos( i / 1.6f ) * 5f;
			nextPosition.y = startingY + Mathf.Sin( i / 1.6f ) * 3f;
			actualCoin.position = nextPosition;
			coinQueue.Enqueue( actualCoin );
		}
		
		Vector3 lastCoinPosition 	= nextPosition;
		lastCoinPosition.y 	= startingY;
		
		return lastCoinPosition;
	}
	
	public static Vector3 DrawFullWave( Queue<Transform> coinQueue, Vector3 nextPosition )
	{ 
		Transform actualCoin;
		float startingY = nextPosition.y; 
		
		for ( float i = 0; i < coinQueue.Count; i++ )
		{
			actualCoin = coinQueue.Dequeue();
			actualCoin.GetComponent<Coin>().Reset();
			nextPosition.x += 3;
			nextPosition.y = startingY + Mathf.Sin( i / 1f ) * 2.5f;
			actualCoin.position = nextPosition;
			coinQueue.Enqueue( actualCoin );
		}
		
		Vector3 lastCoinPosition 	= nextPosition;
		lastCoinPosition.y 	= startingY;
		
		return lastCoinPosition;
	}
	
	public static Vector3 DrawHalfWave( Queue<Transform> coinQueue, Vector3 nextPosition )
	{ 
		Transform actualCoin;
		float startingY = nextPosition.y; 
		
		for ( float i = 0; i < coinQueue.Count; i++ )
		{
			actualCoin = coinQueue.Dequeue();
			actualCoin.GetComponent<Coin>().Reset();
			nextPosition.x += 3;
			nextPosition.y = startingY + Mathf.Sin( i / 2.8f ) * 5;
			actualCoin.position = nextPosition;
			coinQueue.Enqueue( actualCoin );
		}
		
		Vector3 lastCoinPosition 	= nextPosition;
		lastCoinPosition.y 			= startingY;
		
		return lastCoinPosition;
	}
}

