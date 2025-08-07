struct Wave{
	float2 direction;
	float frequency;
	float amplitude;
	float speed;
};

StructuredBuffer<Wave> _Waves;

void OceanWaves_float(float xPos, float zPos, int count, out float yPos, out float dx, out float dz)
{
	yPos = 0;
	dx = 0;
	dz = 0;

	for(int wi = 0; wi < count; wi++){
		float xz = xPos * _Waves[wi].direction.x + zPos * _Waves[wi].direction.y;
		float n = _Waves[wi].frequency * xz + _Waves[wi].speed * _Time.y;
		float result = _Waves[wi].amplitude * (exp(sin(n) - 1) - .135335);

		yPos += result;

		float derivative = _Waves[wi].frequency * cos(n) * result;
		dx += _Waves[wi].direction.x * derivative;
		dz += _Waves[wi].direction.y * derivative;
	}
}