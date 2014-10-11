using UnityEngine;
using System.Collections;

public class UiController : MonoBehaviour
{
	[SerializeField]
	private float inputTimer = 1.5f;
	private float setInputTimer;

	private Vector3 startVec;
	private Vector3 nowVec;
	private float houkouAns;

	private Vector3 rightVec;
	private Vector3 leftVec;
	private Vector3 nowVecObj;
	private Vector3 targetPostion;

	private TestEnum nowEnum;

	void Start()
	{
		setInputTimer = inputTimer;
		rightVec = new Vector3(0, 90f, 0);
		leftVec = new Vector3(0, -90f, 0);
		targetPostion = Vector3.zero;
		nowEnum = TestEnum.type_A;
	}

	// Update is called once per frame
	void Update ()
	{
		if(Input.GetMouseButtonDown(0))
		{
			startVec = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		}

		bool isMouseButton = Input.GetMouseButton(0);
		if(isMouseButton)
		{
			setInputTimer -= Time.deltaTime;
			if(setInputTimer <= 0 )
			{
				return;
			}

			nowVec = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			houkouAns = startVec.x - nowVec.x;

			Vector3 setVec = Vector3.zero;

			if(houkouAns > 0)
			{
				setVec = rightVec;
			}
			else if(houkouAns < 0)
			{
				setVec = leftVec;
			}

			setInputTimer = inputTimer;
			nowVecObj = this.transform.eulerAngles;
			this.transform.Rotate(setVec * Time.deltaTime * 0.8f);
		}
		else
		{
	
			this.transform.rotation = Quaternion.Slerp(
				this.transform.rotation,
				Quaternion.Euler(targetPostion),
				3 * Time.deltaTime
				);

		}

		Debug.Log("targetPostion ---> "+targetPostion+ " : "+Quaternion.Euler(targetPostion));

		bool isMouseButtonUp = Input.GetMouseButtonUp(0);
		if(isMouseButtonUp)
		{
			TestEnum targetEnumType = CheckTargetPosition (nowVecObj, houkouAns);
			targetPostion = new Vector3(0, (float)targetEnumType, 0 );
		}
	}


	private TestEnum CheckTargetPosition(Vector3 vec, float houkou)
	{
		if(vec.y > 315)
		{
			return CheckRightOrLeft(houkou, TestEnum.type_H, TestEnum.type_I);
		}
		else if(vec.y <= 315 && vec.y > 270)
		{
			return CheckRightOrLeft(houkou, TestEnum.type_G, TestEnum.type_H);
		}
		else if(vec.y <= 270 && vec.y > 225)
		{
			return CheckRightOrLeft(houkou, TestEnum.type_F, TestEnum.type_G);
		}
		else if(vec.y <= 225 && vec.y > 180)
		{
			return CheckRightOrLeft(houkou, TestEnum.type_E, TestEnum.type_F);
		}
		else if(vec.y <= 180 && vec.y > 135)
		{
			return CheckRightOrLeft(houkou, TestEnum.type_D, TestEnum.type_E);
		}
		else if(vec.y <= 135 && vec.y > 90)
		{
			return CheckRightOrLeft(houkou, TestEnum.type_C, TestEnum.type_D);
		}
		else if(vec.y <= 90 && vec.y > 45)
		{
			return CheckRightOrLeft(houkou, TestEnum.type_B, TestEnum.type_C);
		}
		else if(vec.y <= 45 && vec.y > 0)
		{
			return CheckRightOrLeft(houkou, TestEnum.type_A, TestEnum.type_B);
		}

		return TestEnum.type_null;
	}


	private TestEnum CheckRightOrLeft(float houkou, TestEnum leftAnsType, TestEnum rightAnsType)
	{
		if(houkou < 0)
		{
			return leftAnsType;
		}
		return rightAnsType;
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 200, 60), this.transform.rotation.ToString());
		GUI.Label(new Rect(10, 60, 200, 60), this.transform.eulerAngles.ToString());
	}

}
