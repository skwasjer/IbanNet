using System;
using System.Diagnostics;
using IbanNet.Validation;

namespace IbanNet.Registry
{
	/// <summary>
	/// Defines a section of a structure.
	/// </summary>
	[DebuggerStepThrough]
	public abstract class StructureSection : IStructureSection
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _example;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _structure;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _length;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _position;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IStructureValidationFactory _structureValidationFactory;

		/// <summary>
		/// Initializes a new instance of the <see cref="StructureSection"/> class.
		/// </summary>
		protected internal StructureSection()
			: this(string.Empty, new NullStructureValidationFactory())
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StructureSection"/> class using specified parameters.
		/// </summary>
		/// <param name="structure">The structure.</param>
		/// <param name="structureValidationFactory">The validation factory.</param>
		// ReSharper disable once UnusedMember.Global
		protected StructureSection(string structure, IStructureValidationFactory structureValidationFactory)
		{
			_example = string.Empty;
			_structure = structure ?? throw new ArgumentNullException(nameof(structure));
			_structureValidationFactory = structureValidationFactory ?? throw new ArgumentNullException(nameof(structureValidationFactory));
		}

		/// <summary>
		/// Gets or sets the position within the structure.
		/// </summary>
		public int Position
		{
			get => _position;
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value));
				}

				_position = value;
			}
		}

		/// <summary>
		/// Gets or sets the section length.
		/// </summary>
		public int Length
		{
			get => _length;
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(value));
				}

				_length = value;
			}
		}

		/// <summary>
		/// Gets or sets the section example.
		/// </summary>
		public string Example
		{
			get => _example;
			set => _example = value ?? string.Empty;
		}

		/// <summary>
		/// Gets or sets the structure.
		/// </summary>
		public string Structure
		{
			get => _structure;
			internal set => _structure = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Gets or sets the structure validation factory.
		/// </summary>
		public IStructureValidationFactory ValidationFactory
		{
			get => _structureValidationFactory;
			internal set => _structureValidationFactory = value ?? throw new ArgumentNullException(nameof(value));
		}
	}
}
