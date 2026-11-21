using System;
using System.Collections.Generic;
using System.Linq;

public static class VariableLengthQuantity
{
    public static uint[] Encode(uint[] numbers)
    {
        var result = new List<uint>();
        foreach (var number in numbers)
        {
            var bytes = new Stack<uint>();
            uint n = number;
            bytes.Push(n & 0x7F);
            while ((n >>= 7) > 0)
            {
                bytes.Push((n & 0x7F) | 0x80);
            }
            result.AddRange(bytes);
        }
        return result.ToArray();
    }

    public static uint[] Decode(uint[] bytes)
    {
        var result = new List<uint>();
        uint current = 0;
        bool incomplete = false;

        foreach (var b in bytes)
        {
            if ((current & 0xFE000000) != 0)
                throw new OverflowException();

            current = (current << 7) | (b & 0x7F);
            if ((b & 0x80) == 0)
            {
                result.Add(current);
                current = 0;
                incomplete = false;
            }
            else
            {
                incomplete = true;
            }
        }

        if (incomplete)
            throw new InvalidOperationException();

        return result.ToArray();
    }
}