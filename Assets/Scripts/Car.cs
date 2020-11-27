using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
	public float maxSteerAngle;
	public float motorTorque;
	public float breakForce;

	public Renderer brakeLights;
	private Material normalMaterial;
	public Material brakeMaterial;

	private Rigidbody rb;
	public Transform centreOfMass;

	public static bool isBussy = false;
	private Renderer taxiDome;
	private Material greenDomeMat;
	public Material redDomeMat;

	private WheelCollider[] colliders;
	private GameObject[] wheels;

	[HideInInspector]
	public int inputValue;

	private static int NoOfGears = 5;
	private float m_GearFactor;
	private int m_GearNum;
	private float m_RevRangeBoundary = 1f;
	public float Revs { get; private set; }
	public float CurrentSpeed { get { return rb.velocity.magnitude * 2.23693629f; } }
	public float MaxSpeed { get { return motorTorque; } }

	public Text speed;


	private void Awake()
	{
		rb = GetComponentInChildren<Rigidbody>();
		colliders = GetComponentsInChildren<WheelCollider>();
		wheels = GameObject.FindGameObjectsWithTag("wheel");
	}


	private void Start()
	{
		taxiDome = GameObject.FindGameObjectWithTag("taxiDome").GetComponent<Renderer>();
		rb.centerOfMass = centreOfMass.localPosition;
		normalMaterial = brakeLights.material;
		greenDomeMat = taxiDome.material;
	}


	private void FixedUpdate()
	{
		/*for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject.layer == LayerMask.NameToLayer("Power"))
			{
				colliders[i].motorTorque = inputValue * motorTorque;
			}
			else
			{
				if (colliders[i].gameObject.layer == LayerMask.NameToLayer("Steer"))
				{
					colliders[i].steerAngle = UISteeringWheel.outPut * maxSteerAngle;
				}
			}
		}*/

		CalculateRevs();
		GearChanging();
		speed.text = (rb.velocity.magnitude * 7.5f).ToString("0");
	}


	private void Update()
	{
		for (int i = 0; i < colliders.Length; i++)
		{
			colliders[i].GetWorldPose(out Vector3 pos, out Quaternion rot);
			wheels[i].transform.position = pos;
			wheels[i].transform.rotation = rot;
		}
	}


	public void Brake()
	{
		colliders[0].brakeTorque = breakForce;
		colliders[1].brakeTorque = breakForce;
	}

	public void BrakeTexture()
    {
		brakeLights.material = brakeMaterial;
    }


	public void DontBrake()
	{
		colliders[0].brakeTorque = 0f;
		colliders[1].brakeTorque = 0f;
	}

	public void NormalTexture()
	{
		brakeLights.material = normalMaterial;
	}


	public void Accelerate()
	{
		if (Shifter.isInDrive == true)
		{
			inputValue = 1;
		}
		else
		{
			inputValue = -1;
		}
		colliders[0].motorTorque = inputValue * motorTorque;
		colliders[1].motorTorque = inputValue * motorTorque;
	}


	public void DontAccelerate()
	{
		inputValue = 0;
		colliders[0].motorTorque = inputValue * motorTorque;
		colliders[1].motorTorque = inputValue * motorTorque;
	}


	public void Steer()
	{
		colliders[2].steerAngle = UISteeringWheel.outPut * maxSteerAngle;
		colliders[3].steerAngle = UISteeringWheel.outPut * maxSteerAngle;
	}

	public void TaxiDome()
    {
		if(taxiDome.material == greenDomeMat)
        {
			taxiDome.material = redDomeMat;
        }
		else
        {
			taxiDome.material = greenDomeMat;
        }
    }


	// unclamped version of Lerp, to allow value to exceed the from-to range
	private static float ULerp(float from, float to, float value)
	{
		return (1.0f - value) * from + value * to;
	}


	// simple function to add a curved bias towards 1 for a value in the 0-1 range
	private static float CurveFactor(float factor)
	{
		return 1 - (1 - factor) * (1 - factor);
	}


	private void GearChanging()
	{
		float f = Mathf.Abs(CurrentSpeed / MaxSpeed);
		float upgearlimit = (1 / (float)NoOfGears) * (m_GearNum + 1);
		float downgearlimit = (1 / (float)NoOfGears) * m_GearNum;

		if (m_GearNum > 0 && f < downgearlimit)
		{
			m_GearNum--;
		}

		if (f > upgearlimit && (m_GearNum < (NoOfGears - 1)))
		{
			m_GearNum++;
		}
	}


	private void CalculateGearFactor()
	{
		float f = (1 / (float)NoOfGears);
		// gear factor is a normalised representation of the current speed within the current gear's range of speeds.
		// We smooth towards the 'target' gear factor, so that revs don't instantly snap up or down when changing gear.
		var targetGearFactor = Mathf.InverseLerp(f * m_GearNum, f * (m_GearNum + 1), Mathf.Abs(CurrentSpeed / MaxSpeed));
		m_GearFactor = Mathf.Lerp(m_GearFactor, targetGearFactor, Time.deltaTime * 5f);
	}


	private void CalculateRevs()
	{
		// calculate engine revs (for display / sound)
		// (this is done in retrospect - revs are not used in force/power calculations)
		CalculateGearFactor();
		var gearNumFactor = m_GearNum / (float)NoOfGears;
		var revsRangeMin = ULerp(0f, m_RevRangeBoundary, CurveFactor(gearNumFactor));
		var revsRangeMax = ULerp(m_RevRangeBoundary, 1f, gearNumFactor);
		Revs = ULerp(revsRangeMin, revsRangeMax, m_GearFactor);
	}
}

