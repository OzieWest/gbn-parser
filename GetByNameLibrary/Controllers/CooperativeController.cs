using GetByNameLibrary.Cooperatives;
using GetByNameLibrary.Utilities;
using SerializeLibra;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleParser.Cooperative
{
	public class CooperativeController
	{
		TxtLogger _logger;

		public CooperativeController()
		{
			_logger = new TxtLogger(@"logs\cooperativeController.logs");
		}

		protected CooperativeParser LoadConfig()
		{
			return new JsonSerializer().Load<CooperativeParser>(@"configs\CooperativeParser.config");
		}

		public RetValue<Boolean> CompileCoops()
		{
			var result = new RetValue<Boolean>();
			try
			{
				var parserStatus = this.LoadConfig().StartParser();

				result.Value = true;
				result.Description = String.Format("{0} | {1}", parserStatus.Value, parserStatus.Description);
			}
			catch (Exception ex)
			{
				result.Value = false;
				result.Description = ex.Message;
				_logger.AddEntry(ex.ToString(), MessageType.Error);
				_logger.WriteLogs();
			}

			return result;
		}
	}
}
