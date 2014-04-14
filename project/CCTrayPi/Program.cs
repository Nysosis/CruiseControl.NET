using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ThoughtWorks.CruiseControl.CCTrayLib.Configuration;
using ThoughtWorks.CruiseControl.CCTrayLib.Monitoring;
using ThoughtWorks.CruiseControl.Remote;
using ThoughtWorks.CruiseControl.CCTrayLib.Presentation;
using System.Threading;
using System.Globalization;

namespace CCTrayPi {
	class Program {
		static void Main(string[] args) {
			string settingsFilename = GetSettingsFilename(args.ToList());

			var remoteCruiseManagerFactory = new CruiseServerClientFactory();
			ICruiseServerManagerFactory cruiseServerManagerFactory = new CruiseServerManagerFactory(remoteCruiseManagerFactory);
			ICruiseProjectManagerFactory cruiseProjectManagerFactory = new CruiseProjectManagerFactory(remoteCruiseManagerFactory);
			CCTrayMultiConfiguration configuration = new CCTrayMultiConfiguration(cruiseServerManagerFactory, cruiseProjectManagerFactory, settingsFilename);
			var sync = new NoOpSynchronizeInvoke();

			var controller = new MainFormController(configuration, sync, null);

			controller.StartServerMonitoring();

			Console.ReadKey();

			controller.StopServerMonitoring();
		}

		private static string GetSettingsFilename(List<string> extra) {
			if (extra.Count == 1)
				return extra[0]; // use settings file specified on command line

			string oldFashionedSettingsFilename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "settings.xml");
			string newSettingsFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "cctray-settings.xml");

			if (File.Exists(oldFashionedSettingsFilename) && !File.Exists(newSettingsFilename))
				File.Copy(oldFashionedSettingsFilename, newSettingsFilename);

			return newSettingsFilename;
		}
	}
}
