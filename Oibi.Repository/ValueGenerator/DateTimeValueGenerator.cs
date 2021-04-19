using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Oibi.Repository.ValueGenerator
{
	public class DateTimeValueGenerator : ValueGenerator<DateTime>
	{
		public override bool GeneratesTemporaryValues => false;

		public override DateTime Next([NotNullAttribute] EntityEntry entry)
		{
			return DateTime.UtcNow;
		}
	}
}