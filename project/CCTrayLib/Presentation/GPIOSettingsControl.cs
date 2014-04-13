using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThoughtWorks.CruiseControl.CCTrayLib.GPIO;
using ThoughtWorks.CruiseControl.CCTrayLib.Monitoring;
using ThoughtWorks.CruiseControl.CCTrayLib.Configuration;

namespace ThoughtWorks.CruiseControl.CCTrayLib.Presentation {
	public partial class GPIOSettingsControl : UserControl {

		private GPIOServerStatus Broken = new GPIOServerStatus(ProjectState.Broken);
		private GPIOServerStatus BrokenAndBuilding = new GPIOServerStatus(ProjectState.BrokenAndBuilding);
		private GPIOServerStatus Building = new GPIOServerStatus(ProjectState.Building);
		private GPIOServerStatus NotConnected = new GPIOServerStatus(ProjectState.NotConnected);
		private GPIOServerStatus Success = new GPIOServerStatus(ProjectState.Success);

		public GPIOSettingsControl() {
			InitializeComponent();
		}

		public void BindGPIOConfiguration(GPIOConfiguration configuration) {
			Broken.SetPins(configuration.BrokenPins);
			BrokenAndBuilding.SetPins(configuration.BrokenBuildingPins);
			Building.SetPins(configuration.BuildingPins);
			NotConnected.SetPins(configuration.NotConnectedPins);
			Success.SetPins(configuration.SuccessPins);
		}

		public void PersistGPIOConfiguration(GPIOConfiguration configuration) {
			configuration.BrokenPins = Broken.GetPins();
			configuration.BrokenBuildingPins = BrokenAndBuilding.GetPins();
			configuration.BuildingPins = Building.GetPins();
			configuration.NotConnectedPins = NotConnected.GetPins();
			configuration.SuccessPins = Success.GetPins();
		}

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);

			gPIOServerStatusBindingSource.Add(Broken);
			gPIOServerStatusBindingSource.Add(BrokenAndBuilding);
			gPIOServerStatusBindingSource.Add(Building);
			gPIOServerStatusBindingSource.Add(NotConnected);
			gPIOServerStatusBindingSource.Add(Success);
		}
	}
}
