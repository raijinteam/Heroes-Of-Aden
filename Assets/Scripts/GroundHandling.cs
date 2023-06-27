using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHandling : MonoBehaviour
{
	[Header("X Axis")]
	[SerializeField] private Transform[] all_GroundsOneXAxis;
	[SerializeField] private Transform[] all_GroundsTwoXAxis;
	[SerializeField] private Transform[] all_GroundsThreeXAxis;
	[SerializeField] private float[] all_GroundsDistanceWithPlayerInXAxis;
	[SerializeField] private float xDistanceBetweenTwoGrounds;
	[SerializeField] private float xOffSet;

	[Header("Y Axis")]
	[SerializeField] private Transform[] all_GroundsOneYAxis;
	[SerializeField] private Transform[] all_GroundsTwoYAxis;
	[SerializeField] private Transform[] all_GroundsThreeYAxis;
	[SerializeField] private float[] all_GroundsDistanceWithPlayerInYAxis;
	[SerializeField] private float yDistanceBetweenTwoGrounds;
	[SerializeField] private float yOffset;

	private void Update()
	{
		if (!GameManager.Instance.isGameRunning)
		{
			return;
		}

		HandleXAxisMovement();
		HandleYAxisMovement();
	}

	private void HandleXAxisMovement()
	{
		for (int i = 0; i < all_GroundsOneXAxis.Length; i++)
		{
			all_GroundsDistanceWithPlayerInXAxis[i] = all_GroundsOneXAxis[i].position.x - GameManager.Instance.player.transform.position.x;

			if(all_GroundsDistanceWithPlayerInXAxis[i] < 0)
			{
				// ground is on left of the player
				if(all_GroundsDistanceWithPlayerInXAxis[i] < -xOffSet)
				{
					// ground too far from player, make it jump to right hand side.
					int targetIndex = i - 1;

					if(i == 0)
					{
						targetIndex = all_GroundsOneXAxis.Length - 1;
					}

					Vector3 targetPositionOne = new Vector3(all_GroundsOneXAxis[targetIndex].position.x + xDistanceBetweenTwoGrounds, all_GroundsOneXAxis[i].position.y, all_GroundsOneXAxis[i].position.z);
					Vector3 targetPositionTwo = new Vector3(all_GroundsTwoXAxis[targetIndex].position.x + xDistanceBetweenTwoGrounds, all_GroundsTwoXAxis[i].position.y, all_GroundsTwoXAxis[i].position.z);
					Vector3 targetPositionThree= new Vector3(all_GroundsThreeXAxis[targetIndex].position.x + xDistanceBetweenTwoGrounds, all_GroundsThreeXAxis[i].position.y, all_GroundsThreeXAxis[i].position.z);
					all_GroundsOneXAxis[i].position = targetPositionOne;
					all_GroundsTwoXAxis[i].position = targetPositionTwo;
					all_GroundsThreeXAxis[i].position = targetPositionThree;
				}
			}
			else
			{
				// ground is on right of the player
				if (all_GroundsDistanceWithPlayerInXAxis[i] > xOffSet)
				{
					// ground too far from player, make it jump to left hand side.
					int targetIndex = i + 1;

					if(i == all_GroundsOneXAxis.Length - 1)
					{
						targetIndex = 0;
					}

					Vector3 targetPositionOne = new Vector3(all_GroundsOneXAxis[targetIndex].position.x - xDistanceBetweenTwoGrounds, all_GroundsOneXAxis[i].position.y, all_GroundsOneXAxis[i].position.z);
					Vector3 targetPositionTwo= new Vector3(all_GroundsTwoXAxis[targetIndex].position.x - xDistanceBetweenTwoGrounds, all_GroundsTwoXAxis[i].position.y, all_GroundsTwoXAxis[i].position.z);
					Vector3 targetPositionThree = new Vector3(all_GroundsThreeXAxis[targetIndex].position.x - xDistanceBetweenTwoGrounds, all_GroundsThreeXAxis[i].position.y, all_GroundsThreeXAxis[i].position.z);
					all_GroundsOneXAxis[i].position = targetPositionOne;
					all_GroundsTwoXAxis[i].position = targetPositionTwo;
					all_GroundsThreeXAxis[i].position = targetPositionThree;
				}
			}
		}
	}

	private void HandleYAxisMovement()
	{
		for (int i = 0; i < all_GroundsDistanceWithPlayerInYAxis.Length; i++)
		{
			all_GroundsDistanceWithPlayerInYAxis[i] = all_GroundsOneYAxis[i].position.y - GameManager.Instance.player.transform.position.y;

	
			if (all_GroundsDistanceWithPlayerInYAxis[i] < -yOffset)
			{
				int targetIndex = i - 1;
				if (i == 0)
				{
					targetIndex = all_GroundsOneYAxis.Length - 1;
				}

				Vector3 targetPositionOne = new Vector3(all_GroundsOneYAxis[i].position.x,
														all_GroundsOneYAxis[targetIndex].position.y + yDistanceBetweenTwoGrounds,
														all_GroundsOneYAxis[i].position.z);
				Vector3 targetPositionTwo = new Vector3(all_GroundsTwoYAxis[i].position.x,
														all_GroundsTwoYAxis[targetIndex].position.y + yDistanceBetweenTwoGrounds,
														all_GroundsTwoYAxis[i].position.z);
				Vector3 targetPositionThree = new Vector3(all_GroundsThreeYAxis[i].position.x,
														all_GroundsThreeYAxis[targetIndex].position.y + yDistanceBetweenTwoGrounds,
														all_GroundsThreeYAxis[i].position.z);
				all_GroundsOneYAxis[i].position = targetPositionOne;
				all_GroundsTwoYAxis[i].position = targetPositionTwo;
				all_GroundsThreeYAxis[i].position = targetPositionThree;
			}
			else
			{

				if (all_GroundsDistanceWithPlayerInYAxis[i] > yOffset)
				{
					int targetIndex = i + 1;
					if (i == all_GroundsOneYAxis.Length - 1)
					{
						targetIndex = 0;
					}

					Vector3 targetPositionOne = new Vector3(all_GroundsOneYAxis[i].position.x,
															all_GroundsOneYAxis[targetIndex].position.y - yDistanceBetweenTwoGrounds,
															all_GroundsOneYAxis[i].position.z);
					Vector3 targetPositionTwo = new Vector3(all_GroundsTwoYAxis[i].position.x,
														all_GroundsTwoYAxis[targetIndex].position.y - yDistanceBetweenTwoGrounds,
														all_GroundsTwoYAxis[i].position.z);
					Vector3 targetPositionThree = new Vector3(all_GroundsThreeYAxis[i].position.x,
															all_GroundsThreeYAxis[targetIndex].position.y - yDistanceBetweenTwoGrounds,
															all_GroundsThreeYAxis[i].position.z);
					all_GroundsOneYAxis[i].position = targetPositionOne;
					all_GroundsTwoYAxis[i].position = targetPositionTwo;
					all_GroundsThreeYAxis[i].position = targetPositionThree;
				}
					
			}
		}
	}
}
