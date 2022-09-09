using System;

[Serializable]
public struct BeatPattern
{
	// Controls the pattern the beats happen to
	public string Letter;
	// Controls number of beats per letter
	public int NumBeats;
}
