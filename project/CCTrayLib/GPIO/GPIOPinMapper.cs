using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RaspberryPiDotNet;

namespace ThoughtWorks.CruiseControl.CCTrayLib.GPIO {
	public class GPIOPinMapper : IDisposable {

		private IDictionary<int, GPIOPinState> _Map = new Dictionary<int, GPIOPinState>();

		public GPIOPinMapper() {
			var map = new Dictionary<int, GPIOPins>();
			map.Add(03, GPIOPins.Pin_P1_03);
			map.Add(05, GPIOPins.Pin_P1_05);
			map.Add(07, GPIOPins.Pin_P1_07);
			map.Add(11, GPIOPins.Pin_P1_11);
			map.Add(13, GPIOPins.Pin_P1_13);
			map.Add(15, GPIOPins.Pin_P1_15);
			map.Add(19, GPIOPins.Pin_P1_19);
			map.Add(21, GPIOPins.Pin_P1_21);
			map.Add(23, GPIOPins.Pin_P1_23);
			map.Add(08, GPIOPins.Pin_P1_08);
			map.Add(10, GPIOPins.Pin_P1_10);
			map.Add(12, GPIOPins.Pin_P1_12);
			map.Add(16, GPIOPins.Pin_P1_16);
			map.Add(18, GPIOPins.Pin_P1_18);
			map.Add(22, GPIOPins.Pin_P1_22);
			map.Add(24, GPIOPins.Pin_P1_24);
			map.Add(26, GPIOPins.Pin_P1_26);

			_Map = map.ToDictionary(kvp => kvp.Key, kvp => new GPIOPinState(new GPIOMem(kvp.Value)));
		}

		public void EnablePins(int[] pins) {

			foreach (var pair in _Map) {
				var expectedState = pins.Contains(pair.Key);
				pair.Value.Write(expectedState);
			}

			foreach (var pin in pins) {
				EnablePin(pin);
			}
		}

		public void EnablePin(int pin) {
			WritePin(pin, true);
		}

		public void DisablePins(int[] pins) {
			foreach (var pin in pins) {
				DisablePin(pin);
			}
		}

		public void DisablePin(int pin) {
			WritePin(pin, false);
		}

		private void WritePin(int pin, bool state) {
			if (!_Map.ContainsKey(pin))
				return;

			_Map[pin].Write(state);
		}

		public void Dispose() {
			foreach (var pin in _Map.Values) {
				pin.Dispose();
			}
		}

		private class GPIOPinState : IDisposable {

			public GPIOPinState(RaspberryPiDotNet.GPIO gpio) {
				this._GPIO = gpio;
			}

			private readonly RaspberryPiDotNet.GPIO _GPIO;
			private bool _State;

			public void Dispose() {
				this._GPIO.Dispose();
			}

			public void Write(bool state) {
				if (state == this._State)
					return;

				this._State = state;
				this._GPIO.Write(state);
			}
		}
	}
}
