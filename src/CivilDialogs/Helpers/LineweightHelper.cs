using Autodesk.AutoCAD.DatabaseServices;

namespace CivilDialogs.Helpers;

public static class LineweightHelpers
{
	public static LineWeight LineweightStringtoLineweight(string lineweight)
	{
		return lineweight switch
		{
			"0.00 mm" => LineWeight.LineWeight000,
			"0.05 mm" => LineWeight.LineWeight005,
			"0.09 mm" => LineWeight.LineWeight009,
			"0.13 mm" => LineWeight.LineWeight013,
			"0.15 mm" => LineWeight.LineWeight015,
			"0.18 mm" => LineWeight.LineWeight018,
			"0.20 mm" => LineWeight.LineWeight020,
			"0.25 mm" => LineWeight.LineWeight025,
			"0.30 mm" => LineWeight.LineWeight030,
			"0.35 mm" => LineWeight.LineWeight035,
			"0.40 mm" => LineWeight.LineWeight040,
			"0.50 mm" => LineWeight.LineWeight050,
			"0.53 mm" => LineWeight.LineWeight053,
			"0.60 mm" => LineWeight.LineWeight060,
			"0.70 mm" => LineWeight.LineWeight070,
			"0.80 mm" => LineWeight.LineWeight080,
			"0.90 mm" => LineWeight.LineWeight090,
			"1.00 mm" => LineWeight.LineWeight100,
			"1.06 mm" => LineWeight.LineWeight106,
			"1.20 mm" => LineWeight.LineWeight120,
			"1.40 mm" => LineWeight.LineWeight140,
			"1.58 mm" => LineWeight.LineWeight158,
			"2.00 mm" => LineWeight.LineWeight200,
			"2.11 mm" => LineWeight.LineWeight211,
			"ByLayer" => LineWeight.ByLayer,
			"ByBlock" => LineWeight.ByBlock,
			"Default" => LineWeight.ByLineWeightDefault,
			"ByDIPs" => LineWeight.ByDIPs,
			_ => LineWeight.ByLayer
		};
	}
}