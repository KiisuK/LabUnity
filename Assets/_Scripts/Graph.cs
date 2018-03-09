using System;
using UnityEngine;

public class Graph : MonoBehaviour {

	public Transform pointPrefab;
	Transform[] points;

	[Range(10,100)]
	public int resolution = 10;
	public GraphFuncionName function;
	static GraphFunction[] functions = {
		SineFunction, Sine2DFunction, MultiSineFunction, MultiSine2DFunction, Ripple
	};
	const float pi = Mathf.PI;

	void Awake()
	{
		points = new Transform[resolution * resolution];

		float step = 2f / resolution;

		Vector3 scale = Vector3.one * step;
		Vector3 pos;
		pos.y = 0f;
		pos.z = 0f;

		for (int i = 0, z = 0; z < resolution; z++) 
		{
			for(int x = 0; x < resolution; x++, i++)
			{
				Transform point = Instantiate (pointPrefab);

				pos.x =  (x + 0.5f) * step - 1f;
				pos.z =  (z + 0.5f) * step - 1f;
			
				point.localPosition = pos;
				point.localScale = scale;
				point.SetParent (transform, false);

				points [i] = point;
			}
		}			
	}

	void Update()
	{
		float t = Time.time;
		GraphFunction f = functions [(int)function];

		for (int i = 0; i < points.Length; i++) 
		{
			Transform point = points [i];
			Vector3 pos = point.localPosition;

			pos.y = f (pos.x, pos.z, t);
			 
			point.localPosition = pos;
		}

		float step = 2f / resolution;

	}

	static Vector3 SineFunction (float x, float z, float t)
	{
		Vector3 p;
		p.x = x;
		p.y = Mathf.Sin (pi * (x + t));
		p.z = z;
		return p;
	}

	static Vector3 Sine2DFunction(float x, float z, float t)
	{
		Vector3 p;
		p.x = x;
		p.y = Mathf.Sin (pi * (x + t));
		p.y += Mathf.Sin (pi * (z + t));
		p.y *= 0.5f;
		p.z = z;
		return p;
	}

	static Vector3 MultiSineFunction (float x, float z, float t)
	{
		Vector3 p;
		p.x = x;
		p.y = Mathf.Sin (pi * (x + t));
		p.y += Mathf.Sin (2f *pi * (x + 2f * t)) * 0.5f;
		p.y *= 2f / 3f;
		p.z = z;
		return p;
	}
		
	static Vector3 MultiSine2DFunction(float x, float z, float t)
	{
		Vector3 p;
		p.x = x;
		p.y = 4f * Mathf.Sin(pi * (x + z + t * 0.5f));
		p.y += Mathf.Sin (pi * (x + t));
		p.y += Mathf.Sin (2f * pi * (z + 2f * t)) * 0.5f;
		p.y *= 1f / 5.5f;
		p.z = z;
		return p;

	}

	static Vector3 Ripple(float x, float z, float t)
	{
		Vector3 p;
	
		float d = Mathf.Sqrt (x * x + z * z);
		p.x = x;
		p.y = Mathf.Sin (pi * (4f * d - t));
		p.y /= 1f + 10f *d;
		p.z = z;
		return p;
	}
}
