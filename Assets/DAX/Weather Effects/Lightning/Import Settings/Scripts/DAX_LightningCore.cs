using UnityEngine;
using System.Collections;
using System.Reflection;

public class DAX_LightningCore : MonoBehaviour 
{
	public float StartTime = 0.0f; 
	public float Duration = -1.0f;
	public float FreqPerSecond = 1.0f;
	public Vector2 FreqRandom;
	public float StartSize = 40.0f;
	float curTime = 0.0f;
	float fullTime;
	
	public ParticleSystem[] PSMatrix;
	public Material[] Materials;
	
		
	Transform b_Transform;

	// Use this for initialization
	void Start () 
	{
		this.b_Transform = this.transform; //bufferize transform component
		this.SelfEnable();
	}
	
	void OnEnable() 
	{
		this.SelfEnable();
	}
	
	//reset
	void SelfEnable()
	{
		if (PSMatrix==null) {this.enabled = false;};
		if (PSMatrix.Length==0) {this.enabled = false;};
		
		if (Materials==null) {this.enabled = false;};
		if (Materials.Length==0) {this.enabled = false;};
				
		this.curTime = this.StartTime;
		this.fullTime = -this.StartTime;
	}
	
	//spawn 
	void Spawn( int index )
	{
		this.curTime -= Time.deltaTime; //time to next spawn
		this.fullTime += Time.deltaTime; //full work time
			
		//time to spawn		
		if (this.curTime<=0.0f)
		{
			this.curTime+=1/FreqPerSecond + Random.Range( FreqRandom.x, FreqRandom.y );			//next spawn time offset
			
			int rnd_Material = Random.Range(0, Materials.Length);
			if ( (PSMatrix[ index ]!=null) & (Materials[rnd_Material]!=null) )
			{
				ParticleSystem PS = ParticleSystem.Instantiate( PSMatrix[ index ] );//clone particle system
				
				PS.startSize = this.StartSize;
				PS.GetComponent<Renderer>().material = Materials[rnd_Material];	//set random material	
				PS.transform.Translate( this.b_Transform.position, Space.World );//move system to buf position in world space
				PS.gameObject.SetActive( true );//lightning
			}
		}
		
		//not disable when duration < 0.0f
		if ((this.fullTime > this.Duration)&(this.Duration > 0.0f))
		{
			this.gameObject.SetActive( false );//disable
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

		Spawn(Random.Range( 0, PSMatrix.Length));
		//Screate();
	}
}
