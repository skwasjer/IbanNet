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
		private string _example = string.Empty;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _structure = string.Empty;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _length;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _position;

		/// <summary>
		/// Initializes a new instance of the <see cref="StructureSection"/> class.
		/// </summary>
		protected internal StructureSection()
		{
			//
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StructureSection"/> class using specified parameters.
		/// </summary>
		/// <param name="structure">The structure.</param>
		// ReSharper disable once UnusedMember.Global
		protected StructureSection(string structure)
		{
			Structure = structure ?? throw new ArgumentNullException(nameof(structure));
		}

		/// <inheritdoc />
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

		/// <inheritdoc />
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

		/// <inheritdoc />
		public string Example
		{
			get => _example;
			set => _example = value ?? string.Empty;
		}

		/// <inheritdoc />
		public string Structure
		{
			get => _structure;
			internal set => _structure = value ?? string.Empty;
		}
	}
}