using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    class SudokuCell : IEquatable<SudokuCell>
    {
        public int Value { get; private set; }
        public bool IsChangeable { get; private set; }

        public SudokuCell()
        {
            Value = 0;
            IsChangeable = true;
        }
        
        public bool TrySet(int num, bool isChangeable)
        {
            // allow to set 0 - 9
            if (num >= 0 && num <= 9)
            {
                if (this.IsChangeable.Equals(true))
                {
                    // make change
                    Value = num;
                    IsChangeable = isChangeable;
                    return true;
                }
                else
                {
                    // this value was set initially and is not allowed to change
                    // below was causing out of memory errors for very difficult sudokus
                    // Console.WriteLine($"Cell value was part of initial setup, and can't change");
                    return false;
                }
            }
            else {
                // invalid value
                Console.WriteLine($"Cell value can be 0-9, and not null. Value provided: {num}");
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SudokuCell);
        }

        public bool Equals(SudokuCell other)
        {
            return other != null &&
                   Value == other.Value &&
                   IsChangeable == other.IsChangeable;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, IsChangeable);
        }
    }
}
