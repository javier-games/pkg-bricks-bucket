using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace BricksBucket.Web
{
	[CreateAssetMenu (fileName = "WebAPI", menuName = "WebAPI", order = 0)]
	// ReSharper disable once InconsistentNaming
	public class WebAPI : ScriptableObject
	{
		
		
		
		#region Fields
		
		/// <summary>
		/// URL of the host for the production environment.
		/// </summary>
		[SerializeField]
		[LabelText("Production")]
		[Tooltip("Host URL for the production environment.")]
		private string _productionHost = "http://";
		
		/// <summary>
		/// URL of the host for the development environment.
		/// </summary>
		[SerializeField]
		[LabelText("Development")]
		[Tooltip("Host URL for the development environment.")]
		private string _developmentHost = "http://";

		/// <summary>
		/// URL to override the host URL of the development and production
		/// environments.
		/// </summary>
		[SerializeField]
		[LabelText("Override")]
		[Tooltip("URL of the host that overrides the environments.")]
		private string _overrideHost = "http://";
		
		/// <summary>
		/// Defines which URL to use as host.
		/// </summary>
		[SerializeField]
		[Tooltip("Defines which host URL to use.")]
		private EnvironmentType _activeHost =
			EnvironmentType.DEVELOPMENT;

		/// <summary>
		/// Collection of services.
		/// </summary>
		[SerializeField]
		[ListDrawerSettings(HideAddButton = true)]
		private List<Service> _services;

		[SerializeField]
		private Service _service;

		#endregion



		#region Properties

		/// <summary>
		/// Controlled by the <see cref="ActiveHost"/> variable this property
		/// returns the URL of the host for the active environment.
		/// </summary>
		/// <returns>URL of the host for the active environment.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Throw exception on
		/// argument out of range.</exception>
		public string Host
		{
			get
			{
				switch (ActiveHost)
				{
					case EnvironmentType.PRODUCTION: return _productionHost;
					case EnvironmentType.DEVELOPMENT: return _developmentHost;
					case EnvironmentType.OVERRIDE: return _overrideHost;
					default: throw new ArgumentOutOfRangeException ();
				}
			}
		}

		/// <summary>
		/// Defines which URL to use as host.
		/// </summary>
		public EnvironmentType ActiveHost
		{
			get => _activeHost;
			set => _activeHost = value;
		}

		public Auth Auth { get; set; }

		#endregion
	}
}