using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace IbanNet
{
	[TestFixture]
	public class ExtensionTests
	{
		[Test]
		public void Given_null_collection_when_partitioning_it_should_throw()
		{
			IEnumerable<object> sequence = null;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			Action act = () => sequence.Partition(2);

			// Assert
			act.Should()
				.Throw<ArgumentNullException>()
				.Which.ParamName.Should()
				.Be(nameof(sequence));
		}

		[TestCase(0)]
		[TestCase(-1)]
		[TestCase(int.MinValue)]
		public void Given_invalid_size_when_partitioning_it_should_throw(int size)
		{
			IEnumerable<object> sequence = new List<object>();

			// Act
			Action act = () => sequence.Partition(size);

			// Assert
			act.Should()
				.Throw<ArgumentOutOfRangeException>()
				.Which.ParamName.Should()
				.Be(nameof(size));
		}

		[TestCase(1, 50, 1)]
		[TestCase(3, 17, 2)]
		[TestCase(9, 6, 5)]
		public void Given_collection_when_partitioning_it_should_return_correct_partitioned_enumerable(int size, int expectedPartitions, int expectedLastPartitionSize)
		{
			IEnumerable<int> sequence = Enumerable.Range(0, 50).Select((_, i) => i).ToList();

			// Act
			List<IEnumerable<int>> actual = sequence.Partition(size).ToList();

			// Assert
			actual.Should().HaveCount(expectedPartitions);
			actual.Take(actual.Count - 1).Should().OnlyContain(inner => inner.Count() == size, "all but the last should at least be of the requested size");
			actual.Last().Should().HaveCount(expectedLastPartitionSize, "the last partition can be less than or equal to the requested size");
			actual.SelectMany(i => i).Should().BeEquivalentTo(sequence, "joined back together it should be same as original sequence");
		}

		[Test]
		public void Given_null_string_when_partitioning_on_char_it_should_throw()
		{
			string sequence = null;

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			Action act = () => sequence.PartitionOn(' ');

			// Assert
			act.Should()
				.Throw<ArgumentNullException>()
				.Which.ParamName.Should()
				.Be(nameof(sequence));
		}

		[TestCase(null)]
		[TestCase("")]
		public void Given_null_chars_when_partitioning_on_char_it_should_throw(string charsToPartitionOn)
		{
			char[] chars = charsToPartitionOn?.ToCharArray();

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			Action act = () => string.Empty.PartitionOn(chars);

			// Assert
			act.Should()
				.Throw<ArgumentException>()
				.WithMessage("At least one character to partition on is required.*")
				.Which.ParamName.Should()
				.Be(nameof(chars));
		}

		[TestCase(' ', null, "a ", "quick ", "brown ", "fox ", "jumps ", "over ", "the ", "lazy ", "dog")]
		[TestCase('o', 'm', "a quick bro", "wn fo", "x jum", "ps o", "ver the lazy do", "g")]
		public void Given_string_when_partitioning_it_should_return_correct_partitioned_enumerable1(char char1, char? char2, params string[] expectedPartitions)
		{
			const string sequence = "a quick brown fox jumps over the lazy dog";
			char[] chars = char2.HasValue ? new[] { char1, char2.Value } : new[] { char1 };

			// Act
			List<string> actual = sequence.PartitionOn(chars).ToList();

			// Assert
			actual.Should().BeEquivalentTo(expectedPartitions);
		}
	}
}
