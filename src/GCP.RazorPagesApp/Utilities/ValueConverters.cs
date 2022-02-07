
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GCP.RazorPagesApp.Utilities;
public class DateOnlyValueConverter : ValueConverter<DateOnly, DateTime>
{
	public DateOnlyValueConverter()
		: base(
			dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
			dateTime => DateOnly.FromDateTime(dateTime)
		)
	{
	}
}

public class DateOnlyComparer : ValueComparer<DateOnly>
{
	public DateOnlyComparer()
		: base(
			(d1, d2) => d1.DayNumber == d2.DayNumber,
			d => d.GetHashCode()
		)
	{
	}
}

public class TimeOnlyValueConverter : ValueConverter<TimeOnly, TimeSpan>
{
	public TimeOnlyValueConverter()
		: base(
			timeOnly => timeOnly.ToTimeSpan(),
			timeSpan => TimeOnly.FromTimeSpan(timeSpan)
		)
	{
	}
}

public class TimeOnlyComparer : ValueComparer<TimeOnly>
{
	public TimeOnlyComparer()
		: base(
			(t1, t2) => t1.Ticks == t2.Ticks,
			t => t.GetHashCode()
		)
	{
	}
}
