<Query Kind="Program" />

void Main()
{
	var file = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "1.input.txt");
	var measurements = File.ReadAllLines(file)
		.Select(t => int.Parse(t))
		.ToList();

	Part1(measurements);
	Part2(measurements);
}

void Part1(List<int> measurements)
{
	var output = new List<(int, bool?)>();

	int? prev = null;
	foreach (var depth in measurements)
	{
		var deeper = prev == null ? (bool?)null
						: prev < depth ? true
						: false;
		prev = depth;
		output.Add((depth, deeper));
	}

	var msg = (bool? i) => i == null ? "N/A - no previous measurement"
							: i == true ? "increased"
							: "decreased";

	output.Count(x => x.Item2 == true).Dump("Increased measurement count");
	output.Select(x => $"{x.Item1} ({msg(x.Item2)})").Dump(collapseTo: 0);
}

void Part2(List<int> measurements)
{
	var output = new List<(int, bool?)>();
	
	int? prev = null;
	for (var i = 0; i < measurements.Count - 2; i++)
	{
		var depthSample = Enumerable.Range(i, 3).Sum(j => measurements[j]);

		var deeper = prev == null ? (bool?)null
						: prev < depthSample ? true
						: false;

		prev = depthSample;
		output.Add((depthSample, deeper));
	}

	var msg = (bool? i) => i == null ? "N/A - no previous sum"
						: i == true ? "increased"
						: "decreased";

	output.Count(x => x.Item2 == true).Dump("Increased sum count");
	output.Select(x => $"{x.Item1} ({msg(x.Item2)})").Dump(collapseTo: 0);
}