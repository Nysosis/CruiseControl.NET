using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThoughtWorks.CruiseControl.CCTrayLib.Monitoring;

namespace ThoughtWorks.CruiseControl.CCTrayLib.GPIO {
	internal class GPIOServerStatus {

		public GPIOServerStatus(ProjectState state) {
			this.State = state;
		}

		public ProjectState State { get; private set; }

		public bool Pin03 { get; set; }
		public bool Pin05 { get; set; }
		public bool Pin07 { get; set; }
		public bool Pin11 { get; set; }
		public bool Pin13 { get; set; }
		public bool Pin15 { get; set; }
		public bool Pin19 { get; set; }
		public bool Pin21 { get; set; }
		public bool Pin23 { get; set; }
		public bool Pin08 { get; set; }
		public bool Pin10 { get; set; }
		public bool Pin12 { get; set; }
		public bool Pin16 { get; set; }
		public bool Pin18 { get; set; }
		public bool Pin22 { get; set; }
		public bool Pin24 { get; set; }
		public bool Pin26 { get; set; }

		internal void SetPins(int[] pins) {
			this.Pin03 = pins.Contains(03);
			this.Pin05 = pins.Contains(05);
			this.Pin07 = pins.Contains(07);
			this.Pin11 = pins.Contains(11);
			this.Pin13 = pins.Contains(13);
			this.Pin15 = pins.Contains(15);
			this.Pin19 = pins.Contains(19);
			this.Pin21 = pins.Contains(21);
			this.Pin23 = pins.Contains(23);
			this.Pin08 = pins.Contains(08);
			this.Pin10 = pins.Contains(10);
			this.Pin12 = pins.Contains(12);
			this.Pin16 = pins.Contains(16);
			this.Pin18 = pins.Contains(18);
			this.Pin22 = pins.Contains(22);
			this.Pin24 = pins.Contains(24);
			this.Pin26 = pins.Contains(26);
		}

		internal int[] GetPins() {
			var ints = new List<int>();

			if (Pin03)
				ints.Add(03);
			if (Pin05)
				ints.Add(05);
			if (Pin07)
				ints.Add(07);
			if (Pin11)
				ints.Add(11);
			if (Pin13)
				ints.Add(13);
			if (Pin15)
				ints.Add(15);
			if (Pin19)
				ints.Add(19);
			if (Pin21)
				ints.Add(21);
			if (Pin23)
				ints.Add(23);
			if (Pin08)
				ints.Add(08);
			if (Pin10)
				ints.Add(10);
			if (Pin12)
				ints.Add(12);
			if (Pin16)
				ints.Add(16);
			if (Pin18)
				ints.Add(18);
			if (Pin22)
				ints.Add(22);
			if (Pin24)
				ints.Add(24);
			if (Pin26)
				ints.Add(26);

			return ints.ToArray();
		}
	}
}
