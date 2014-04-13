using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThoughtWorks.CruiseControl.CCTrayLib.Configuration {
	public class GPIOConfiguration {
		public int[] BrokenPins;
		public int[] BrokenBuildingPins;
		public int[] BuildingPins;
		public int[] NotConnectedPins;
		public int[] SuccessPins;
	}
}
