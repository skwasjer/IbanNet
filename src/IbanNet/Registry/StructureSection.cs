using System;
using System.Diagnostics;

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

		/// <summary>
		/// Initializes a new instance of the <see cref="StructureSection"/> class.
		/// </summary>
		protected internal StructureSection()
		{
			_example = string.Empty;
			_structure = string.Empty;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StructureSection"/> class using specified parameters.
		/// </summary>
		/// <param name="structure">The structure.</param>
		// ReSharper disable once UnusedMember.Global
		protected StructureSection(string structure)
			: this()
		{
			Structure = structure ?? throw new ArgumentNullException(nameof(structure));
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
	}
}
