﻿using KSP.IO; // UNITY
using System;

namespace Kalculator {
	public static class Globals {
		/// <summary>Add a variaty of KSP globals to a Kalculator, such as information about the planets.</summary>
		/// <param name="kalc">The Kalculator to add the globals to.</param>
		public static void Add(Kalculator kalc) {
			// Atronomical almanac, taken from http://wiki.kerbalspaceprogram.com
			kalc.AddGlobal(new Variable("Kerbol.R", VarType.NUMBER, 262600E3));
			kalc.AddGlobal(new Variable("Kerbol.M", VarType.NUMBER, 1.756567E31));
			kalc.AddGlobal(new Variable("Kerbol.mu", VarType.NUMBER, 1.1723328E18));
			kalc.AddGlobal(new Variable("Kerbol.µ", VarType.NUMBER, 1.1723328E18));
			kalc.AddGlobal(new Variable("Kerbol.day", VarType.NUMBER, 432E3));
			kalc.AddGlobal(new Variable("Kerbol.SOI", VarType.NUMBER, double.PositiveInfinity));
			kalc.AddGlobal(new Variable("Kerbol.AtmosHeight", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Kerbol.AtmosPress", VarType.NUMBER, 0.0));

			kalc.AddGlobal(new Variable("Moho.Ap", VarType.NUMBER, 6315765980.0));
			kalc.AddGlobal(new Variable("Moho.Pe", VarType.NUMBER, 4210510628.0));
			kalc.AddGlobal(new Variable("Moho.a", VarType.NUMBER, 5263138304.0));
			kalc.AddGlobal(new Variable("Moho.e", VarType.NUMBER, 0.2));
			kalc.AddGlobal(new Variable("Moho.ArgPe", VarType.NUMBER, 15.0));
			kalc.AddGlobal(new Variable("Moho.LongAS", VarType.NUMBER, 70.0));
			kalc.AddGlobal(new Variable("Moho.Inc", VarType.NUMBER, 7.0));
			kalc.AddGlobal(new Variable("Moho.R", VarType.NUMBER, 250E3));
			kalc.AddGlobal(new Variable("Moho.M", VarType.NUMBER, 2.5263617E24));
			kalc.AddGlobal(new Variable("Moho.mu", VarType.NUMBER, 1.6860938E11));
			kalc.AddGlobal(new Variable("Moho.µ", VarType.NUMBER, 1.6860938E11));
			kalc.AddGlobal(new Variable("Moho.g", VarType.NUMBER, 2.70));
			kalc.AddGlobal(new Variable("Moho.day", VarType.NUMBER, 1210E3));
			kalc.AddGlobal(new Variable("Moho.year", VarType.NUMBER, 2215754.0));
			kalc.AddGlobal(new Variable("Moho.SOI", VarType.NUMBER, 9646663.0));
			kalc.AddGlobal(new Variable("Moho.AtmosHeight", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Moho.AtmosPress", VarType.NUMBER, 0.0));

			kalc.AddGlobal(new Variable("Eve.Ap", VarType.NUMBER, 9931011387.0));
			kalc.AddGlobal(new Variable("Eve.Pe", VarType.NUMBER, 9734357701.0));
			kalc.AddGlobal(new Variable("Eve.a", VarType.NUMBER, 9832684544.0));
			kalc.AddGlobal(new Variable("Eve.e", VarType.NUMBER, 0.01));
			kalc.AddGlobal(new Variable("Eve.ArgPe", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Eve.LongAS", VarType.NUMBER, 15.0));
			kalc.AddGlobal(new Variable("Eve.Inc", VarType.NUMBER, 2.1));
			kalc.AddGlobal(new Variable("Eve.R", VarType.NUMBER, 700E3));
			kalc.AddGlobal(new Variable("Eve.M", VarType.NUMBER, 1.2244127E26));
			kalc.AddGlobal(new Variable("Eve.mu", VarType.NUMBER, 8.1717302E12));
			kalc.AddGlobal(new Variable("Eve.µ", VarType.NUMBER, 8.1717302E12));
			kalc.AddGlobal(new Variable("Eve.g", VarType.NUMBER, 16.7));
			kalc.AddGlobal(new Variable("Eve.day", VarType.NUMBER, 80500.0));
			kalc.AddGlobal(new Variable("Eve.year", VarType.NUMBER, 5657995.0));
			kalc.AddGlobal(new Variable("Eve.SOI", VarType.NUMBER, 85109365.0));
			kalc.AddGlobal(new Variable("Eve.AtmosHeight", VarType.NUMBER, 96708.574));
			kalc.AddGlobal(new Variable("Eve.AtmosPress", VarType.NUMBER, 506625.0));

			kalc.AddGlobal(new Variable("Gilly.Ap", VarType.NUMBER, 48825000.0));
			kalc.AddGlobal(new Variable("Gilly.Pe", VarType.NUMBER, 14175000.0));
			kalc.AddGlobal(new Variable("Gilly.a", VarType.NUMBER, 31500000.0));
			kalc.AddGlobal(new Variable("Gilly.e", VarType.NUMBER, 0.55));
			kalc.AddGlobal(new Variable("Gilly.ArgPe", VarType.NUMBER, 10.0));
			kalc.AddGlobal(new Variable("Gilly.LongAS", VarType.NUMBER, 80.0));
			kalc.AddGlobal(new Variable("Gilly.Inc", VarType.NUMBER, 12.0));
			kalc.AddGlobal(new Variable("Gilly.R", VarType.NUMBER, 13E3));
			kalc.AddGlobal(new Variable("Gilly.M", VarType.NUMBER, 1.2420512E20));
			kalc.AddGlobal(new Variable("Gilly.mu", VarType.NUMBER, 8289449.8));
			kalc.AddGlobal(new Variable("Gilly.µ", VarType.NUMBER, 8289449.8));
			kalc.AddGlobal(new Variable("Gilly.g", VarType.NUMBER, 0.049));
			kalc.AddGlobal(new Variable("Gilly.day", VarType.NUMBER, 28255.0));
			kalc.AddGlobal(new Variable("Gilly.year", VarType.NUMBER, 388587.0));
			kalc.AddGlobal(new Variable("Gilly.SOI", VarType.NUMBER, 126123.27));
			kalc.AddGlobal(new Variable("Gilly.AtmosHeight", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Gilly.AtmosPress", VarType.NUMBER, 0.0));

			kalc.AddGlobal(new Variable("Kerbin.Ap", VarType.NUMBER, 13599840256.0));
			kalc.AddGlobal(new Variable("Kerbin.Pe", VarType.NUMBER, 13599840256.0));
			kalc.AddGlobal(new Variable("Kerbin.a", VarType.NUMBER, 13599840256.0));
			kalc.AddGlobal(new Variable("Kerbin.e", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Kerbin.ArgPe", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Kerbin.LongAS", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Kerbin.Inc", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Kerbin.R", VarType.NUMBER, 600E3));
			kalc.AddGlobal(new Variable("Kerbin.M", VarType.NUMBER, 5.2915793E25));
			kalc.AddGlobal(new Variable("Kerbin.mu", VarType.NUMBER, 3.5316000E12));
			kalc.AddGlobal(new Variable("Kerbin.µ", VarType.NUMBER, 3.5316000E12));
			kalc.AddGlobal(new Variable("Kerbin.g", VarType.NUMBER, 9.81));
			kalc.AddGlobal(new Variable("Kerbin.day", VarType.NUMBER, 21600.0));
			kalc.AddGlobal(new Variable("Kerbin.year", VarType.NUMBER, 9203545.0));
			kalc.AddGlobal(new Variable("Kerbin.SOI", VarType.NUMBER, 84159286.0));
			kalc.AddGlobal(new Variable("Kerbin.AtmosHeight", VarType.NUMBER, 69077.553));
			kalc.AddGlobal(new Variable("Kerbin.AtmosPress", VarType.NUMBER, 101325.0));

			kalc.AddGlobal(new Variable("Mun.Ap", VarType.NUMBER, 12000000.0));
			kalc.AddGlobal(new Variable("Mun.Pe", VarType.NUMBER, 12000000.0));
			kalc.AddGlobal(new Variable("Mun.a", VarType.NUMBER, 12000000.0));
			kalc.AddGlobal(new Variable("Mun.e", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Mun.ArgPe", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Mun.LongAS", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Mun.Inc", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Mun.R", VarType.NUMBER, 200E3));
			kalc.AddGlobal(new Variable("Mun.M", VarType.NUMBER, 9.7600236E23));
			kalc.AddGlobal(new Variable("Mun.mu", VarType.NUMBER, 6.5138398E10));
			kalc.AddGlobal(new Variable("Mun.µ", VarType.NUMBER, 6.5138398E10));
			kalc.AddGlobal(new Variable("Mun.g", VarType.NUMBER, 1.63));
			kalc.AddGlobal(new Variable("Mun.day", VarType.NUMBER, 138984.38));
			kalc.AddGlobal(new Variable("Mun.year", VarType.NUMBER, 138984.38));
			kalc.AddGlobal(new Variable("Mun.SOI", VarType.NUMBER, 2429559.1));
			kalc.AddGlobal(new Variable("Mun.AtmosHeight", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Mun.AtmosPress", VarType.NUMBER, 0.0));

			kalc.AddGlobal(new Variable("Minmus.Ap", VarType.NUMBER, 47000000.0));
			kalc.AddGlobal(new Variable("Minmus.Pe", VarType.NUMBER, 47000000.0));
			kalc.AddGlobal(new Variable("Minmus.a", VarType.NUMBER, 47000000.0));
			kalc.AddGlobal(new Variable("Minmus.e", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Minmus.ArgPe", VarType.NUMBER, 38.0));
			kalc.AddGlobal(new Variable("Minmus.LongAS", VarType.NUMBER, 78.0));
			kalc.AddGlobal(new Variable("Minmus.Inc", VarType.NUMBER, 6.0));
			kalc.AddGlobal(new Variable("Minmus.R", VarType.NUMBER, 60E3));
			kalc.AddGlobal(new Variable("Minmus.M", VarType.NUMBER, 2.6457897E22));
			kalc.AddGlobal(new Variable("Minmus.mu", VarType.NUMBER, 1.7658000E9));
			kalc.AddGlobal(new Variable("Minmus.µ", VarType.NUMBER, 1.7658000E9));
			kalc.AddGlobal(new Variable("Minmus.g", VarType.NUMBER, 0.491));
			kalc.AddGlobal(new Variable("Minmus.day", VarType.NUMBER, 40400.0));
			kalc.AddGlobal(new Variable("Minmus.year", VarType.NUMBER, 1077311.0));
			kalc.AddGlobal(new Variable("Minmus.SOI", VarType.NUMBER, 2247428.4));
			kalc.AddGlobal(new Variable("Minmus.AtmosHeight", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Minmus.AtmosPress", VarType.NUMBER, 0.0));

			kalc.AddGlobal(new Variable("Duna.Ap", VarType.NUMBER, 21783189163.0));
			kalc.AddGlobal(new Variable("Duna.Pe", VarType.NUMBER, 19669121365.0));
			kalc.AddGlobal(new Variable("Duna.a", VarType.NUMBER, 20726155264.0));
			kalc.AddGlobal(new Variable("Duna.e", VarType.NUMBER, 0.05));
			kalc.AddGlobal(new Variable("Duna.ArgPe", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Duna.LongAS", VarType.NUMBER, 135.5));
			kalc.AddGlobal(new Variable("Duna.Inc", VarType.NUMBER, 0.06));
			kalc.AddGlobal(new Variable("Duna.R", VarType.NUMBER, 320E3));
			kalc.AddGlobal(new Variable("Duna.M", VarType.NUMBER, 4.5154812E24));
			kalc.AddGlobal(new Variable("Duna.mu", VarType.NUMBER, 3.0136321E11));
			kalc.AddGlobal(new Variable("Duna.µ", VarType.NUMBER, 3.0136321E11));
			kalc.AddGlobal(new Variable("Duna.g", VarType.NUMBER, 2.94));
			kalc.AddGlobal(new Variable("Duna.day", VarType.NUMBER, 65517.859));
			kalc.AddGlobal(new Variable("Duna.year", VarType.NUMBER, 17315400.0));
			kalc.AddGlobal(new Variable("Duna.SOI", VarType.NUMBER, 47921949.0));
			kalc.AddGlobal(new Variable("Duna.AtmosHeight", VarType.NUMBER, 41446.532));
			kalc.AddGlobal(new Variable("Duna.AtmosPress", VarType.NUMBER, 20265.0));

			kalc.AddGlobal(new Variable("Ike.Ap", VarType.NUMBER, 3296000.0));
			kalc.AddGlobal(new Variable("Ike.Pe", VarType.NUMBER, 3104000.0));
			kalc.AddGlobal(new Variable("Ike.a", VarType.NUMBER, 3200000.0));
			kalc.AddGlobal(new Variable("Ike.e", VarType.NUMBER, 0.03));
			kalc.AddGlobal(new Variable("Ike.ArgPe", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Ike.LongAS", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Ike.Inc", VarType.NUMBER, 0.2));
			kalc.AddGlobal(new Variable("Ike.R", VarType.NUMBER, 130E3));
			kalc.AddGlobal(new Variable("Ike.M", VarType.NUMBER, 2.7821949E23));
			kalc.AddGlobal(new Variable("Ike.mu", VarType.NUMBER, 1.8568369E10));
			kalc.AddGlobal(new Variable("Ike.µ", VarType.NUMBER, 1.8568369E10));
			kalc.AddGlobal(new Variable("Ike.g", VarType.NUMBER, 1.10));
			kalc.AddGlobal(new Variable("Ike.day", VarType.NUMBER, 65517.862));
			kalc.AddGlobal(new Variable("Ike.year", VarType.NUMBER, 65518.0));
			kalc.AddGlobal(new Variable("Ike.SOI", VarType.NUMBER, 1049598.9));
			kalc.AddGlobal(new Variable("Ike.AtmosHeight", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Ike.AtmosPress", VarType.NUMBER, 0.0));

			kalc.AddGlobal(new Variable("Dres.Ap", VarType.NUMBER, 46761053522.0));
			kalc.AddGlobal(new Variable("Dres.Pe", VarType.NUMBER, 34917642884.0));
			kalc.AddGlobal(new Variable("Dres.a", VarType.NUMBER, 40839348203.0));
			kalc.AddGlobal(new Variable("Dres.e", VarType.NUMBER, 0.14));
			kalc.AddGlobal(new Variable("Dres.ArgPe", VarType.NUMBER, 90.0));
			kalc.AddGlobal(new Variable("Dres.LongAS", VarType.NUMBER, 280.0));
			kalc.AddGlobal(new Variable("Dres.Inc", VarType.NUMBER, 5.0));
			kalc.AddGlobal(new Variable("Dres.R", VarType.NUMBER, 138E3));
			kalc.AddGlobal(new Variable("Dres.M", VarType.NUMBER, 3.2191322E23));
			kalc.AddGlobal(new Variable("Dres.mu", VarType.NUMBER, 2.1484489E10));
			kalc.AddGlobal(new Variable("Dres.µ", VarType.NUMBER, 2.1484489E10));
			kalc.AddGlobal(new Variable("Dres.g", VarType.NUMBER, 1.13));
			kalc.AddGlobal(new Variable("Dres.day", VarType.NUMBER, 34800.0));
			kalc.AddGlobal(new Variable("Dres.year", VarType.NUMBER, 47893063.0));
			kalc.AddGlobal(new Variable("Dres.SOI", VarType.NUMBER, 32832840.0));
			kalc.AddGlobal(new Variable("Dres.AtmosHeight", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Dres.AtmosPress", VarType.NUMBER, 0.0));

			kalc.AddGlobal(new Variable("Jool.Ap", VarType.NUMBER, 72212238387.0));
			kalc.AddGlobal(new Variable("Jool.Pe", VarType.NUMBER, 65334882253.0));
			kalc.AddGlobal(new Variable("Jool.a", VarType.NUMBER, 68773560320.0));
			kalc.AddGlobal(new Variable("Jool.e", VarType.NUMBER, 0.05));
			kalc.AddGlobal(new Variable("Jool.ArgPe", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Jool.LongAS", VarType.NUMBER, 52.0));
			kalc.AddGlobal(new Variable("Jool.Inc", VarType.NUMBER, 1.304));
			kalc.AddGlobal(new Variable("Jool.R", VarType.NUMBER, 6000E3));
			kalc.AddGlobal(new Variable("Jool.M", VarType.NUMBER, 4.2332635E27));
			kalc.AddGlobal(new Variable("Jool.mu", VarType.NUMBER, 2.8252800E14));
			kalc.AddGlobal(new Variable("Jool.µ", VarType.NUMBER, 2.8252800E14));
			kalc.AddGlobal(new Variable("Jool.g", VarType.NUMBER, 7.85));
			kalc.AddGlobal(new Variable("Jool.day", VarType.NUMBER, 36000.0));
			kalc.AddGlobal(new Variable("Jool.year", VarType.NUMBER, 104661432.0));
			kalc.AddGlobal(new Variable("Jool.SOI", VarType.NUMBER, 2.4559852E9));
			kalc.AddGlobal(new Variable("Jool.AtmosHeight", VarType.NUMBER, 138155.11));
			kalc.AddGlobal(new Variable("Jool.AtmosPress", VarType.NUMBER, 1519880.0));

			kalc.AddGlobal(new Variable("Laythe.Ap", VarType.NUMBER, 27184000.0));
			kalc.AddGlobal(new Variable("Laythe.Pe", VarType.NUMBER, 27184000.0));
			kalc.AddGlobal(new Variable("Laythe.a", VarType.NUMBER, 27184000.0));
			kalc.AddGlobal(new Variable("Laythe.e", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Laythe.ArgPe", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Laythe.LongAS", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Laythe.Inc", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Laythe.R", VarType.NUMBER, 500E3));
			kalc.AddGlobal(new Variable("Laythe.M", VarType.NUMBER, 2.9397663E22));
			kalc.AddGlobal(new Variable("Laythe.mu", VarType.NUMBER, 1.9620000E12));
			kalc.AddGlobal(new Variable("Laythe.µ", VarType.NUMBER, 1.9620000E12));
			kalc.AddGlobal(new Variable("Laythe.g", VarType.NUMBER, 7.85));
			kalc.AddGlobal(new Variable("Laythe.day", VarType.NUMBER, 52980.879));
			kalc.AddGlobal(new Variable("Laythe.year", VarType.NUMBER, 52980.879));
			kalc.AddGlobal(new Variable("Laythe.SOI", VarType.NUMBER, 3723645.8));
			kalc.AddGlobal(new Variable("Laythe.AtmosHeight", VarType.NUMBER, 55262.042));
			kalc.AddGlobal(new Variable("Laythe.AtmosPress", VarType.NUMBER, 81060.0));

			kalc.AddGlobal(new Variable("Vall.Ap", VarType.NUMBER, 43152000.0));
			kalc.AddGlobal(new Variable("Vall.Pe", VarType.NUMBER, 43152000.0));
			kalc.AddGlobal(new Variable("Vall.a", VarType.NUMBER, 43152000.0));
			kalc.AddGlobal(new Variable("Vall.e", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Vall.ArgPe", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Vall.LongAS", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Vall.Inc", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Vall.R", VarType.NUMBER, 300E3));
			kalc.AddGlobal(new Variable("Vall.M", VarType.NUMBER, 3.1088028E24));
			kalc.AddGlobal(new Variable("Vall.mu", VarType.NUMBER, 2.0748150E11));
			kalc.AddGlobal(new Variable("Vall.µ", VarType.NUMBER, 2.0748150E11));
			kalc.AddGlobal(new Variable("Vall.g", VarType.NUMBER, 2.31));
			kalc.AddGlobal(new Variable("Vall.day", VarType.NUMBER, 105962.09));
			kalc.AddGlobal(new Variable("Vall.year", VarType.NUMBER, 105962.09));
			kalc.AddGlobal(new Variable("Vall.SOI", VarType.NUMBER, 2406401.4));
			kalc.AddGlobal(new Variable("Vall.AtmosHeight", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Vall.AtmosPress", VarType.NUMBER, 0.0));

			kalc.AddGlobal(new Variable("Tylo.Ap", VarType.NUMBER, 68500000.0));
			kalc.AddGlobal(new Variable("Tylo.Pe", VarType.NUMBER, 68500000.0));
			kalc.AddGlobal(new Variable("Tylo.a", VarType.NUMBER, 68500000.0));
			kalc.AddGlobal(new Variable("Tylo.e", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Tylo.ArgPe", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Tylo.LongAS", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Tylo.Inc", VarType.NUMBER, 0.025));
			kalc.AddGlobal(new Variable("Tylo.R", VarType.NUMBER, 600E3));
			kalc.AddGlobal(new Variable("Tylo.M", VarType.NUMBER, 4.2332635E25));
			kalc.AddGlobal(new Variable("Tylo.mu", VarType.NUMBER, 2.8252800E12));
			kalc.AddGlobal(new Variable("Tylo.µ", VarType.NUMBER, 2.8252800E12));
			kalc.AddGlobal(new Variable("Tylo.g", VarType.NUMBER, 7.85));
			kalc.AddGlobal(new Variable("Tylo.day", VarType.NUMBER, 211926.36));
			kalc.AddGlobal(new Variable("Tylo.year", VarType.NUMBER, 211926.36));
			kalc.AddGlobal(new Variable("Tylo.SOI", VarType.NUMBER, 10856518.0));
			kalc.AddGlobal(new Variable("Tylo.AtmosHeight", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Tylo.AtmosPress", VarType.NUMBER, 0.0));

			kalc.AddGlobal(new Variable("Bop.Ap", VarType.NUMBER, 158697500.0));
			kalc.AddGlobal(new Variable("Bop.Pe", VarType.NUMBER, 98302500.0));
			kalc.AddGlobal(new Variable("Bop.a", VarType.NUMBER, 128500000.0));
			kalc.AddGlobal(new Variable("Bop.e", VarType.NUMBER, 0.24));
			kalc.AddGlobal(new Variable("Bop.ArgPe", VarType.NUMBER, 25.0));
			kalc.AddGlobal(new Variable("Bop.LongAS", VarType.NUMBER, 10.0));
			kalc.AddGlobal(new Variable("Bop.Inc", VarType.NUMBER, 15.0));
			kalc.AddGlobal(new Variable("Bop.R", VarType.NUMBER, 65E3));
			kalc.AddGlobal(new Variable("Bop.M", VarType.NUMBER, 3.7261536E22));
			kalc.AddGlobal(new Variable("Bop.mu", VarType.NUMBER, 2.4868349E9));
			kalc.AddGlobal(new Variable("Bop.µ", VarType.NUMBER, 2.4868349E9));
			kalc.AddGlobal(new Variable("Bop.g", VarType.NUMBER, 0.589));
			kalc.AddGlobal(new Variable("Bop.day", VarType.NUMBER, 544507.40));
			kalc.AddGlobal(new Variable("Bop.year", VarType.NUMBER, 544507.40));
			kalc.AddGlobal(new Variable("Bop.SOI", VarType.NUMBER, 1221060.9));
			kalc.AddGlobal(new Variable("Bop.AtmosHeight", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Bop.AtmosPress", VarType.NUMBER, 0.0));

			kalc.AddGlobal(new Variable("Pol.Ap", VarType.NUMBER, 210624206.0));
			kalc.AddGlobal(new Variable("Pol.Pe", VarType.NUMBER, 149155794.0));
			kalc.AddGlobal(new Variable("Pol.a", VarType.NUMBER, 179890000.0));
			kalc.AddGlobal(new Variable("Pol.e", VarType.NUMBER, 0.17));
			kalc.AddGlobal(new Variable("Pol.ArgPe", VarType.NUMBER, 15.0));
			kalc.AddGlobal(new Variable("Pol.LongAS", VarType.NUMBER, 2.0));
			kalc.AddGlobal(new Variable("Pol.Inc", VarType.NUMBER, 4.25));
			kalc.AddGlobal(new Variable("Pol.R", VarType.NUMBER, 44E3));
			kalc.AddGlobal(new Variable("Pol.M", VarType.NUMBER, 1.0813636E22));
			kalc.AddGlobal(new Variable("Pol.mu", VarType.NUMBER, 7.2170208E8));
			kalc.AddGlobal(new Variable("Pol.µ", VarType.NUMBER, 7.2170208E8));
			kalc.AddGlobal(new Variable("Pol.g", VarType.NUMBER, 0.373));
			kalc.AddGlobal(new Variable("Pol.day", VarType.NUMBER, 901902.62));
			kalc.AddGlobal(new Variable("Pol.year", VarType.NUMBER, 901902.62));
			kalc.AddGlobal(new Variable("Pol.SOI", VarType.NUMBER, 1042138.9));
			kalc.AddGlobal(new Variable("Pol.AtmosHeight", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Pol.AtmosPress", VarType.NUMBER, 0.0));

			kalc.AddGlobal(new Variable("Eeloo.Ap", VarType.NUMBER, 113549713200.0));
			kalc.AddGlobal(new Variable("Eeloo.Pe", VarType.NUMBER, 66687926800.0));
			kalc.AddGlobal(new Variable("Eeloo.a", VarType.NUMBER, 90118820000.0));
			kalc.AddGlobal(new Variable("Eeloo.e", VarType.NUMBER, 0.26));
			kalc.AddGlobal(new Variable("Eeloo.ArgPe", VarType.NUMBER, 260.0));
			kalc.AddGlobal(new Variable("Eeloo.LongAS", VarType.NUMBER, 50.0));
			kalc.AddGlobal(new Variable("Eeloo.Inc", VarType.NUMBER, 6.15));
			kalc.AddGlobal(new Variable("Eeloo.R", VarType.NUMBER, 210E3));
			kalc.AddGlobal(new Variable("Eeloo.M", VarType.NUMBER, 1.1149358E24));
			kalc.AddGlobal(new Variable("Eeloo.mu", VarType.NUMBER, 7.4410815E10));
			kalc.AddGlobal(new Variable("Eeloo.µ", VarType.NUMBER, 7.4410815E10));
			kalc.AddGlobal(new Variable("Eeloo.g", VarType.NUMBER, 1.69));
			kalc.AddGlobal(new Variable("Eeloo.day", VarType.NUMBER, 19460.0));
			kalc.AddGlobal(new Variable("Eeloo.year", VarType.NUMBER, 156992048.0));
			kalc.AddGlobal(new Variable("Eeloo.SOI", VarType.NUMBER, 1.1908294E8));
			kalc.AddGlobal(new Variable("Eeloo.AtmosHeight", VarType.NUMBER, 0.0));
			kalc.AddGlobal(new Variable("Eeloo.AtmosPress", VarType.NUMBER, 0.0));
		}

		// UNITY
		public static void AddOrbit(Kalculator kalc, Orbit orbit, string prefix) {
			kalc.AddGlobal(new Variable(prefix +".Ap", VarType.NUMBER, (double)orbit.ApA));
			kalc.AddGlobal(new Variable(prefix +".Pe", VarType.NUMBER, (double)orbit.PeA));
			kalc.AddGlobal(new Variable(prefix +".Inc", VarType.NUMBER, (double)orbit.inclination));
			kalc.AddGlobal(new Variable(prefix +".Alt", VarType.NUMBER, (double)orbit.altitude));
			kalc.AddGlobal(new Variable(prefix +".ArgPe", VarType.NUMBER, (double)orbit.argumentOfPeriapsis));
			kalc.AddGlobal(new Variable(prefix +".ω", VarType.NUMBER, (double)orbit.argumentOfPeriapsis));
			kalc.AddGlobal(new Variable(prefix +".TimeToAp", VarType.NUMBER, (double)orbit.timeToAp));
			kalc.AddGlobal(new Variable(prefix +".TimeToPe", VarType.NUMBER, (double)orbit.timeToPe));
			kalc.AddGlobal(new Variable(prefix +".v", VarType.NUMBER, (double)orbit.vel.magnitude));
			kalc.AddGlobal(new Variable(prefix +".TrueAnomaly", VarType.NUMBER, (double)orbit.trueAnomaly));
			kalc.AddGlobal(new Variable(prefix +".θ", VarType.NUMBER, (double)orbit.trueAnomaly));
			kalc.AddGlobal(new Variable(prefix +".TimeToSOI", VarType.NUMBER, (double)orbit.UTsoi-Planetarium.GetUniversalTime()));
			kalc.AddGlobal(new Variable(prefix +".TimeToAppr", VarType.NUMBER, (double)orbit.UTappr-Planetarium.GetUniversalTime()));
		}

		public static void AddCelestialBody(Kalculator kalc, CelestialBody body) {
			AddCelestialBody(kalc, body, body.name);
		}

		public static void AddCelestialBody(Kalculator kalc, CelestialBody body, string prefix) {
			AddOrbit(kalc, body.orbit, prefix);
			kalc.AddGlobal(new Variable(prefix +".R", VarType.NUMBER, (double)body.Radius));
			kalc.AddGlobal(new Variable(prefix +".M", VarType.NUMBER, (double)body.Mass));
			kalc.AddGlobal(new Variable(prefix +".mu", VarType.NUMBER, (double)body.gravParameter));
			kalc.AddGlobal(new Variable(prefix +".µ", VarType.NUMBER, (double)body.gravParameter));
			kalc.AddGlobal(new Variable(prefix +".day", VarType.NUMBER, (double)body.rotationPeriod));
			kalc.AddGlobal(new Variable(prefix +".SOI", VarType.NUMBER, (double)body.sphereOfInfluence));
			kalc.AddGlobal(new Variable(prefix +".AtmosHeight", VarType.NUMBER, (double)body.maxAtmosphereAltitude));
			//kalc.AddGlobal(new Variable(prefix +".AtmosPress", VarType.NUMBER, 0.0));
		}
		// /UNITY
	}
}
