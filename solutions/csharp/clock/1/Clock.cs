using System;

public struct Clock : IEquatable<Clock>
{
    private readonly int _totalMinutes;

    public Clock(int hours, int minutes)
    {
        int total = (hours * 60 + minutes) % 1440;
        _totalMinutes = total < 0 ? total + 1440 : total;
    }

    public Clock Add(int minutesToAdd) => new Clock(0, _totalMinutes + minutesToAdd);

    public Clock Subtract(int minutesToSubtract) => new Clock(0, _totalMinutes - minutesToSubtract);

    public override string ToString() => $"{_totalMinutes / 60:D2}:{_totalMinutes % 60:D2}";

    public bool Equals(Clock other) => _totalMinutes == other._totalMinutes;

    public override bool Equals(object obj) => obj is Clock other && Equals(other);

    public override int GetHashCode() => _totalMinutes.GetHashCode();
}