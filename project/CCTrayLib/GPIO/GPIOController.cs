using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThoughtWorks.CruiseControl.CCTrayLib.Monitoring;
using ThoughtWorks.CruiseControl.CCTrayLib.Configuration;

namespace ThoughtWorks.CruiseControl.CCTrayLib.GPIO {
	public class GPIOController : IDisposable {

		private readonly GPIOConfiguration _Configuration;
		private readonly IProjectMonitor _Monitor;
		private readonly GPIOPinMapper _Mapper;

		private readonly IDictionary<ProjectState, Func<GPIOConfiguration, int[]>> _Map = new Dictionary<ProjectState, Func<GPIOConfiguration, int[]>>();

		public GPIOController(IProjectMonitor monitor, GPIOConfiguration configuration) {
			this._Monitor = monitor;
			this._Configuration = configuration;
			this._Mapper = new GPIOPinMapper();
			this._Mapper.DisableAllPins();

			this._Monitor.Polled += new MonitorPolledEventHandler(_Monitor_Polled);

			_Map.Add(ProjectState.Broken, (c) => c.BrokenPins);
			_Map.Add(ProjectState.BrokenAndBuilding, (c) => c.BrokenBuildingPins);
			_Map.Add(ProjectState.Building, (c) => c.BuildingPins);
			_Map.Add(ProjectState.NotConnected, (c) => c.NotConnectedPins);
			_Map.Add(ProjectState.Success, (c) => c.SuccessPins);

			_Mapper.EnablePins(_Map[ProjectState.NotConnected](_Configuration));
		}

		void _Monitor_Polled(object sender, MonitorPolledEventArgs args) {
			var monitor = sender as IProjectMonitor;
			if (null == monitor)
				return;

			var pins = _Map[monitor.ProjectState](_Configuration);
			_Mapper.EnablePins(pins);
		}

		public void Dispose() {
			this._Monitor.Polled -= _Monitor_Polled;
			this._Map.Clear();
			this._Mapper.Dispose();
		}
	}
}
