using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace IbanNet.Extensions
{
	public class PartitionExtensionTests
	{
		[Fact]
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

		[Theory]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(int.MinValue)]
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

		[Theory]
		[InlineData(1, 50, 1)]
		[InlineData(3, 17, 2)]
		[InlineData(9, 6, 5)]
		public void Given_collection_when_partitioning_it_should_return_correct_partitioned_enumerable(int size, int expectedPartitions, int expectedLastPartitionSize)
		{
			IEnumerable<int> sequence = Enumerable.Range(0, 50).Select((_, i) => i).ToList();

			// Act
			var actual = sequence.Partition(size).ToList();

			// Assert
			actual.Should().HaveCount(expectedPartitions);
			actual.Take(actual.Count - 1).Should().OnlyContain(inner => inner.Count() == size, "all but the last should at least be of the requested size");
			actual.Last().Should().HaveCount(expectedLastPartitionSize, "the last partition can be less than or equal to the requested size");
			actual.SelectMany(i => i).Should().BeEquivalentTo(sequence, "joined back together it should be same as original sequence");
		}

		[Fact]
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

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void Given_null_chars_when_partitioning_on_char_it_should_throw(string charsToPartitionOn)
		{
			char[] chars = charsToPartitionOn?.ToCharArray();

			// Act
			// ReSharper disable once ExpressionIsAlwaysNull
			Action act = () => string.Empty.PartitionOn(chars);

			// Assert
			act.Should()
				.Throw<ArgumentException>()
				.WithMessage(Resources.PartitionOn_At_least_one_character_to_partition_on_is_required + "*")
				.Which.ParamName.Should()
				.Be(nameof(chars));
		}

		[Theory]
		[InlineData(' ', null, "a ", "quick ", "brown ", "fox ", "jumps ", "over ", "the ", "lazy ", "dog")]
		[InlineData('o', 'm', "a quick bro", "wn fo", "x jum", "ps o", "ver the lazy do", "g")]
		public void Given_string_when_partitioning_it_should_return_correct_partitioned_enumerable1(char char1, char? char2, params string[] expectedPartitions)
		{
			const string sequence = "a quick brown fox jumps over the lazy dog";
			char[] chars = char2.HasValue ? new[] { char1, char2.Value } : new[] { char1 };

			// Act
			var actual = sequence.PartitionOn(chars).ToList();

			// Assert
			actual.Should().BeEquivalentTo(expectedPartitions);
		}
	}
}
