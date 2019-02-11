using System;
using System.Collections.Concurrent;

namespace IbanNet.Validation
{
	/// <summary>
	/// Represents a cache for validators.
	/// </summary>
	internal class CachedStructureValidationFactory : IStructureValidationFactory
	{
		private readonly ConcurrentDictionary<string, IStructureValidator> _cache;
		private readonly IStructureValidationFactory _innerFactory;

		public CachedStructureValidationFactory(IStructureValidationFactory innerFactory)
		{
			_innerFactory = innerFactory ?? throw new ArgumentNullException(nameof(innerFactory));

			_cache = new ConcurrentDictionary<string, IStructureValidator>();
		}

		// ReSharper disable once InconsistentNaming
		public IStructureValidator CreateValidator(string twoLetterISORegionName, string structure)
		{
			string cacheKey = twoLetterISORegionName + "-" + structure;
			return _cache.GetOrAdd(cacheKey, s => _innerFactory.CreateValidator(twoLetterISORegionName, structure));
		}
	}
}
