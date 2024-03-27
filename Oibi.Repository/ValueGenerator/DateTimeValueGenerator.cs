using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace Oibi.Repository.ValueGenerator;

public class DateTimeValueGenerator : ValueGenerator<DateTime>
{
	public override bool GeneratesTemporaryValues => false;

	public override DateTime Next(EntityEntry entry)
	{
		return DateTime.UtcNow;
	}
}
