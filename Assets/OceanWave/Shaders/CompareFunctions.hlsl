void Compare_float(float2 comp, out float answer)
{
	answer = 1;
	if(comp.x < 0 || comp.x > 1 || comp.y < 0 || comp.y > 1){
		answer = 0;
	}
}