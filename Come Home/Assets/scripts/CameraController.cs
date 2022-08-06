/*

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

	public GameObject target;
	public float followAhead;
	private Vector3 targetPosition;
	public float smoothing;

	public bool followTarget;



	// Use this for initialization
	void Start()
	{
		followTarget = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (followTarget)
		{
			targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);

			if (target.transform.localScale.x > 0f)
			{
				targetPosition = new Vector3(targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
			}
			else
			{
				targetPosition = new Vector3(targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
			}

			//transform.position = targetPosition;
			transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime); //make camera smooth by lerping 
		}

	}




}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    public float cameraSpeedx;
    public float cameraSpeedy;

    public float xRatio = 0.18f;
    public float yRatio = 0.25f;

    void Update()
    {

        float cameraPosX = transform.position.x;
        float cameraPosY = transform.position.y;

        float distanceX = Mathf.Abs(cameraPosX - player.position.x);
        float distanceY = Mathf.Abs(cameraPosY - player.position.y);

        cameraSpeedx = xRatio * distanceX * distanceX;
        cameraSpeedy = yRatio * distanceY * distanceY;

        if (distanceX > 1)
        {
            if (cameraPosX < player.position.x)
            {
                transform.position += new Vector3(cameraSpeedx * Time.deltaTime, 0);
            }
            else
            {
                transform.position -= new Vector3(cameraSpeedx * Time.deltaTime, 0);
            }
        }
        if (distanceY > 1)
        {
            if (cameraPosY < player.position.y)
            {
                transform.position += new Vector3(0, cameraSpeedy * Time.deltaTime);
            }
            else
            {
                transform.position -= new Vector3(0, cameraSpeedy * Time.deltaTime);
            }

        }



    }


}