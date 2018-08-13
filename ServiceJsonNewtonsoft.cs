/* ------------------------------------------------------------------------- *
thZero.NetCore.Library.Services.Json.Newtonsoft
Copyright (C) 2016-2018 thZero.com

<development [at] thzero [dot] com>

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

	http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
 * ------------------------------------------------------------------------- */

using System;
using System.IO;

using Newtonsoft.Json;

namespace thZero.Services
{
	public sealed class ServiceJsonNewtonsoft : IServiceJson
	{
		#region Public Methods
		public T Deserialize<T>(string data)
		{
			Enforce.AgainstNullOrEmpty(() => data);

			return JsonConvert.DeserializeObject<T>(data);
		}

		public object Deserialize(string data, Type type)
		{
			Enforce.AgainstNullOrEmpty(() => data);
			Enforce.AgainstNull(() => type);

			return JsonConvert.DeserializeObject(data, type);
		}

		public T Deserialize<T>(Stream stream)
		{
			throw new NotImplementedException();
		}

		public object Deserialize(Stream stream, Type type)
		{
			throw new NotImplementedException();
		}

		public object DeserializeObject(string data)
		{
			return JsonConvert.DeserializeObject(data);
		}

		public string Serialize(object data)
		{
			return JsonConvert.SerializeObject(data);
		}

		public void Serialize(object data, Stream stream)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Public Properties
		public IServiceJsonSettings Settings
		{
			get
			{
				if (_settingsJson != null)
					return _settingsJson;

				lock (_lock)
				{
					if (_settingsJson != null)
						return _settingsJson;

					_settingsJson = new ServiceJsonSettings
					{
						ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
						MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore,
						NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
						DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.IgnoreAndPopulate
					};
					return _settingsJson;
				}
			}
		}
		#endregion

		#region Fields
		private static readonly object _lock = new object();
		private static volatile IServiceJsonSettings _settingsJson;
		#endregion
	}
}