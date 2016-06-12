var EffectName = ["BlueBall","DeadBall","DeadExplode","ElementalArrow","ElementalArrow2","ElementalBal","ElementalBall2","ElementalBall3","ElementalBall4","Explode","Explode2","Explode3","Explode4","Explode5","Explode6","Explode7","Explode8","Explode9","Explode10","Explode11","FireBall","FlameEmission","Hole","IceBall","IceCloud","Kunai","Kunai2","Kunai3","Kunai4","Kunai5","LaserFire","LaserFire2","LightningArrow","LightningBall","LightningBall2","LightningRotateBall","MagicCircleExplode","MagicCircleRelease","MagicCube","PosionExplode","Portal","Portal2","RainBowExplode","RainBowExplode2","RuneOfMagicCircle","StarCore","StormCloud","Strom","SummonMagicCircle","SummonMagicCircle2","SummonMagicCircle3","Swamp"];
var EffectName2 = ["Explosion3"];
var Effect = new Transform[53];
var Text1 : GUIText;
var i : int = 0;
var a : int = 0;

function Start(){var obj = Instantiate(Effect[i], Vector3(0,0,0),Quaternion.identity);}

function Update () {

	Text1.text = i+1 + ":" +EffectName[i];
	
	if(Input.GetKeyDown(KeyCode.Z))
	{
		if(i<=0)
			i= 47;

		else
			i--;
		a = 0;
		
		for(a = 0 ; a < EffectName2.Length; a++)
		{
			if(EffectName[i] == EffectName2[a])
			{
				var obz = Instantiate(Effect[i], Vector3(0,-4.9,0),Quaternion.identity);
				break;
			}
		}
		if(a++ == EffectName2.Length)
		var obz2 = Instantiate(Effect[i], Vector3(0,5,0),Quaternion.identity);
	}
	
	if(Input.GetKeyDown(KeyCode.X))
	{
		if(i< 47)
			i++;

		else
			i=0;
		
		for(a = 0 ; a < EffectName2.Length; a++)
		{
			if(EffectName[i] == EffectName2[a])
			{
				var obx = Instantiate(Effect[i], Vector3(0,-4.9,0),Quaternion.identity);
				break;
			}
		}
		if(a++ == EffectName2.Length)
		var obx2 = Instantiate(Effect[i], Vector3(0,5,0),Quaternion.identity);
	}
	
	if(Input.GetKeyDown(KeyCode.C))
	{
				for(a = 0 ; a < EffectName2.Length; a++)
		{
			if(EffectName[i] == EffectName2[a])
			{
				var obc = Instantiate(Effect[i], Vector3(0,-4.9,0),Quaternion.identity);
				break;
			}
		}
		if(a++ == EffectName2.Length)
		var obc2 = Instantiate(Effect[i], Vector3(0,5,0),Quaternion.identity);
	}
}